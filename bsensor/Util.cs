using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace bsensor
{
    public class Util
    {
        private const string EXCEPTION = "Exception";

        public static void LogException(String tag, Exception ex)
        {
            Debug.Write(ex.Message, tag);
            MessageBox.Show(ex.Message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void LogException(String tag, String additionalInfo, Exception ex)
        {
            string message = "(" + additionalInfo + ") " + ex.Message;
            Debug.Write(message, tag);
            MessageBox.Show(message, EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string ToRGB(Color color)
        {
            return String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        public static Color FromText(string hexString)
        {
            return Color.FromArgb(Int32.Parse("FF" + hexString.Substring(1), System.Globalization.NumberStyles.HexNumber));
        }

        public static string ProjectNameFromFileName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        public static IList<double> DistancesFromLatLng(IList<LatLng> latLngs)
        {
            IList<double> distances = null;
            double totalDistance = 0.0;
            double distance = 0.0;

            if (null != latLngs)
            {
                distances = new List<double>();
                if (latLngs.Count > 0)
                {
                    distances.Add(0.0);
                    if (latLngs.Count > 1)
                    {
                        for (int i = 1; i < latLngs.Count; ++i)
                        {
                            distance = DistanceBetweenLatLngs(latLngs[i - 1], latLngs[i]);

                            totalDistance += distance;
                            if (totalDistance == double.NaN)
                            {

                            }

                            distances.Add(totalDistance);
                        }
                    }
                }
            }
            return distances;
        }

        public static double DistanceBetweenLatLngs(LatLng p1, LatLng p2)
        {
            //return UtilLatLng.distance1(p1.Latitude, p1.Longitude, p2.Latitude, p2.Longitude, 'M');
            return UtilLatLng.distance2(p1.Latitude, p1.Longitude, p2.Latitude, p2.Longitude, 'M');
        }

        /// <summary>
        /// Returns the maximum value found within the two lists. 
        /// </summary>
        /// <param name="values1">First List of values.  this list must not be empty or null.</param>
        /// <param name="values2">Second list of values.  This list may be empty or null.</param>
        /// <returns>Maximum value found within the two lists.</returns>
        public static double MaxValue(IList<double> values1, IList<double> values2)
        {
            double maxValue1;
            double maxValue2;

            maxValue1 = MaxValue(values1);

            if ((null != values2) && (values2.Count > 0))
            {
                maxValue2 = MaxValue(values2);
                return maxValue1 > maxValue2 ? maxValue1 : maxValue2;
            }
            return maxValue1;
        }

        public static double MaxValue(IList<double> values)
        {
            double maxValue = values[0];
            foreach (double value in values)
            {
                if (value > maxValue)
                    maxValue = value;
            }
            return maxValue;
        }

        public static DateTime MaxValue(IList<DateTime> values1, IList<DateTime> values2)
        {
            DateTime maxValue1 = MaxValue(values1);
            DateTime maxValue2 = MaxValue(values2);

            return maxValue1 > maxValue2 ? maxValue1 : maxValue2;
        }

        public static DateTime MaxValue(IList<DateTime> values)
        {
            DateTime maxValue = values[0];
            foreach (DateTime value in values)
            {
                if (value > maxValue)
                    maxValue = value;
            }
            return maxValue;
        }

        public static DateTime MinValue(IList<DateTime> values1, IList<DateTime> values2)
        {
            DateTime minValue1 = MinValue(values1);
            DateTime minValue2 = MinValue(values2);

            return minValue1 < minValue2 ? minValue1 : minValue2;
        }

        public static DateTime MinValue(IList<DateTime> values)
        {
            DateTime minValue = values[0];
            foreach (DateTime value in values)
            {
                if (value < minValue)
                    minValue = value;
            }
            return minValue;
        }
    }
}
