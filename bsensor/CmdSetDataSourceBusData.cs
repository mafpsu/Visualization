﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsensor
{
    public class CmdSetDataSourceBusData : BsensorCommand
    {
        private MyApplication myApp = null;

        public CmdSetDataSourceBusData(MyApplication myApp)
        {
            this.myApp = myApp;
        }

        public override void Execute(string fileName)
        {
            myApp.SetDataSourceBusData(fileName);
        }
    }
}
