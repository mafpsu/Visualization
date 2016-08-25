using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace bsensor
{
    public class BsensorCommand
    {
        public BsensorCommand()
        {

        }

        public virtual void Execute()
        {

        }

        public virtual void Execute(IList<long> l)
        {

        }

        public virtual void Execute(long id)
        {

        }

        public virtual void Execute(string s)
        {

        }

        public virtual void Execute(int start, int stop, Color color)
        {

        }

        public virtual void Execute(string dataSetName, string rangeName, double lowValue, double highValue, Color color)
        {

        }

        public virtual void Execute(int dataIndex)
        {

        }

        public virtual void Execute(string s1, string s2)
        {

        }

        public virtual void Execute(string dataSetName, GraphColorRange[] graphColorRanges)
        {

        }
    }
}
