using ExhibFlat.Core;
using ExhibFlat.Components.Validation;
using ExhibFlat.Components.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExhibFlat.Entities.Members
{
    //[HasSelfValidation]
    public class ChanelQuery
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        //[StringLengthValidator(1, 20, Ruleset = "ValEditChanel", MessageTemplate = "姓名长度在20个字符以内"), HtmlCoding]
        public string RealName { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public int? DistributorRate  { get; set; }

        public int? SupplierRate { get; set; }

        public int? ServiceRate { get; set; }

        public int? ParentID { set; get; }

        public string RequertCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? Indate { get; set; }

        public DateTime? Outdate { get; set; }

        public string UserRole { get; set; }

        //[SelfValidation(Ruleset = "ValEditChanel")]
        //public void CheckChanel(ValidationResults results)
        //{
        //    if ((this.RealName.Length > 20))
        //    {
        //        results.AddResult(new ValidationResult("姓名长度在20个字符以内", this, "", "", null));
        //    }
        //    if (Regex.IsMatch(this.DistributorRate.ToString(),"^(0|[1-9]\\d*)$"))
        //    {
        //        results.AddResult(new ValidationResult("比例只能为非负整数", this, "", "", null));
        //    }
        //    if (Regex.IsMatch(this.SupplierRate.ToString(),"^(0|[1-9]\\d*)$"))
        //    {
        //        results.AddResult(new ValidationResult("比例只能为非负整数", this, "", "", null));
        //    }
        //    if (Regex.IsMatch(this.ServiceRate.ToString(),"^(0|[1-9]\\d*)$"))
        //    {
        //        results.AddResult(new ValidationResult("比例只能为非负整数", this, "", "", null));
        //    }
        //}
    }
}
