using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NBusClassLibrary
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class Route : NBusComponent 
    {
        private Dictionary<string, Direction> directions;
        private Dictionary<string, Stop> stops;

        //key is direction tag, value is the stops for that direction
        private Dictionary<string, List<Stop>> directedStops;

        /// <summary>
        /// Mapping between the tag of a direction and a list of the corresponding stops
        /// </summary>
        public Dictionary<string, List<Stop>> DirectedStops
        {
            get
            {
                Dictionary<string, List<Stop>> toRet = new Dictionary<string, List<Stop>>();
                foreach (string key in directions.Keys)
                {
                    List<Stop> localStops = new List<Stop>();
                    foreach(Stop curStop in directedStops[key])
                    {
                        localStops.Add(curStop);
                    }
                    toRet.Add(key, localStops);
                }
                return toRet;
            }
        }

        /// <summary>
        /// Map of the stops: value is the stop, key is its tag
        /// </summary>
        public Dictionary<string, Stop> Stops
        {
            get
            {
                Dictionary<string, Stop> toRet = new Dictionary<string, Stop>();
                foreach (string key in stops.Keys)
                {
                    toRet.Add(key, stops[key]);
                }
                return toRet;
            }
        }

        /// <summary>
        /// Map of the directions: value is the Direction, key is its tag
        /// </summary>
        public Dictionary<string, Direction> Directions
        {
            get
            {
                Dictionary<string, Direction> toRet = new Dictionary<string, Direction>();
                foreach (string key in directions.Keys)
                {
                    toRet.Add(key, directions[key]);
                }
                return toRet;
            }
        }

        public Route(XElement root)
            : base(root)
        {
            directions = getDirections();
            stops = getStops();
            directedStops = composeDirectedStops();
        }
        
        private Dictionary<string, List<Stop>> composeDirectedStops()
        {
            Dictionary<string, List<Stop>> toRet = new Dictionary<string, List<Stop>>();
            foreach(XElement dir in root.Descendants("direction"))
            {
                string dirTag = dir.Attribute("tag").Value;
                List<Stop> localDirStops = new List<Stop>();
                
                foreach(XElement stop in dir.Descendants("stop"))
                {
                    localDirStops.Add(this.stops[stop.Attribute("tag").Value]);
                }
                toRet.Add(dirTag, localDirStops);
            }
            return toRet;
        }

        private Dictionary<string, Direction> getDirections()
        {
            Dictionary<string, Direction> toRet = new Dictionary<string, Direction>();
            foreach(XElement dir in root.Descendants("direction"))
            {
                Direction tmp = new Direction(dir);
                toRet.Add(tmp.Tag, tmp);
            }
            return toRet;
        }

        private Dictionary<string, Stop> getStops()
        {
            Dictionary<string, Stop> toRet = new Dictionary<string, Stop>();
            foreach(XElement stop in root.Descendants("stop"))
            {
                //Descendants are in document order -> stops outside of directions come first and
                //have the actual stop data, whereas the stops under Direction only have a tag attribute.
                if (stop.Attributes().Count() == 1)
                    break;

                Stop tmp = new Stop(stop);
                toRet.Add(tmp.Tag, tmp);
            }
            return toRet;
        } 

        public static Route deserialize(String input)
        {
            XElement root = XElement.Parse(input);
            Route toRet = new Route(root);
            return toRet;
        }
    }
}
