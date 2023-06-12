using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Xylia.Preview.Data.Helper.Output;

public class OutSetTable
{
    [XmlElement]
    public float version;

    [XmlElement]
    public string type;

    [XmlElement]
    public List<Attribute> attribute = new();


    public class Attribute : ICloneable
    {
        [XmlAttribute]
        public string name;

        [XmlAttribute]
        public string text;

        [XmlAttribute]
        public uint repeat;


        [XmlAttribute("ref")]
        public string Ref;

        [XmlAttribute]
        public string extra;





        #region Constructor
        public Attribute()
        {

        }

        public Attribute(string name)
        {
            this.name = name;
        }
        #endregion

        #region ICloneable
        object ICloneable.Clone() => MemberwiseClone();

        public Attribute Clone() => (Attribute)MemberwiseClone();
        #endregion
    }
}