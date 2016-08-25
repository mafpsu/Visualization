using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class CmdAddRanges : BsensorCommand
    {
        private MyApplication myApp = null;

        public CmdAddRanges(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        public override void Execute(string dataSetName, GraphColorRange[] graphColorRanges)
        {
            myApp.AddRanges(dataSetName, graphColorRanges, true);
        }
    }
}
