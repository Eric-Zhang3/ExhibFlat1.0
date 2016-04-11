using Hishop.TransferManager;
using Ionic.Zip;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Hidistro.Core;
using Hidistro.Membership.Context;
using System.Data.OleDb;
namespace Hishop.Transfers.TaobaoImporters
{
    public class Yfx1_2_from_Taobao5_0 : ImportAdapter
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
        public Yfx1_2_from_Taobao5_0()
        {
            this._importTo = new YfxTarget("1.2");
            this._source = new TbTarget("5.0");
            string tempPath = "~/storage/data/taobao/" + HiContext.Current.User.Username.ToString();
            if (!Directory.Exists(HttpContext.Current.Request.MapPath(tempPath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Request.MapPath(tempPath));
            }
            this._baseDir = new DirectoryInfo(HttpContext.Current.Request.MapPath(tempPath));
        }
        
        public override object[] ParseProductData(params object[] importParams)
        {
            //图片服务器地址
            string IMAGEURL = System.Configuration.ConfigurationManager.AppSettings["ImageUrl"].ToString();
            string text = (string)importParams[0];
            HttpContext current = HttpContext.Current;
            DataTable productSet = this.GetProductSet();

            string s = Path.Combine(text, "products.csv");
            StreamReader streamReader = new StreamReader(Path.Combine(text, "products.csv"), Encoding.Default);
            string text2 = streamReader.ReadToEnd();
            streamReader.Close();
            text2 = text2.Substring(text2.IndexOf('\n') + 1);
            text2 = text2.Substring(text2.IndexOf('\n') + 1);
            /*在宝贝描述中存在“,”时，会造成数据读取错误，暂时删除*/
            //if (text2.Contains(","))
            //{
            //    text2 = text2.Replace(",", "	");
            //}  
            DataTable dttb = JHCSV.LoadCsv(Path.Combine(text, "products.csv"), Encoding.UTF8, '\t', 2);
            //StreamWriter streamWriter = new StreamWriter(Path.Combine(text, "products.csv"), false, Encoding.Default);
            //streamWriter.Write(text2);
            //streamWriter.Close();
            //using (CsvReader csvReader = new CsvReader(new StreamReader(Path.Combine(text, "products.csv"), Encoding.Default), true, '\t'))
            //{
            int num = 0;
            //while (csvReader.ReadNextRecord())
            //{
            num++;
            
            for (int i = 0; i < dttb.Rows.Count; i++)
            {
                DataRow dataRow = productSet.NewRow();
                dataRow["ProductName"] = dttb.Rows[i]["宝贝名称"].ToString().Trim();
                if (!string.IsNullOrEmpty(dttb.Rows[i]["宝贝描述"].ToString().Trim()))
                {
                    dataRow["Description"] = dttb.Rows[i]["宝贝描述"].ToString().Replace("\"\"", "\"").Replace("alt=\"\"", "").Replace("alt=\"", "").Replace("alt=''", "");
                }
                string text3 = dttb.Rows[i]["新图片"].ToString().Trim();
                dataRow["Image"] = text3;
                if (dttb.Columns.Contains("宝贝数量") && !string.IsNullOrEmpty(dttb.Rows[i]["宝贝数量"].ToString().Trim()))
                {
                    dataRow["Stock"] = dttb.Rows[i]["宝贝数量"].ToString().Trim();
                }
                if (dttb.Columns.Contains("宝贝类目") && !string.IsNullOrEmpty(dttb.Rows[i]["宝贝类目"].ToString().Trim()))
                {
                    dataRow["PCid"] = dttb.Rows[i]["宝贝类目"].ToString().Trim();
                }
                if (dttb.Columns.Contains("宝贝价格") && !string.IsNullOrEmpty(dttb.Rows[i]["宝贝价格"].ToString().Trim()))
                {
                    string ss = dttb.Rows[i]["宝贝价格"].ToString().Trim();
                    dataRow["SalePrice"] = dttb.Rows[i]["宝贝价格"].ToString().Trim();
                }
                if (dttb.Columns.Contains("物流重量")&& !string.IsNullOrEmpty(dttb.Rows[i]["物流重量"].ToString().Trim()))
                {
                    dataRow["Weight"] = dttb.Rows[i]["物流重量"].ToString().Trim();
                }
                if (dttb.Columns.Contains("销售属性组合") && !string.IsNullOrEmpty(dttb.Rows[i]["销售属性组合"].ToString().Trim()))
                {
                    dataRow["SkuProperties"] = dttb.Rows[i]["销售属性组合"].ToString().Trim();
                }
                if (dttb.Columns.Contains("宝贝属性") && !string.IsNullOrEmpty(dttb.Rows[i]["宝贝属性"].ToString().Trim()))
                {
                    dataRow["cateProps"] = dttb.Rows[i]["宝贝属性"].ToString().Trim();
                }
                if (dttb.Columns.Contains("省") && !string.IsNullOrEmpty(dttb.Rows[i]["省"].ToString().Trim()))
                {
                    dataRow["LocationState"] = dttb.Rows[i]["省"].ToString().Trim();
                }
                if (dttb.Columns.Contains("城市") && !string.IsNullOrEmpty(dttb.Rows[i]["城市"].ToString().Trim()))
                {
                    dataRow["LocationCity"] = dttb.Rows[i]["城市"].ToString().Trim();
                }
                if (dttb.Columns.Contains("用户输入ID串") && !string.IsNullOrEmpty(dttb.Rows[i]["用户输入ID串"].ToString().Trim()))
                {
                    dataRow["inputPids"] = dttb.Rows[i]["用户输入ID串"].ToString().Trim();
                }
                if (dttb.Columns.Contains("用户输入名-值对") && !string.IsNullOrEmpty(dttb.Rows[i]["用户输入名-值对"].ToString().Trim()))
                {
                    dataRow["inputValues"] = dttb.Rows[i]["用户输入名-值对"].ToString().Trim();
                }
                if (dttb.Columns.Contains("销售属性别名") && !string.IsNullOrEmpty(dttb.Rows[i]["销售属性别名"].ToString().Trim()))
                {
                    dataRow["propAlias"] = dttb.Rows[i]["销售属性别名"].ToString().Trim();
                }
                if (dttb.Columns.Contains("属性值备注") && !string.IsNullOrEmpty(dttb.Rows[i]["属性值备注"].ToString().Trim()))
                {
                    dataRow["cpv_memo"] = dttb.Rows[i]["属性值备注"].ToString().Trim();
                }
                if (dttb.Columns.Contains("自定义属性值") && !string.IsNullOrEmpty(dttb.Rows[i]["自定义属性值"].ToString().Trim()))
                {
                    dataRow["input_custom_cpv"] = dttb.Rows[i]["自定义属性值"].ToString().Trim();
                }
                if (!string.IsNullOrEmpty(text3))
                {
                    string[] array = text3.Split(';');
                    for (int j = 0; j < array.Length - 1; j++)
                    {

                        string str = System.Guid.NewGuid().ToString(); //array[j].Substring(0, array[j].IndexOf(":"));
                        string str2 = str + ".jpg";
                        string str3 = array[j].Substring(0, array[j].Length - 1);
                        string str4 = str + ".tbi";

                        string[] strTemp = str3.Split(':');

                        Regex re = new Regex("^(http|https)://");
                        if (strTemp[0] != "" && re.IsMatch(strTemp[0]))
                            Hidistro.Core.ResourcesHelper.GetPictures(strTemp[0], current.Request.MapPath("~/Storage/data/taobao/" + HiContext.Current.User.Username.ToString() + "/products/" + str4));
                        else
                        {
                            Hidistro.Core.ResourcesHelper.GetPictures(current.Request.MapPath("~/Storage/data/taobao/" + HiContext.Current.User.Username.ToString() + "/products/products/" + strTemp[0] + ".tbi"), current.Request.MapPath("~/Storage/data/taobao/" + HiContext.Current.User.Username.ToString() + "/products/" + str4));
                        }
                        if (File.Exists(Path.Combine(text, str + ".tbi")))
                        {
                            File.Copy(Path.Combine(text, str + ".tbi"), current.Request.MapPath("~/Storage/master/product/images/" + str2), true);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs40/40_" + str2), 40, 40);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs60/60_" + str2), 60, 60);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs100/100_" + str2), 100, 100);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs160/160_" + str2), 160, 160);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs180/180_" + str2), 180, 180);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs220/220_" + str2), 200, 200);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs310/310_" + str2), 310, 310);
                            Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs410/410_" + str2), 410, 410);

                            switch (j)
                            {
                                case 0:
                                    dataRow["ImageUrl1"] = IMAGEURL + "/Storage/master/product/images/" + str2;
                                    // Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs40/40_" + str2), 40, 40);
                                    dataRow["ThumbnailUrl40"] = IMAGEURL + "/Storage/master/product/thumbs40/40_" + str2;
                                    // Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs60/60_" + str2), 60, 60);
                                    dataRow["ThumbnailUrl60"] = IMAGEURL + "/Storage/master/product/thumbs60/60_" + str2;
                                    // Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs100/100_" + str2), 100, 100);
                                    dataRow["ThumbnailUrl100"] = IMAGEURL + "/Storage/master/product/thumbs100/100_" + str2;
                                    //  Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs160/160_" + str2), 160, 160);
                                    dataRow["ThumbnailUrl160"] = IMAGEURL + "/Storage/master/product/thumbs160/160_" + str2;
                                    //  Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs180/180_" + str2), 180, 180);
                                    dataRow["ThumbnailUrl180"] = IMAGEURL + "/Storage/master/product/thumbs180/180_" + str2;
                                    //  Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs220/220_" + str2), 220, 220);
                                    dataRow["ThumbnailUrl220"] = IMAGEURL + "/Storage/master/product/thumbs220/220_" + str2;
                                    // Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs310/310_" + str2), 310, 310);
                                    dataRow["ThumbnailUrl310"] = IMAGEURL + "/Storage/master/product/thumbs310/310_" + str2;
                                    //  Hidistro.Core.ResourcesHelper.CreateThumbnail(current.Request.MapPath("~/Storage/master/product/images/" + str2), current.Request.MapPath("~/Storage/master/product/thumbs410/410_" + str2), 410, 410);
                                    dataRow["ThumbnailUrl410"] = IMAGEURL + "/Storage/master/product/thumbs410/410_" + str2;
                                    break;
                                case 1:
                                    dataRow["ImageUrl2"] = IMAGEURL + "/Storage/master/product/images/" + str2;
                                    break;
                                case 2:
                                    dataRow["ImageUrl3"] = IMAGEURL + "/Storage/master/product/images/" + str2;
                                    break;
                                case 3:
                                    dataRow["ImageUrl4"] = IMAGEURL + "/Storage/master/product/images/" + str2;
                                    break;
                                case 4:
                                    dataRow["ImageUrl5"] = IMAGEURL + "/Storage/master/product/images/" + str2;
                                    break;
                            }

                        }
                       
                    }
                }
                if (dataRow["ProductName"] != null && dataRow["ProductName"].ToString() != "")
                    productSet.Rows.Add(dataRow);
                
            }
            string result;
            using (StringWriter sw = new StringWriter())
            {
                productSet.WriteXml(sw);
                result = sw.ToString();
            }
            //log.blog = true;
            //log.GetInstance().writeLog(result);
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
                    new DataColumn("ThumbnailUrl40")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("ThumbnailUrl60")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("ThumbnailUrl100")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("ThumbnailUrl160")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("ThumbnailUrl180")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("ThumbnailUrl220")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("ThumbnailUrl310")
					{
						DataType = Type.GetType("System.String")

					},
                    new DataColumn("ThumbnailUrl410")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("SKU")
					{
						DataType = Type.GetType("System.String")
					},
                    new DataColumn("cateProps")
                    {
                        DataType=Type.GetType("System.String")
                    },
					new DataColumn("Stock")
					{
						DataType = Type.GetType("System.Int32")
					},
					new DataColumn("SalePrice")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("Weight")
					{
						DataType = Type.GetType("System.String")
					},
					new DataColumn("Cid")
					{
						DataType = Type.GetType("System.Int64")
					},
                    new DataColumn("PCid")
                    {
                        DataType=Type.GetType("System.Int64")
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
					},
                     //用户输入名-值对
                    new DataColumn("inputValues")
					{
						DataType = Type.GetType("System.String")
					},
                    //销售属性别名
                    new DataColumn("propAlias")
					{
						DataType = Type.GetType("System.String")
					},
                    //属性值备注
                    new DataColumn("cpv_memo")
					{
						DataType = Type.GetType("System.String")
					},
                    //自定义属性值
                    new DataColumn("input_custom_cpv")
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
