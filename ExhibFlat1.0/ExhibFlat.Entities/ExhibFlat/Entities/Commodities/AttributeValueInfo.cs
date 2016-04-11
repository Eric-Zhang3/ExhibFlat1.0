namespace ExhibFlat.Entities.Commodities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class AttributeValueInfo
    {
        public int AttributeId { get; set; }

        public int DisplaySequence { get; set; }

        public string ImageUrl { get; set; }

        public int ValueId { get; set; }

        public string ValueStr { get; set; }

        public int DefValueId { get; set; }

        public AttributeValueType ValueType { get; set; }
    }
    public enum AttributeValueType
    {
        NoThing, Memo, Custom, Alias
    }
}

