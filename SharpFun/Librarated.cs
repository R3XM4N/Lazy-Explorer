using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer
{
    public class Displayed
    {
        public string Name { get; set; }
        public string PathWay { get; private set; }

        public Displayed() { }
        public Displayed(string name, string pathWay)
        {
            Name = name;
            PathWay = pathWay;
        }
    }
}
