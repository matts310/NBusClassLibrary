using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NBusClassLibrary
{
    public class Stop : NBusComponent
    {
        internal Stop(XElement root)
            : base(root)
        {

        }

        public static Stop deserialize(String input)
        {
            XElement root = XElement.Parse(input);
            Stop toRet = new Stop(root);
            return toRet;
        }

        /// <summary>
        /// Retrieves a list of predictions for the current stop
        /// </summary>
        /// <param name="agencyTag"></param> Agency tag
        /// <param name="routeTag"></param> Route tag
        /// <returns></returns> returns a list of Predictions
        public List<Prediction> getPredictions(string agencyTag, string routeTag)
        {
            return NBusApi.getPredictions(agencyTag, routeTag, Tag);
        }
    }
}
