using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class CmdSetDataSourceBikeData:BsensorCommand
    {
        private MyApplication myApp = null;

        public CmdSetDataSourceBikeData(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        public override void Execute(IList<long> tripIDs)
        {
            myApp.SetDataSourceBikeData(tripIDs);
        }
    }
}
