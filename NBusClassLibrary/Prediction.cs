using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NBusClassLibrary
{

    //NBusComponent has tag and title, just ignore them
    public class Prediction : NBusComponent
    {
        /// <summary>
        /// DO NOT USE THIS. EVER. NOT EVEN ONCE. DON'T EVEN THINK ABOUT IT.
        /// </summary>
        public string Tag
        {
            get { return "NOT DEFINED FOR PREDICTIONS"; }
        }

        /// <summary>
        /// DO NOT USE THIS. EVER. NOT EVEN ONCE. DON'T EVEN THINK ABOUT IT.
        /// </summary>
        public string Title
        {
            get { return "NOT DEFINED FOR PREDICTIONS"; }
        }

        internal Prediction(XElement root) : base(root)
        {

        }
    }
}
