using Hishop.TransferManager;
using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using Hidistro.Membership.Context;
namespace Hishop.Transfers.TaobaoExporters
{
    public class Yfx1_2_to_ShangJi_2_7 : ExportAdapter
    {
        private const string ExportVersion = "2.7";
        private const string ProductFilename = "products.csv";
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _zipFilename;
        private readonly Target _exportTo;
        private readonly Target _source;
        private readonly DirectoryInfo _baseDir;
        private readonly DataSet _exportData;
        private readonly bool _includeImages;
        private readonly bool _includeCostPrice;
        private readonly bool _includeStock;
        private readonly string _url;
        private readonly string _applicationPath;
        private readonly string _flag;
        private DirectoryInfo _workDir;
        private DirectoryInfo _productImagesDir;
        public override Target ExportTo
        {
            get
            {
                return this._exportTo;
            }
        }
        public override Target Source
        {
            get
            {
                return this._source;
            }
        }
        public Yfx1_2_to_ShangJi_2_7()
        {
            this._exportTo = new SJTarget("2.7");
            this._source = new YfxTarget("1.2");
        }
        public Yfx1_2_to_ShangJi_2_7(params object[] exportParams)
            : this()
        {
            this._exportData = (DataSet)exportParams[0];
            this._includeCostPrice = (bool)exportParams[1];
            this._includeStock = (bool)exportParams[2];
            this._includeImages = (bool)exportParams[3];
            this._url = (string)exportParams[4];
            this._applicationPath = (string)exportParams[5];
            this._baseDir = new DirectoryInfo(HttpContext.Current.Request.MapPath("~/storage/data/1688"));
            this._flag = DateTime.Now.ToString("yyyyMMddHHmmss");
            this._zipFilename = string.Format("ali.{0}.{1}.zip", "2.7", this._flag);
        }
        public override void DoExport()
        {
            this._workDir = this._baseDir.CreateSubdirectory(this._flag);
            this._productImagesDir = this._workDir.CreateSubdirectory("products");
            string text = Path.Combine(this._workDir.FullName, "products.csv");
            using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
            {
                string productCSV = this.GetProductCSV();
                UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
                int byteCount = unicodeEncoding.GetByteCount(productCSV);
                byte[] preamble = unicodeEncoding.GetPreamble();
                byte[] array = new byte[preamble.Length + byteCount];
                Buffer.BlockCopy(preamble, 0, array, 0, preamble.Length);
                unicodeEncoding.GetBytes(productCSV.ToCharArray(), 0, productCSV.Length, array, preamble.Length);
                fileStream.Write(array, 0, array.Length);
            }
            using (ZipFile zipFile = new ZipFile())
            {
                zipFile.CompressionLevel = CompressionLevel.Default;
                zipFile.AddFile(text, "");
                zipFile.AddDirectory(this._productImagesDir.FullName, this._productImagesDir.Name);
                HttpResponse response = HttpContext.Current.Response;
                response.ContentType = "application/x-zip-compressed";
                response.ContentEncoding = this._encoding;
                response.AddHeader("Content-Disposition", "attachment; filename=" + this._zipFilename);
                response.Clear();
                zipFile.Save(response.OutputStream);
                this._workDir.Delete(true);
                response.Flush();
                response.Close();
            }
        }
        private string GetProductCSV()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string format = "\"{0}\"\t{1}\t\"{2}\"\t{3}\t\"{4}\"\t\"{5}\"\t{6}\t{7}\t{8}\t\r\n";
            stringBuilder.Append("version 1.00\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\r\n");
            stringBuilder.Append("productname\tcid\tprice\tnum\tvalid_thru\tfreight_payer");
            stringBuilder.Append("\tdescription\tpropAlias");
            stringBuilder.Append("\tpicture\t\r\n");
            stringBuilder.Append("标题\t类目\t单价\t可售数量\t有效期\t运费设置\t产品详情");
            stringBuilder.Append("\t产品属性\t产品图片");
            stringBuilder.Append("\r\n");
            foreach (DataRow dataRow in this._exportData.Tables["products"].Rows)
            {
                string text;
                if (dataRow["Description"] != DBNull.Value)
                {
                    text = this.Trim((string)dataRow["Description"]);
                    text = text.Replace(string.Format("src=\"{0}/Storage/master/gallery", this._applicationPath), string.Format("src=\"{0}/Storage/master/gallery", this._url));
                }
                else
                {
                    text = string.Empty;
                }
                if (dataRow["ShortDescription"] != DBNull.Value)
                {
                    string text2 = this.Trim(Convert.ToString(dataRow["ShortDescription"]).Trim());
                    if (!string.IsNullOrEmpty(text2) && text2.Length > 0)
                    {
                        text = text2 + "<br/>" + text;
                    }
                }
                text = text.Replace("\r\n", "");
                text = text.Replace("\r", "").Replace("\n", "");
                text = text.Replace("\"", "\"\"");
                string text55 = string.Empty;
                if (dataRow["Image"] != DBNull.Value)
                {
                    text55 = (string)dataRow["Image"];
                }
                string text3=string.Empty;

                if (dataRow["ImageUrl1"] != DBNull.Value)
                {
                    //text3 = this.CopyImage((string)dataRow["ImageUrl1"], 1);
                    //text3=text3.Split(new char[] { ';' }) + text33 + ";";
                    text3 += this.CopyImage((string)dataRow["ImageUrl1"], 1);
                }
                if (dataRow["ImageUrl2"] != DBNull.Value)
                {
                    //   text3 =text3+this.CopyImage((string)dataRow["ImageUrl2"], 2).Split(new char[]{';'})+text33+";";
                    text3 += this.CopyImage((string)dataRow["ImageUrl2"], 2);
                }
                if (dataRow["ImageUrl3"] != DBNull.Value)
                {
                    //text3 = text3 + this.CopyImage((string)dataRow["ImageUrl2"], 3).Split(new char[] { ';' }) + text33 + ";";
                    text3 += this.CopyImage((string)dataRow["ImageUrl3"], 3);
                }
                if (dataRow["ImageUrl4"] != DBNull.Value)
                {
                    //text3 = text3 + this.CopyImage((string)dataRow["ImageUrl2"], 4).Split(new char[] { ';' }) + text33 + ";";
                    text3 += this.CopyImage((string)dataRow["ImageUrl4"], 4);
                }
                if (dataRow["ImageUrl5"] != DBNull.Value)
                {
                    //text3 = text3 + this.CopyImage((string)dataRow["ImageUrl2"], 5).Split(new char[] { ';' }) + text33 + ";";
                    text3 += this.CopyImage((string)dataRow["ImageUrl5"], 5);
                }
               // DataRow[] array = this._exportData.Tables["skus"].Select("ProductId=" + dataRow["ProductId"].ToString(), "SalePrice desc");
                string text4 = "";
                int num = 0;
                
                string text5 = "1";
                string text6 = "0";
                string text7 = "0";
                string text8 = "0";
                string text9 = "";
                string text10 = "";
                string text11 = "";
                string text12 = Convert.ToString(dataRow["productcode"]);
                string text13 = "";
                string text14 = "";
                string text15 = "";
                string text16 = "";
                string text17 = "";
                string text18 = "";
                string text19 = "";
                string text20 = "";
                string text26="";
                DataRow[] array2 = this._exportData.Tables["TaobaoSku"].Select("ProductId=" + dataRow["Productid"].ToString());
                if (array2.Length > 0)
                {
                    if (this._includeStock)
                    {
                        text4 = Convert.ToString(array2[0]["Num"]);
                    }
                    text15 = Convert.ToString(array2[0]["LocationState"]);
                    text16 = Convert.ToString(array2[0]["LocationCity"]);
                    text17 = ((Convert.ToString(array2[0]["HasInvoice"]).ToLower() == "true") ? "1" : "0");
                    text18 = ((Convert.ToString(array2[0]["HasWarranty"]).ToLower() == "true") ? "1" : "0");
                    text19 = ((Convert.ToString(array2[0]["HasDiscount"]).ToLower() == "true") ? "1" : "0");
                    text20 = ((array2[0]["StuffStatus"].ToString() == "new") ? "1" : "0");
                    if (Convert.ToString(array2[0]["FreightPayer"]) == "buyer")
                    {
                        text5 = "2";
                        text6 = Convert.ToString(array2[0]["PostFee"]);
                        text7 = Convert.ToString(array2[0]["ExpressFee"]);
                        text8 = Convert.ToString(array2[0]["EMSFee"]);
                    }
                    text10 = Convert.ToString(array2[0]["Cid"]);
                    text13 = Convert.ToString(array2[0]["PropertyAlias"]);
                    text9 = Convert.ToString(array2[0]["inputpids"]);
                    text11 = Convert.ToString(array2[0]["inputstr"]);
                    string text21 = Convert.ToString(array2[0]["SkuQuantities"]);
                    string text22 = Convert.ToString(array2[0]["skuPrices"]);
                    string text23 = Convert.ToString(array2[0]["SkuProperties"]);
                    string text24 = Convert.ToString(array2[0]["SkuOuterIds"]);
                     text26 = Convert.ToString(array2[0]["ValidThru"]);
                    if (!string.IsNullOrEmpty(text21))
                    {
                        string[] array3 = text21.Split(new char[]
						{
							','
						});
                        string[] array4 = text22.Split(new char[]
						{
							','
						});
                        string[] array5 = text24.Split(new char[]
						{
							','
						});
                        string[] array6 = text23.Split(new char[]
						{
							','
						});
                        for (int i = 0; i < array3.Length; i++)
                        {
                            string text25 = text14;
                            text14 = string.Concat(new string[]
							{
								text25,
								array4[i],
								":",
								array3[i],
								":",
								array5[i],
								":",
								array6[i],
								";"
							});
                        }
                    }
                }
                //else
                //{
                //    if (this._includeStock)
                //    {
                //        DataRow[] array7 = array;
                //        for (int j = 0; j < array7.Length; j++)
                //        {
                //            DataRow dataRow2 = array7[j];
                //            num += (int)dataRow2["Stock"];
                //        }
                //        text4 = num.ToString();
                //    }
                //}
                 string[] array31 = text3.Split(new char[]
						{
							','
						});
                stringBuilder.AppendFormat(format, new object[]
				{
					this.Trim(Convert.ToString(dataRow["ProductName"])),
					text10,
                    Convert.ToDecimal(dataRow["LowestSalePrice"]),
                    text4,
                    text26,
                    text5,
                    text,
                    text13,
                   text55,
					
				});
            }
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).ToString();
        }
        private string CopyImage(string imageUrl, int index)
        {
            string text = string.Empty;
            if (!imageUrl.StartsWith("http://"))
            {
                imageUrl = this.Trim(imageUrl);
                 string text2 = HttpContext.Current.Request.MapPath("~" + imageUrl);
                if (File.Exists(text2))
                {
                    
                    FileInfo fileInfo = new FileInfo(text2);
                    string text3 = fileInfo.Name.ToLower();
                    if (text3.EndsWith(".jpg") || text3.EndsWith(".gif") || text3.EndsWith(".jpeg") || text3.EndsWith(".png") || text3.EndsWith(".bmp"))
                    {
                        text3 = text3.Replace(fileInfo.Extension.ToLower(), ".ali");
                        fileInfo.CopyTo(Path.Combine(this._productImagesDir.FullName, text3), true);
                        text += text3.Replace(".ali", string.Format(":1:{0}:|;", index - 1));
                    }
                }
            }
            return text;
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

        public override void DoExport(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
