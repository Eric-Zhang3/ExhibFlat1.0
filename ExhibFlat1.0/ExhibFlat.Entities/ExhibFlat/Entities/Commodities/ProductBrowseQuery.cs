namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ProductBrowseQuery : Pagination
    {
        private IList<AttributeValueInfo> attributeValues;

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

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public bool IsPrecise { get; set; }

        public string Keywords { get; set; }

        public decimal? MaxSalePrice { get; set; }

        public decimal? MinSalePrice { get; set; }

        public string ProductCode { get; set; }

        public string SubjectType { get; set; }
        //产品ID
        public int? ProductId { get; set; }
    }
}

