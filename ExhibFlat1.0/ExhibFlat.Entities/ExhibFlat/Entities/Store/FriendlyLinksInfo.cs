namespace ExhibFlat.Entities.Store
{
    using ExhibFlat.Components.Validation;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class FriendlyLinksInfo
    {
        public int DisplaySequence { get; set; }

        public string ImageUrl { get; set; }

        public int? LinkId { get; set; }

        [IgnoreNulls, StringLengthValidator(0, Ruleset="ValFriendlyLinksInfo"), RegexValidator(@"^(http://).*[\.]+.*", Ruleset="ValFriendlyLinksInfo"), ValidatorComposition(CompositionType.Or, Ruleset="ValFriendlyLinksInfo", MessageTemplate="网站地址必须为有效格式")]
        public string LinkUrl { get; set; }

        [StringLengthValidator(0, 60, Ruleset="ValFriendlyLinksInfo", MessageTemplate="网站名称长度限制在60个字符以内")]
        public string Title { get; set; }

        public bool Visible { get; set; }
    }
}

