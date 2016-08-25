using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace bsensor
{
    public class CmdAddRange : BsensorCommand
    {
        private MyApplication myApp = null;

        public CmdAddRange(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        public override void Execute(string dataSetName, string rangeName, double lowValue, double highValue, Color color)
        {
            //myApp.ColorCoordinates(dataSetName, rangeName, lowValue, highValue, color);
        }
    }
}
