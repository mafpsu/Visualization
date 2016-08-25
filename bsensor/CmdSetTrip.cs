using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    class CmdSetTrip : BsensorCommand
    {
        private MyApplication myApp = null;

        public CmdSetTrip(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        public override void Execute(int key)
        {
            myApp.SetTrip(key);
        }
    }
}
