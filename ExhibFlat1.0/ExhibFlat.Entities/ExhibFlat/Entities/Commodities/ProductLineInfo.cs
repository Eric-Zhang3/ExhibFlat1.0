namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class ProductLineInfo
    {
        public int LineId { get; set; }

        [StringLengthValidator(1, 60, Ruleset="ValProductLine", MessageTemplate="产品线名称不能为空，长度限制在1-60个字符之间"), HtmlCoding]
        public string Name { get; set; }

        public string SupplierName { get; set; }

        public int UserId { get; set; }
        public string SupplierId { get; set; }
    }
}

