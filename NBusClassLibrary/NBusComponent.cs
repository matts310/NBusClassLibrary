using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace NBusClassLibrary
{

    /// <summary>
    /// The idea behind this abstract class is that all NBusComponents (except for predictions) have a tag and title, 
    /// but variable other values/attributes. In the XML, these values are stored as node/element attributes. Instead
    /// of hardcoding and retrieving each value individually, all attributes are stored in a fieldMap. The attribute is
    /// accessed by using the getField method by entering the attribute name of the desired property. These can be found
    /// in the NextBus XML files and the NextBus API guide online.
    /// </summary>
    public abstract class NBusComponent
    {
        private Dictionary<string, string> fieldMap;

        protected Dictionary<string, string> FieldMap
        {
            get
            {
                Dictionary<string, string> toRet = new Dictionary<string, string>();
                foreach(string key in fieldMap.Keys)
                {
                    toRet[key] = fieldMap[key];
                }
                return toRet;
            }
        }

        private string tag, title;
        public string Tag
        {
            get
            {
                if (tag == null)
                {
                    if (fieldMap.ContainsKey("tag"))
                    {
                        tag = fieldMap["tag"];
                        return tag;
                    }
                    else
                        return null;
                }
                else
                    return tag;
            }
        }

        public string Title
        {
            get
            {
                if (title == null)
                {
                    if (fieldMap.ContainsKey("title"))
                    {
                        title = fieldMap["title"];
                        return title;
                    }
                    else
                        return null;
                }
                else
                    return title;
            }
        }

        protected XElement root;


        internal NBusComponent(XElement rootIn)
        {
            this.root = rootIn;
            this.fieldMap = new Dictionary<string, string>();
            defineFieldMap();
        }

   /*     internal NBusComponent(XElement root, toStringDelegate dIn)
            : this(root)
        {
            this.toStringMethod = dIn;
        }*/

        private void defineFieldMap()
        {
            foreach(XAttribute atr in root.Attributes())
            {
                fieldMap.Add(atr.Name.ToString(), atr.Value);
            }
        }

        /// <summary>
        /// Retrieves the string value that maps to the key parameter. 
        /// 
        /// Values stored in the xml are extracted by putting all attribute-value pairs
        /// into a mapping for each NBusComponent. 
        /// </summary>
        /// <param name="key"></param> name of the attribute being sought
        /// <returns></returns> value of attribute that is paired with the key
        public string getField(string key)
        {
            if (fieldMap.ContainsKey(key))
                return fieldMap[key];
            else
                return null;
        }

         public override string ToString()
        {

            if (toStringMethod == null)
            {
                StringBuilder toRet = new StringBuilder();
                foreach (string key in fieldMap.Keys)
                {
                    if (fieldMap[key] != "") // some stuff had literally "", so useless
                        toRet.AppendLine(key + ": " + fieldMap[key]);
                }
                return toRet.ToString();
            }
            else
            {
                return toStringMethod(this);
            }
        }
 
        public toStringDelegate toStringMethod;

        public delegate string toStringDelegate(NBusComponent componentIn);

        /// <summary>
        /// Compares another NBusComponent by comparing field maps
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            NBusComponent other = (NBusComponent)obj;

            foreach (string key in this.fieldMap.Keys)
            {
                if (other.fieldMap.ContainsKey(key))
                {
                    if (other.fieldMap[key] != this.fieldMap[key])
                        return false;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        internal static Route createRoute(string agencyTag, string routeTag)
        {
            return NBusApi.getRoute(agencyTag, routeTag);
        }
    }
}
