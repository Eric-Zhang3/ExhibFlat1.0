namespace ExhibFlat.Entities.Commodities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    [Serializable]
    public class DxAttriDef
    {
        private IList<AttributeValueInfo> attributeValues;
        private IList<AttributeInfo> attributes;
        public int? AttriDefID { get; set; }

        public string AttriName { get; set; }

        public int?  InputMode{ get; set; }
        public IList<AttributeValueInfo> AttributeValues
        {
            get
            {
                if (this.attributeValues == null)
                {
                    this.attributeValues = new List<AttributeValueInfo>();
                }
                return this.attributeValues;
            }
            set
            {
                this.attributeValues = value;
            }
        }
        public IList<AttributeInfo> Attributes
        {
            get
            {
                if (this.attributes == null)
                {
                    this.attributes = new List<AttributeInfo>();
                }
                return this.attributes;
            }
            set
            {
                this.attributes = value;
            }
        }


    }
}