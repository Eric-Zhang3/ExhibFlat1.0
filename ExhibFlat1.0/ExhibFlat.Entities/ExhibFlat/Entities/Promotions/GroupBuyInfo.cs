﻿namespace ExhibFlat.Entities.Promotions
{
    using ExhibFlat.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GroupBuyInfo
    {
        private IList<GropBuyConditionInfo> groupBuyConditions;

        [HtmlCoding]
        public string Content { get; set; }

        public DateTime EndDate { get; set; }

        public IList<GropBuyConditionInfo> GroupBuyConditions
        {
            get
            {
                if (this.groupBuyConditions == null)
                {
                    this.groupBuyConditions = new List<GropBuyConditionInfo>();
                }
                return this.groupBuyConditions;
            }
        }

        public int GroupBuyId { get; set; }

        public int MaxCount { get; set; }

        public decimal NeedPrice { get; set; }

        public int ProductId { get; set; }

        public DateTime StartDate { get; set; }

        public GroupBuyStatus Status { get; set; }
    }
}

