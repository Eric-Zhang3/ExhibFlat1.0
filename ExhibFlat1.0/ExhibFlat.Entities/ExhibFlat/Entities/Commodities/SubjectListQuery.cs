namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SubjectListQuery : Pagination
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

        public int? BrandCategoryId { get; set; }

        public string CategoryIds { get; set; }

        public string Keywords { get; set; }

        public int MaxNum { get; set; }

        public decimal? MaxPrice { get; set; }

        public decimal? MinPrice { get; set; }

        public int? ProductTypeId { get; set; }

        public int TagId { get; set; }
    }
}

