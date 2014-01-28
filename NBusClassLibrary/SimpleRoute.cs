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
            //this.deserializer = new SimpleRouteDeserializer();
            //this.serializer = new SimpleRouteSerializer(this);
        }

        public static SimpleRoute deserialize(String input)
        {
            XElement root = XElement.Parse(input);
            SimpleRoute toRet = new SimpleRoute(root);
            return toRet;
        }

        /*
        
        private class SimpleRouteSerializer : NBusSerializer 
        {

            SimpleRoute comp;
            public SimpleRouteSerializer(SimpleRoute srIn) : base(srIn)
            {
                comp = (SimpleRoute)srIn;
                 
            }

            protected override string output()
            {
                //dont need anything more than just the node
                return base.output();
            }
        }

        private class SimpleRouteDeserializer : NBusDeserializer
        {
            public SimpleRouteDeserializer()
                : base()
            {
            }

            public override SimpleRoute deserialize(string input)
            {
                return new SimpleRoute(XElement.Parse(input));
            }
        }*/
    }
}
