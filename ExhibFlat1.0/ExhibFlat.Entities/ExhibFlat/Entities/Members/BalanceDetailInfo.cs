namespace ExhibFlat.Entities.Members
{
    using ExhibFlat.Components.Validation;
    using ExhibFlat.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class BalanceDetailInfo
    {
        public decimal Balance { get; set; }

        [RangeValidator(typeof(decimal), "-10000000", RangeBoundaryType.Exclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValBalanceDetail"), ValidatorComposition(CompositionType.Or, Ruleset="ValBalanceDetail", MessageTemplate="本次支出的金额，金额大小正负1000万之间"), NotNullValidator(Negated=true, Ruleset="ValBalanceDetail")]
        public decimal? Expenses { get; set; }

        [NotNullValidator(Negated=true, Ruleset="ValBalanceDetail"), ValidatorComposition(CompositionType.Or, Ruleset="ValBalanceDetail", MessageTemplate="本次收入的金额，金额大小正负1000万之间"), RangeValidator(typeof(decimal), "-10000000", RangeBoundaryType.Exclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValBalanceDetail")]
        public decimal? Income { get; set; }

        public long JournalNumber { get; set; }

        [StringLengthValidator(0, 300, Ruleset="ValBalanceDetail", MessageTemplate="备注的长度限制在300个字符以内")]
        public string Remark { get; set; }

        public DateTime TradeDate { get; set; }

        public TradeTypes TradeType { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}

