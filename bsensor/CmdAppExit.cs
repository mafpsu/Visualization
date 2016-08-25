using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    class CmdAppExit : BsensorCommand
    {
        private MyApplication myApp = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myApp"></param>
        public CmdAppExit(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Execute()
        {
            myApp.Exit();
        }
    }
}
