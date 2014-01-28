using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NBusClassLibrary
{
    public class Direction : NBusComponent
    {
        internal Direction(XElement root)
            : base(root)
        {

        }
    }
}
