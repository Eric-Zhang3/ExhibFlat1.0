﻿namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class ShippingAddressInfo
    {
        [HtmlCoding]
        public string Address { get; set; }

        [HtmlCoding]
        public string CellPhone { get; set; }

        public int RegionId { get; set; }

        public int ShippingId { get; set; }

        [HtmlCoding]
        public string ShipTo { get; set; }

        [HtmlCoding]
        public string TelPhone { get; set; }

        public int UserId { get; set; }

        public string Zipcode { get; set; }

        public bool IsDefaultAddress { get; set; } 
    }
}

