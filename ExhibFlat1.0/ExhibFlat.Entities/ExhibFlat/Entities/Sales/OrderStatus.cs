
﻿namespace ExhibFlat.Entities.Sales
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public enum OrderStatus
    {
        [DescriptionAttribute("全部")]
        All = 0,
        [DescriptionAttribute("待付款")]
        WaitBuyerPay = 1,
        [DescriptionAttribute("已付款")]
        BuyerAlreadyPaid = 7,
        [DescriptionAttribute("已发货")]
        SellerAlreadySent = 3,
        [DescriptionAttribute("关闭交易")]
        Closed = 4,
        [DescriptionAttribute("已完成")]
        Finished = 5,
        [DescriptionAttribute("生产中")]
        InProduction = 6,
        [DescriptionAttribute("待发货")]
        WaitToBeSent = 8,
        [DescriptionAttribute("已取消")]
        Canceled=9,
        [DescriptionAttribute("异常订单")]
        Unusual = 10,
        [DescriptionAttribute("历史")]
        History = 0x63
    }


}


