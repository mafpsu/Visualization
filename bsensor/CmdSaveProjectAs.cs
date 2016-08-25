using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class CmdSaveProjectAs : BsensorCommand
    {
        private MyApplication myApp = null;

        public CmdSaveProjectAs(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        public override void Execute()
        {
            bool cancel;
            myApp.SaveProjectAs(out cancel);
        }
    }
}
