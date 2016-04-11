using Hishop.TransferManager;
using Ionic.Zip;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Hidistro.UI.ControlPanel.Utility;
using Hidistro.Membership.Context;

namespace Hishop.Transfers.TaobaoImporters
{
    public class Yfx1_2_from_ShangJi2_7 : ImportAdapter
    {
        private const string ProductFilename = "products.csv";
        private readonly Target _importTo;
        private readonly Target _source;
        private readonly DirectoryInfo _baseDir;
        private DirectoryInfo _workDir;
        private DirectoryInfo _productImagesDir;
        public override Target Source
        {
            get
            {
                return this._source;
            }
        }
        public override Target ImportTo
        {
            get
            {
                return this._importTo;
            }
        }
        public Yfx1_2_from_ShangJi2_7()
        {
            this._importTo = new YfxTarget("1.2");
            this._source = new SJTarget("2.7");
            string tempPath = "~/storage/data/1688/" + HiContext.Current.User.Username.ToString();
            if (!Directory.Exists(HttpContext.Current.Request.MapPath(tempPath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Request.MapPath(tempPath));
            }
            this._baseDir = new DirectoryInfo(HttpContext.Current.Request.MapPath(tempPath));
        }
        public override object[] ParseProductData(params object[] importParams)
        {


            string text = (string)importParams[0];
            HttpContext current = HttpContext.Current;
            DataTable productSet = this.GetProductSet();
            StreamReader streamReader = new StreamReader(Path.Combine(text, "products.csv"), Encoding.Unicode);
            string text2 = streamReader.ReadToEnd();
            streamReader.Close();
            text2 = text2.Substring(text2.IndexOf('\n') + 1);
            text2 = text2.Substring(text2.IndexOf('\n') + 1);
            StreamWriter streamWriter = new StreamWriter(Path.Combine(text, "products.csv"), false, Encoding.Unicode);
            streamWriter.Write(text2);
            streamWriter.Close();
            using (CsvReader csvReader = new CsvReader(new StreamReader(Path.Combine(text, "products.csv"), Encoding.Default), true, '\t'))
            {
                int num = 0;
                while (csvReader.ReadNextRecord())
                {
                    num++;
                    DataRow dataRow = productSet.NewRow();
                    new Random();
                    //dataRow["SalePrice"] = decimal.Parse(csvReader.get_Item("宝贝价格"))
                    dataRow["ProductName"] = csvReader["标题"].ToString();
                    dataRow["SalePrice"] = 0;
                    if (!string.IsNullOrEmpty(csvReader["单价"].ToString()))
                    {
                        dataRow["SalePrice"] = Convert.ToDecimal(csvReader["单价"].ToString());
                    }
                    dataRow["Num"] = 0;
                    if (!string.IsNullOrEmpty(csvReader["可售数量"].ToString()))
                    {
                        dataRow["Num"] = (dataRow["Stock"] = Convert.ToInt64(csvReader["可售数量"].ToString()));
                    }

                    //dataRow["ProductName"] = this.Trim(csvReader["宝贝名称"].ToString());
                    if (!string.IsNullOrEmpty(csvReader["产品详情"].ToString()))
                    {
                        dataRow["Description"] = this.Trim(csvReader["产品详情"].Replace("\"\"", "\"").Replace("alt=\"\"", "").Replace("alt=\"", "").Replace("alt=''", ""));
                    }
                    string text3 = this.Trim(csvReader["产品图片"].ToString());
                    dataRow["Image"] = text3;
                    if (!string.IsNullOrEmpty(text3))
                    {

                        string[] array = text3.Split(';');
                        for (int i = 0; i < array.Length; i++)
                        {

                            string str = array[i].Substring(0, array[i].IndexOf(":"));
                            string str2 = str + ".jpg";
                            if (File.Exists(Path.Combine(text + "\\products", str + ".ali")))
                            {
                                File.Copy(Path.Combine(text + "\\products", str + ".ali"), current.Request.MapPath("~/Storage/master/product/images/" + str2), true);
                                switch (i)
                                {
                                    case 0:
                                        dataRow["ImageUrl1"] = "/Storage/master/product/images/" + str2;
                                        break;
                                    case 1:
                                        dataRow["ImageUrl2"] = "/Storage/master/product/images/" + str2;
                                        break;
                                    case 2:
                                        dataRow["ImageUrl3"] = "/Storage/master/product/images/" + str2;
                                        break;
                                    case 3:
                                        dataRow["ImageUrl4"] = "/Storage/master/product/images/" + str2;
                                        break;
                                    case 4:
                                        dataRow["ImageUrl5"] = "/Storage/master/product/images/" + str2;
                                        break;
                                }
                            }
                        }
                    }

                    dataRow["Cid"] = 0;
                    if (!string.IsNullOrEmpty(csvReader["类目"].ToString()))
                    {
                        dataRow["Cid"] = Convert.ToInt64(csvReader["类目"].ToString());
                    }

                    dataRow["FreightPayer"] = ((csvReader["运费设置"].ToString() == "1") ? "seller" : "buyer");
                    dataRow["ValidThru"] = 0;
                    if (!string.IsNullOrEmpty(csvReader["有效期"].ToString()))
                    {
                        dataRow["ValidThru"] = long.Parse(csvReader["有效期"].ToString());
                    }

                    dataRow["PropertyAlias"] = csvReader["产品属性"].ToString();
                    string text4 = string.Empty;
                    string text5 = string.Empty;
                    string text6 = string.Empty;
                    string text7 = string.Empty;

                    dataRow["SkuProperties"] = csvReader["SKU属性"].ToString();
                    dataRow["SkuQuantities"] = text5;
                    dataRow["SkuPrices"] = text4;
                    dataRow["SkuOuterIds"] = text6;
                    dataRow["Image"] = text3;
                    productSet.Rows.Add(dataRow);
                }
            }
            return new object[]
			{
				productSet
			};
        }
        private DataTable GetProductSet()
        {
            return new DataTable("products")
            {
                Columns = 
				{
					new DataColumn("ProductName")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("Description")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("Image")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("ImageUrl1")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("ImageUrl2")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("ImageUrl3")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("ImageUrl4")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("ImageUrl5")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("SKU")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("Stock")
					{
						DataType = Type.GetType("System.Int32")
					},
					new DataColumn("SalePrice")
					{
						DataType = Type.GetType("System.Decimal")
					},
					new DataColumn("Weight")
					{
						DataType = Type.GetType("System.Int32")
					},
					new DataColumn("Cid")
					{
						DataType = Type.GetType("System.Int64")
					},
					new DataColumn("StuffStatus")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("Num")
					{
						DataType = Type.GetType("System.Int64")
					},
					new DataColumn("LocationState")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("LocationCity")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("FreightPayer")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("PostFee")
					{
						DataType = Type.GetType("System.Decimal")
					},
					new DataColumn("ExpressFee")
					{
						DataType = Type.GetType("System.Decimal")
					},
					new DataColumn("EMSFee")
					{
						DataType = Type.GetType("System.Decimal")
					},
					new DataColumn("HasInvoice")
					{
						DataType = Type.GetType("System.Boolean")
					},
					new DataColumn("HasWarranty")
					{
						DataType = Type.GetType("System.Boolean")
					},
					new DataColumn("HasDiscount")
					{
						DataType = Type.GetType("System.Boolean")
					},
					new DataColumn("ValidThru")
					{
						DataType = Type.GetType("System.Int64")
					},
					new DataColumn("ListTime")
					{
						DataType = Type.GetType("System.DateTime")
					},
					new DataColumn("PropertyAlias")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("InputPids")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("InputStr")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("SkuProperties")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("SkuQuantities")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("SkuPrices")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("SkuOuterIds")
					{
						DataType = Type.GetType("System.String")
					}
				}
            };
        }
        public override string PrepareDataFiles(params object[] initParams)
        {
            string text = (string)initParams[0];
            this._workDir = this._baseDir.CreateSubdirectory(Path.GetFileNameWithoutExtension(text));
            using (ZipFile zipFile = ZipFile.Read(Path.Combine(this._baseDir.FullName, text)))
            {
                foreach (ZipEntry current in zipFile)
                {
                    current.Extract(this._workDir.FullName, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            return this._workDir.FullName;
        }
        public override object[] CreateMapping(params object[] initParams)
        {
            throw new NotImplementedException();
        }
        public override object[] ParseIndexes(params object[] importParams)
        {
            throw new NotImplementedException();
        }
        private string Trim(string str)
        {
            while (str.StartsWith("\""))
            {
                str = str.Substring(1);
            }
            while (str.EndsWith("\""))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }
    }
}
