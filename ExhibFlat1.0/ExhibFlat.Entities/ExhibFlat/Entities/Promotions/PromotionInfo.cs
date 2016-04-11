namespace ExhibFlat.Entities.Promotions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PromotionInfo
    {
        public int ActivityId { get; set; }

        public decimal Condition { get; set; }

        public string Description { get; set; }

        public decimal DiscountValue { get; set; }

        public DateTime EndDate { get; set; }

        private IList<int> memberGradeIds { get; set; }

        public IList<int> MemberGradeIds
        {
            get
            {
                if (this.memberGradeIds == null)
                {
                    this.memberGradeIds = new List<int>();
                }
                return this.memberGradeIds;
            }
            set
            {
                this.memberGradeIds = value;
            }
        }

        public string Name { get; set; }

        public ExhibFlat.Entities.Promotions.PromoteType PromoteType { get; set; }

        public DateTime StartDate { get; set; }
    }
}

