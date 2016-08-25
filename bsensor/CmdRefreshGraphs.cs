using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class CmdRefreshGraphs : BsensorCommand
    {
        private MyApplication myApp = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myApp"></param>
        public CmdRefreshGraphs(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Execute()
        {
            myApp.RefreshGraphs();
        }
    }
}
