using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Description;


namespace NBusClassLibrary
{
    public class SimpleRoute :  NBusComponent

    {
        internal SimpleRoute(XElement root)
            : base(root)
        {
 
        }
    }
}
