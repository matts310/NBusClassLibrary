using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NBusClassLibrary
{
   
    public class Agency : NBusComponent
    {
        //private List<string> routeTags;
        private Dictionary<string, SimpleRoute> simpleRoutes;
        public Dictionary<string, SimpleRoute> SimpleRoutes
        {
            get
            {
                if (simpleRoutes.Count > 0)
                {
                    Dictionary<string, SimpleRoute> toRet = new Dictionary<string, SimpleRoute>();
                    foreach (string key in simpleRoutes.Keys)
                    {
                        toRet[key] = simpleRoutes[key];
                    }
                    return toRet;
                }
                else
                {
                    simpleRoutes = NBusApi.getSimpleRouteList(Tag);
                    return SimpleRoutes;
                }
            }
        }

        Dictionary<string, Route> routeShortCircuit;
        /// <summary>
        /// Returns a Route object designated by the route string
        /// </summary>
        /// <param name="routeTag"></param>
        /// <returns></returns>
        public Route getRoute(string routeTag)
        {
            if (routeShortCircuit.ContainsKey(routeTag))
                return routeShortCircuit[routeTag];
            else
            {
                Route tmp = NBusApi.getRoute(Tag, routeTag);
                routeShortCircuit.Add(tmp.Tag, tmp);
                return tmp;
            }
        }

        internal Agency(XElement root)
            : base(root)
        {
            routeShortCircuit = new Dictionary<string, Route>();
            simpleRoutes = new Dictionary<string, SimpleRoute>();
        }

         public static Agency deserialize(String input) 
        {
            XElement root = XElement.Parse(input);
            Agency toRet = new Agency(root);
            return toRet;
        }

    }
}
