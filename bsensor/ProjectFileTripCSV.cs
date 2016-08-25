using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class ProjectFileTripCSV : ProjectFileTrip
    {
        private string fileName;

        public ProjectFileTripCSV(string fileName)
        {
            this.fileName = fileName;
        }

        public string FileName
        {
            get { return fileName; }
        }
    }


}
