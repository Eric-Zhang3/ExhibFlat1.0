namespace ExhibFlat.Entities.Store
{
    using ExhibFlat.Core;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class AdvPositionInfo
    {
        public string AdvHtml { get; set; }

        [HtmlCoding, StringLengthValidator(1, 60, Ruleset="ValAdvPositionInfo", MessageTemplate="请输入广告位名称，长度限制在60字符以内")]
        public string AdvPositionName { get; set; }
    }
}

