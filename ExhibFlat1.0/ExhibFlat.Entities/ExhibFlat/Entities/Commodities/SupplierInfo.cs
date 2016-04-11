using ExhibFlat.Core;
using ExhibFlat.Components.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ExhibFlat.Entities.Commodities
{
   public   class SupplierInfo
    {
        //供应商信息
        public int UserId { get; set; }

        [StringLengthValidator(2, 20, Ruleset = "ValProductSupplier", MessageTemplate = "商品类型名称不能为空，长度限制在2-20个字符之间")]
        public string SupplierName { get; set; }

        [HtmlCoding, StringLengthValidator(0, 500, Ruleset = "ValProductSupplier", MessageTemplate = "备注的长度限制在0-500个字符之间")]
        public string Remark { get; set; }

        public string SupplierId { get; set; }


    }
}
