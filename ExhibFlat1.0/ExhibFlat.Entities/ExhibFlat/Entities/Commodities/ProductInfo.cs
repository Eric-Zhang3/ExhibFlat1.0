namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ProductInfo
    {
        private SKUItem defaultSku;
        private Dictionary<string, SKUItem> skus;

        public DateTime AddedDate { get; set; }

        public int AlertStock
        {
            get
            {
                return this.DefaultSku.AlertStock;
            }
        }

        public int? BrandId { get; set; }

        public int CategoryId { get; set; }

        public decimal CostPrice
        {
            get
            {
                return this.DefaultSku.CostPrice;
            }
        }

        public SKUItem DefaultSku
        {
            get
            {
                return (this.defaultSku ?? (this.defaultSku = this.Skus.Values.First<SKUItem>()));
            }
        }
        public Guid SupplierId { get; set; }

        public int SupplierUserId { get; set; }
        public string SupplierName { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; }

        public int DisplaySequence { get; set; }

        public string ExtendCategoryPath { get; set; }

        public bool HasSKU { get; set; }

        public bool HasCommission { get; set; }

        public string CommissionPath { get; set; }

        public string Image { get; set; }

        public string ImageUrl1 { get; set; }

        public string ImageUrl2 { get; set; }

        public string ImageUrl3 { get; set; }

        public string ImageUrl4 { get; set; }

        public string ImageUrl5 { get; set; }

        public int LineId { get; set; }

        public decimal LowestSalePrice { get; set; }

        public string MainCategoryPath { get; set; }

        public decimal? MarketPrice { get; set; }

        public decimal MaxSalePrice
        {
            get
            {
                decimal[] maxSalePrice = new decimal[1];
                foreach (SKUItem item in from sku in this.Skus.Values
                    where sku.SalePrice > maxSalePrice[0]
                    select sku)
                {
                    maxSalePrice[0] = item.SalePrice;
                }
                return maxSalePrice[0];
            }
        }

        [HtmlCoding]
        public string MetaDescription { get; set; }

        [HtmlCoding]
        public string MetaKeywords { get; set; }

        public decimal MinSalePrice
        {
            get
            {
                decimal[] minSalePrice = new decimal[] { 79228162514264337593543950335M };
                foreach (SKUItem item in from sku in this.Skus.Values
                    where sku.SalePrice < minSalePrice[0]
                    select sku)
                {
                    minSalePrice[0] = item.SalePrice;
                }
                return minSalePrice[0];
            }
        }

        public ExhibFlat.Entities.Commodities.PenetrationStatus PenetrationStatus { get; set; }

        public string ProductCode { get; set; }

        public int ProductId { get; set; }

        [HtmlCoding]
        public string ProductName { get; set; }

        public string ProductShortName { get; set; }

        public string ADImg { get; set; }

        public string ProductSubName { get; set; }

        public string ProductBarCode { get; set; }
        public decimal PurchasePrice
        {
            get
            {
                return this.DefaultSku.PurchasePrice;
            }
        }

        public int SaleCounts { get; set; }

        public ProductSaleStatus SaleStatus { get; set; }

        [HtmlCoding]
        public string ShortDescription { get; set; }

        public int ShowSaleCounts { get; set; }

        public string SKU
        {
            get
            {
                return this.DefaultSku.SKU;
            }
        }

        public string SkuId
        {
            get
            {
                return this.DefaultSku.SkuId;
            }
        }

        public Dictionary<string, SKUItem> Skus
        {
            get
            {
                return (this.skus ?? (this.skus = new Dictionary<string, SKUItem>()));
            }
        }

        public int Stock
        {
            get
            {
                return this.Skus.Values.Sum<SKUItem>(sku => sku.Stock);
            }
        }
        public decimal OriginalPrice { get; set; }
        public string ThumbnailUrl100 { get; set; }

        public string ThumbnailUrl160 { get; set; }

        public string ThumbnailUrl180 { get; set; }

        public string ThumbnailUrl220 { get; set; }

        public string ThumbnailUrl310 { get; set; }

        public string ThumbnailUrl40 { get; set; }

        public string ThumbnailUrl410 { get; set; }

        public string ThumbnailUrl60 { get; set; }

        [HtmlCoding]
        public string Title { get; set; }

        public int? TypeId { get; set; }

        public string Unit { get; set; }

        public int VistiCounts { get; set; }

        public int Weight
        {
            get
            {
                return this.DefaultSku.Weight;
            }
        }
        /// <summary>
        /// 发货方
        /// </summary>
        public string Sipping{get;set;}
        /// <summary>
        /// 配送方式
        /// </summary>
        public string DistributeMode{get;set;}
        /// <summary>
        /// 是否定制
        /// </summary>
        public bool IsCustomize{get;set;}
        /// <summary>
        /// 定制路径
        /// </summary>
        public string CustomizeURL { get; set; }

        public string ProductAutoCode { get; set; }
    }
}

