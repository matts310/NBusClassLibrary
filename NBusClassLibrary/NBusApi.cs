using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NBusClassLibrary
{
    public static class NBusApi
    {
        private static string url = "http://webservices.nextbus.com/service/publicXMLFeed?command=";
        
        //Can throw WebException
        internal static XDocument getXml(string cmd) 
        {
            XDocument toRet = null;
            toRet = XDocument.Load(url + cmd);
            if (toRet.Descendants("Error").Count() != 0)
            {
                throw new NextBusAPIException(toRet.Descendants("Error").First().Value);
            }
            return toRet;
        }

        public class NextBusAPIException : Exception
        {
            public NextBusAPIException(String message) : base(message) { }
        }

        internal static string agencyList()
        {
            return "agencyList";
        }

        internal static string routeList(string agencyTag)
        {
            return "routeList&a=" + agencyTag;
        }

        internal static string routeConfig(string agencyTag)
        {
            return "routeConfig&a=" + agencyTag;
        }

        internal static string routeConfig(string agencyTag, string routeTag)
        {
            return "routeConfig&a=" + agencyTag + "&r=" + routeTag;
        }

        internal static string predictions(string agencyTag, string routeTag, string stopTag)
        {
            return "predictions&a=" + agencyTag + "&r=" + routeTag + "&s=" + stopTag;
        }

        internal static Route getRoute(string agencyTag, string routeTag)
        {
            XDocument xml = getXml(routeConfig(agencyTag, routeTag));

            //routeConfig only expects 1 route element, so just take the first & only one out of the list.
            XElement routeRoot = xml.Descendants("route").First();
            return new Route(routeRoot);
        }

        internal static List<string> getRouteTags(string agencyTag)
        {
            XDocument xml = getXml(routeList(agencyTag));
            List<string> toRet = new List<string>();
            foreach (XElement route in xml.Descendants("route"))
            {
                toRet.Add(route.Attribute("tag").Value);
            }
            return toRet;
        }
        
        public static Dictionary<string, Agency> getAgencies()
        {
            Dictionary<string, Agency> agencies = new Dictionary<string,Agency>();
            XDocument xml = getXml(agencyList());
            
            foreach(XElement agency in xml.Descendants("agency"))
            {
                Agency tmp = new Agency(agency);
                agencies.Add(tmp.Tag, tmp);
            }
            return agencies;
        }

        internal static Dictionary<string, SimpleRoute> getSimpleRouteList(String agencyTag)
        {
            Dictionary<string, SimpleRoute> routes = new Dictionary<string, SimpleRoute>();
            XDocument xml = getXml(routeList(agencyTag));

            foreach (XElement agency in xml.Descendants("route"))
            {
                SimpleRoute tmp = new SimpleRoute(agency);
                routes.Add(tmp.Tag, tmp);
            }
            return routes;
        }

        internal static List<Prediction> getPredictions(string agencyTag, string routeTag, string stopTag)
        {
            XDocument xml = getXml(predictions(agencyTag, routeTag, stopTag));

            List<Prediction> toRet = new List<Prediction>();
            foreach(XElement prediction  in xml.Descendants("prediction"))
            {
                toRet.Add(new Prediction(prediction));
            }

            return toRet;
        }
    }
}
