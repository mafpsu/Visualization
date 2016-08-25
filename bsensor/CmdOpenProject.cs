using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    class CmdOpenProject : BsensorCommand
    {
        private MyApplication myApp = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myApp"></param>
        public CmdOpenProject(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Execute(string fileName)
        {
            myApp.OpenProject(fileName);
        }
    }
}
