using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NBusClassLibrary
{
    public static class NBusSerializer
    {
        public static String serializeOutput(NBusComponent component)
        {
            return component.root.ToString();
        }

        public static NBusComponent deserialize(String input, Type NBusComponentType) 
        {
            return (NBusComponent)Activator.CreateInstance(NBusComponentType, XElement.Parse(input));
        }
    }
}
