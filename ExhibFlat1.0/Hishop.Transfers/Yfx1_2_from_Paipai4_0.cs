using Hishop.TransferManager;
using Ionic.Zip;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using Hidistro.Entities.Store;
using Hidistro.Membership.Context;
using Hishop.Transfers;

namespace Hishop.Transfers.PaipaiImporters
{
    public class Yfx1_2_from_Paipai4_0 : ImportAdapter
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
        public Yfx1_2_from_Paipai4_0()
        {
            this._importTo = new YfxTarget("1.2");
            this._source = new PPTarget("4.0");
            string tempPath = "~/storage/data/paipai/" + HiContext.Current.User.Username.ToString();
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
            using (CsvReader csvReader = new CsvReader(new StreamReader(Path.Combine(text, "products.csv"), Encoding.Default), true, '\t'))
            {
                int num = 0;
                while (csvReader.ReadNextRecord())
                {
                    num++;
                    DataRow dataRow = productSet.NewRow();
                    Random random = new Random();
                    dataRow["SKU"] = string.Format("{0}{1}", string.Concat(new object[]
					{
						random.Next(9).ToString(),
						random.Next(9),
						random.Next(9),
						random.Next(9),
						random.Next(9)
					}), num);
                    dataRow["SalePrice"] = decimal.Parse(csvReader[10].ToString());
                    if (!string.IsNullOrEmpty(csvReader[6].ToString()))
                    {
                        dataRow["Weight"] = int.Parse(csvReader[6].ToString());
                    }
                    if (!string.IsNullOrEmpty(csvReader[5].ToString()))
                    {
                        dataRow["Stock"] = int.Parse(csvReader[5].ToString());
                    }
                    dataRow["ProductName"] = this.Trim(csvReader[1].ToString());
                    if (!string.IsNullOrEmpty(csvReader[30].ToString()))
                    {
                        string path = Path.Combine(text + "\\products", csvReader[30].ToString());
                        if (File.Exists(path))
                        {
                            dataRow["Description"] = File.ReadAllText(path, Encoding.GetEncoding("gb2312"));
                        }
                    }
                    string text2 = this.Substring(csvReader[25].ToString());
                    if (!string.IsNullOrEmpty(text2))
                    {
                        text2 = text2.Substring(text2.LastIndexOf("\\") + 1);
                        if (File.Exists(Path.Combine(text + "\\products", text2)))
                        {
                            File.Copy(Path.Combine(text + "\\products", text2), current.Request.MapPath("~/Storage/master/product/images/" + text2), true);
                            dataRow["ImageUrl1"] = "/Storage/master/product/images/" + text2;
                        }
                    }
                    text2 = this.Substring(csvReader[26].ToString());
                    if (!string.IsNullOrEmpty(text2))
                    {
                        text2 = text2.Substring(text2.LastIndexOf("\\") + 1);
                        if (File.Exists(Path.Combine(text + "\\products", text2)))
                        {
                            File.Copy(Path.Combine(text + "\\products", text2), current.Request.MapPath("~/Storage/master/product/images/" + text2), true);
                            dataRow["ImageUrl2"] = "/Storage/master/product/images/" + text2;
                        }
                    }
                    text2 = this.Substring(csvReader[27].ToString());
                    if (!string.IsNullOrEmpty(text2))
                    {
                        text2 = text2.Substring(text2.LastIndexOf("\\") + 1);
                        if (File.Exists(Path.Combine(text + "\\products", text2)))
                        {
                            File.Copy(Path.Combine(text + "\\products", text2), current.Request.MapPath("~/Storage/master/product/images/" + text2), true);
                            dataRow["ImageUrl3"] = "/Storage/master/product/images/" + text2;
                        }
                    }
                    text2 = this.Substring(csvReader[28].ToString());
                    if (!string.IsNullOrEmpty(text2))
                    {
                        text2 = text2.Substring(text2.LastIndexOf("\\") + 1);
                        if (File.Exists(Path.Combine(text + "\\products", text2)))
                        {
                            File.Copy(Path.Combine(text + "\\products", text2), current.Request.MapPath("~/Storage/master/product/images/" + text2), true);
                            dataRow["ImageUrl4"] = "/Storage/master/product/images/" + text2;
                        }
                    }
                    text2 = this.Substring(csvReader[29].ToString());
                    if (!string.IsNullOrEmpty(text2))
                    {
                        text2 = text2.Substring(text2.LastIndexOf("\\") + 1);
                        if (File.Exists(Path.Combine(text + "\\products", text2)))
                        {
                            File.Copy(Path.Combine(text + "\\products", text2), current.Request.MapPath("~/Storage/master/product/images/" + text2), true);
                            dataRow["ImageUrl5"] = "/Storage/master/product/images/" + text2;
                        }
                    }
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
        private string Substring(string str)
        {
            if (str.StartsWith("\""))
            {
                str = str.Substring(1);
            }
            if (str.EndsWith("\""))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
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
