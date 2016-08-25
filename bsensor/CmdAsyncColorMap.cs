using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    class CmdAsyncColorMap : BsensorCommand
    {
        private MyApplication myApp = null;

        public CmdAsyncColorMap(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        public override void Execute()
        {
            //myApp.AsyncColorMap();
        }
    }
}
