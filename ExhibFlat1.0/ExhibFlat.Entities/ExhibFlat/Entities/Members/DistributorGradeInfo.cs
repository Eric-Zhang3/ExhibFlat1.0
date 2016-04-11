namespace ExhibFlat.Entities.Members
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class DistributorGradeInfo
    {
        [StringLengthValidator(0, 300, Ruleset="ValDistributorGrade", MessageTemplate="备注的长度限制在300个字符以内"), HtmlCoding]
        public string Description { get; set; }

        [RangeValidator(typeof(int), "1", RangeBoundaryType.Inclusive, "100", RangeBoundaryType.Inclusive, Ruleset="ValDistributorGrade", MessageTemplate="等级折扣必须在1-100之间")]
        public int Discount { get; set; }

        public int GradeId { get; set; }

        [StringLengthValidator(1, 60, Ruleset="ValDistributorGrade", MessageTemplate="分销商等级名称不能为空，长度限制在60个字符以内"), HtmlCoding]
        public string Name { get; set; }
    }
}

