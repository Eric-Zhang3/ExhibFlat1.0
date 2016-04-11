using Hishop.TransferManager;
using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using Hidistro.Membership.Context;
using System.Web.UI.WebControls;
using System.Net;
namespace Hishop.Transfers.TaobaoExporters
{
    public class Yfx1_2_to_Taobao5_0 : ExportAdapter
    {
        private const string ExportVersion = "5.0";
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
        public Yfx1_2_to_Taobao5_0()
        {
            this._exportTo = new TbTarget("5.0");
            this._source = new YfxTarget("1.2");
        }
        public Yfx1_2_to_Taobao5_0(params object[] exportParams)
            : this()
        {
            this._exportData = (DataSet)exportParams[0];
            this._includeCostPrice = (bool)exportParams[1];
            this._includeStock = (bool)exportParams[2];
            this._includeImages = (bool)exportParams[3];
            this._url = (string)exportParams[4];
            this._applicationPath = (string)exportParams[5];
            this._baseDir = new DirectoryInfo(HttpContext.Current.Request.MapPath("~/storage/data/taobao"));
            this._flag = DateTime.Now.ToString("yyyyMMddHHmmss");
            this._zipFilename = string.Format("taobao.{0}.{1}.zip", "5.0", this._flag);
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
                // response.ContentType = "application/x-zip-compressed";
                response.ContentType = "application/octet-stream";
                response.ContentEncoding = this._encoding;
                response.AddHeader("Content-Disposition", "attachment; filename=" + this._zipFilename);
                System.IO.MemoryStream ms = new MemoryStream();
                //response.Clear();
                zipFile.Save(ms);
                //zipFile.Save(response.OutputStream);
                this._workDir.Delete(true);
                response.BinaryWrite(ms.ToArray());
                response.Flush();
                response.End();
                response.Close();
            }
        }


        public override void DoExport(string filename)
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
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                System.IO.Stream fs = new System.IO.FileStream(filename, FileMode.OpenOrCreate);
                zipFile.Save(fs);
                fs.Close();
            }
        }

        string text100 = "";
        private string GetProductCSV()
        {
            StringBuilder stringBuilder = new StringBuilder();
            //string format = "\"{0}\"\t{1}\t\"{2}\"\t{3}\t\"{4}\"\t{5}\t\"{6}\"\t{7}\t\"{8}\"\t{9}\t\"{10}\"\t{11}\t\"{12}\"\t{13}\t\"{14}\"\t{15}\t\"{16}\"\t{17}\t\"{18}\"\t{19}\t\"{20}\"\t{21}\t\"{22}\"\t{23}\t\"{24}\"\t{25}\t\"{26}\"\t{27}\t\"{28}\"\t{29}\t\"{30}\"\t{31}\t\"{32}\"\t{33}\t\"{34}\"\t{35}\t\"{36}\"\t{37}\t\"{38}\"\t{39}\t\"{40}\"\t{41}\t\"{42}\"\t{43}\t\"{44}\"\t{45}\t\"{46}\"\t{47}\t\"{48}\"\t{49}\t\"{50}\"\t{51}\t\"{52}\"\t{53}\t\"{54}\"\t{55}\t\"{56}\"\t{57}\t\r\n";
            string format = "\"{0}\"\t{1}\t\"{2}\"\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}\t{24}\t{25}\t{26}\t{27}\t{28}\t{29}\t{30}\t{31}\t{32}\t{33}\t{34}\t{35}\t{36}\t{37}\t{38}\t{39}\t{40}\t{41}\t{42}\t{43}\t{44}\t{45}\t{46}\t{47}\t{48}\t{49}\t{50}\t{51}\t{52}\t{53}\t{54}\t{55}\t{56}\t{57}\t{58}\t{59}\t\r\n";
            stringBuilder.Append("version 1.00\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\r\n");
            stringBuilder.Append("title\tcid\tdescription\tseller_cids\tstuff_status\tlocation_state\tlocation_city\titem_type\tprice\tauction_increment\tnum\tvalid_thru\tfreight_payer\tpost_fee\tems_fee\texpress_fee\thas_invoice\thas_warranty\tapprove_status\thas_showcase\tlist_time\tcateProps\tpostage_id\thas_discount\tmodified\tupload_fail_msg\tpicture_status\tauction_point\tpicture\tvideo\tskuProps\tinputPids\tinputValues\touter_id\tpropAlias\tauto_fill\tnum_id\tlocal_cid\tnavigation_type\tuser_name\tsyncStatus\tis_lighting_consigment\tis_xinpin\tfoodparam\tfeatures\tbuyareatype\tglobal_stock_type\tglobal_stock_country\tsub_stock_type\titem_size\titem_weight\tsell_promise\tcustom_design_flag\twireless_desc\tbarcode\tsku_barcode\tnewprepay\tsubtitle\tcpv_memo\tinput_custom_cpv\t\r\n");
            stringBuilder.Append("宝贝名称\t宝贝类目\t宝贝描述\t店铺类目\t新旧程度\t省\t城市\t出售方式\t宝贝价格\t加价幅度\t宝贝数量\t有效期\t运费承担\t平邮\tEMS\t快递\t发票\t保修\t放入仓库\t橱窗推荐\t开始时间\t宝贝属性\t邮费模板\t会员打折\t修改时间\t上传状态\t图片状态\t返点比例\t新图片\t视频\t销售属性组合\t用户输入ID串\t用户输入名-值对\t商家编码\t销售属性别名\t代充类型\t数字ID\t本地ID\t宝贝分类\t用户名称\t宝贝状态\t闪电发货\t新品\t食品专项\t尺码库\t采购地\t库存类型\t国家地区\t库存计数\t物流体积\t物流重量\t退换货承诺\t定制工具\t无线详情\t商品条形码\tsku 条形码\t7天退货\t宝贝卖点\t属性值备注\t自定义属性值\t\r\n");

            if (this._exportData.Tables["products"].Rows != null)
            {

                foreach (DataRow dataRow in this._exportData.Tables["products"].Rows)
                {
                    string text;
                    string text36 = "";
                    if (dataRow["ProductCode"] != DBNull.Value)
                    {
                        text36 = dataRow["ProductCode"].ToString();
                    }
                    if (dataRow["Description"] != DBNull.Value)
                    {
                        text = this.Trim((string)dataRow["Description"]);
                        // text = text.Replace(string.Format("src=\"{0}/Storage/master/gallery", this._applicationPath), string.Format("src=\"{0}/Storage/master/gallery", this._url));

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
                    if (text.Contains("\r\n"))
                    {
                        text = text.Replace("\r\n", "");
                    }
                    if (text.Contains("\r"))
                    {
                        text = text.Replace("\r", "");
                    }
                    if (text.Contains("\n"))
                    {
                        text = text.Replace("\n", "");
                    }
                    if (text.Contains("\""))
                    {
                        text = text.Replace("\"", "\"\"");
                    }
                    //text = text.Replace("\r\n", "");
                    //text = text.Replace("\r", "").Replace("\n", "");
                    //text = text.Replace("\"", "\"\"");
                    string text55 = string.Empty;
                    if (dataRow["Image"] != DBNull.Value)
                    {
                        text55 = (string)dataRow["Image"];
                    }
                    string text3 = string.Empty;
                    if (!string.IsNullOrEmpty(dataRow["ImageUrl1"].ToString()))
                    {
                        string text111 = dataRow["ImageUrl1"].ToString();
                        string text110 = "";
                        for (int i = text111.Length - 1; i > 0; i--)
                        {
                            if (text111[i] == '/')
                            {
                                text110 = text111.Substring(i + 1, text111.Length - 1 - i);
                                break;
                            }
                        }
                        string text113 = text110.ToLower();
                        string text112 = "";
                        string text114 = "";
                        for (int k = text113.Length - 1; k > 0; k--)
                        {
                            if (text113[k] == '.')
                            {
                                text112 = text113.Substring(k, text113.Length - k);
                                break;
                            }
                        }

                        text114 = text113.Replace(text112, ".tbi");
                        //text100 = text114;
                        ////    text100 = Path.Combine(this._productImagesDir.FullName, text3);
                        text3 += text114.Replace(".tbi", string.Format(":1:{0}:|;", 0));
                        ////}
                        try
                        {
                            WebRequest request;
                            WebResponse response;
                            request = WebRequest.Create(text111);
                            response = request.GetResponse();
                            Stream reader = response.GetResponseStream();
                            string path = Path.Combine(this._productImagesDir.FullName, text114);
                            FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                            byte[] buff = new byte[512];
                            int c = 0; //实际读取的字节数
                            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                            {
                                writer.Write(buff, 0, c);
                            }

                            writer.Close();
                            writer.Dispose();
                            reader.Close();
                            reader.Dispose();
                            response.Close();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (!string.IsNullOrEmpty(dataRow["ImageUrl2"].ToString()))
                    {
                        string text111 = dataRow["ImageUrl2"].ToString();
                        string text110 = "";
                        for (int i = text111.Length - 1; i > 0; i--)
                        {
                            if (text111[i] == '/')
                            {
                                text110 = text111.Substring(i + 1, text111.Length - 1 - i);
                                break;
                            }
                        }
                        string text113 = text110.ToLower();
                        string text112 = "";
                        string text114 = "";
                        for (int k = text113.Length - 1; k > 0; k--)
                        {
                            if (text113[k] == '.')
                            {
                                text112 = text113.Substring(k, text113.Length - k);
                                break;
                            }
                        }

                        text114 = text113.Replace(text112, ".tbi");
                        //text100 = text114;
                        ////    text100 = Path.Combine(this._productImagesDir.FullName, text3);
                        text3 += text114.Replace(".tbi", string.Format(":1:{0}:|;", 1));
                        ////}

                        WebRequest request = WebRequest.Create(text111);
                        WebResponse response = request.GetResponse();

                        Stream reader = response.GetResponseStream();
                        string path = Path.Combine(this._productImagesDir.FullName, text114);
                        FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                        byte[] buff = new byte[512];
                        int c = 0; //实际读取的字节数
                        while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                        {
                            writer.Write(buff, 0, c);
                        }

                        writer.Close();
                        writer.Dispose();
                        reader.Close();
                        reader.Dispose();
                        response.Close();
                        //text3 += this.CopyImage((string)dataRow["ImageUrl1"], 1);
                    }
                    //if (dataRow["ImageUrl3"] != DBNull.Value)
                    if (!string.IsNullOrEmpty(dataRow["ImageUrl3"].ToString()))
                    {
                        string text111 = dataRow["ImageUrl3"].ToString();
                        string text110 = "";
                        for (int i = text111.Length - 1; i > 0; i--)
                        {
                            if (text111[i] == '/')
                            {
                                text110 = text111.Substring(i + 1, text111.Length - 1 - i);
                                break;
                            }
                        }
                        string text113 = text110.ToLower();
                        string text112 = "";
                        string text114 = "";
                        for (int k = text113.Length - 1; k > 0; k--)
                        {
                            if (text113[k] == '.')
                            {
                                text112 = text113.Substring(k, text113.Length - k);
                                break;
                            }
                        }

                        text114 = text113.Replace(text112, ".tbi");
                        //text100 = text114;
                        ////    text100 = Path.Combine(this._productImagesDir.FullName, text3);
                        text3 += text114.Replace(".tbi", string.Format(":1:{0}:|;", 2));
                        ////}

                        WebRequest request = WebRequest.Create(text111);
                        WebResponse response = request.GetResponse();

                        Stream reader = response.GetResponseStream();
                        string path = Path.Combine(this._productImagesDir.FullName, text114);
                        FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                        byte[] buff = new byte[512];
                        int c = 0; //实际读取的字节数
                        while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                        {
                            writer.Write(buff, 0, c);
                        }

                        writer.Close();
                        writer.Dispose();
                        reader.Close();
                        reader.Dispose();
                        response.Close();
                        //text3 += this.CopyImage((string)dataRow["ImageUrl1"], 1);
                    }
                    //if (dataRow["ImageUrl4"] != DBNull.Value)
                    if (!string.IsNullOrEmpty(dataRow["ImageUrl4"].ToString()))
                    {
                        string text111 = dataRow["ImageUrl4"].ToString();
                        string text110 = "";
                        for (int i = text111.Length - 1; i > 0; i--)
                        {
                            if (text111[i] == '/')
                            {
                                text110 = text111.Substring(i + 1, text111.Length - 1 - i);
                                break;
                            }
                        }
                        string text113 = text110.ToLower();
                        string text112 = "";
                        string text114 = "";
                        for (int k = text113.Length - 1; k > 0; k--)
                        {
                            if (text113[k] == '.')
                            {
                                text112 = text113.Substring(k, text113.Length - k);
                                break;
                            }
                        }

                        text114 = text113.Replace(text112, ".tbi");
                        //text100 = text114;
                        ////    text100 = Path.Combine(this._productImagesDir.FullName, text3);
                        text3 += text114.Replace(".tbi", string.Format(":1:{0}:|;", 3));
                        ////}

                        WebRequest request = WebRequest.Create(text111);
                        WebResponse response = request.GetResponse();

                        Stream reader = response.GetResponseStream();
                        string path = Path.Combine(this._productImagesDir.FullName, text114);
                        FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                        byte[] buff = new byte[512];
                        int c = 0; //实际读取的字节数
                        while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                        {
                            writer.Write(buff, 0, c);
                        }

                        writer.Close();
                        writer.Dispose();
                        reader.Close();
                        reader.Dispose();
                        response.Close();
                        //text3 += this.CopyImage((string)dataRow["ImageUrl1"], 1);
                    }
                    if (!string.IsNullOrEmpty(dataRow["ImageUrl5"].ToString()))
                    {
                        string text111 = dataRow["ImageUrl5"].ToString();
                        string text110 = "";
                        for (int i = text111.Length - 1; i > 0; i--)
                        {
                            if (text111[i] == '/')
                            {
                                text110 = text111.Substring(i + 1, text111.Length - 1 - i);
                                break;
                            }
                        }
                        string text113 = text110.ToLower();
                        string text112 = "";
                        string text114 = "";
                        for (int k = text113.Length - 1; k > 0; k--)
                        {
                            if (text113[k] == '.')
                            {
                                text112 = text113.Substring(k, text113.Length - k);
                                break;
                            }
                        }

                        text114 = text113.Replace(text112, ".tbi");
                        //text100 = text114;
                        ////    text100 = Path.Combine(this._productImagesDir.FullName, text3);
                        text3 += text114.Replace(".tbi", string.Format(":1:{0}:|;", 4));
                        ////}

                        WebRequest request = WebRequest.Create(text111);
                        WebResponse response = request.GetResponse();

                        Stream reader = response.GetResponseStream();
                        string path = Path.Combine(this._productImagesDir.FullName, text114);
                        FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                        byte[] buff = new byte[512];
                        int c = 0; //实际读取的字节数
                        while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                        {
                            writer.Write(buff, 0, c);
                        }

                        writer.Close();
                        writer.Dispose();
                        reader.Close();
                        reader.Dispose();
                        response.Close();
                        //text3 += this.CopyImage((string)dataRow["ImageUrl1"], 1);
                    }

                    //DataRow[] array = this._exportData.Tables["skus"].Select("ProductId=" + dataRow["ProductId"].ToString(), "SalePrice desc");
                    string text4 = "";
                    int num = 0;
                    string text5 = '1'.ToString();
                    string text6 = "0";
                    string text7 = "0";
                    string text8 = "0";
                    string text9 = "";
                    string text10 = "";
                    string text11 = "";
                    string text13 = "";
                    string text14 = "";
                    string text15 = "";
                    string text16 = "";
                    string text17 = "";
                    string text18 = "";
                    string text19 = "";
                    string text20 = "";
                    string text24 = "";
                    string text26 = "";
                    string text27 = "";
                    string text23 = "";
                    string text28 = "";
                    string text29 = "";
                    string text30 = "";
                    string text_custom = "";//自定义属性值
                    string text_propAlias = "";//销售属性别名
                    string text_memo = "";//属性值备注
                    if (!string.IsNullOrEmpty(dataRow["input_custom_cpv"].ToString()))
                    {
                        text_custom = dataRow["input_custom_cpv"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dataRow["propAlias"].ToString()))
                    {
                        text_propAlias = dataRow["propAlias"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dataRow["cpv_memo"].ToString()))
                    {
                        text_memo = dataRow["cpv_memo"].ToString();
                        /*转换备注格式*/
                        //if (!string.IsNullOrEmpty(text_memo))
                        //{
                        //    if (text_memo.IndexOf('(') > 0 && text_memo.IndexOf(')') > 0 && text_memo.IndexOf(')') - text_memo.IndexOf('(') > 0)
                        //    {
                        //        text_memo = text_memo.Split(new char[2] { '(', ')' })[1]; 
                        //    }                            
                        //}
                    }
                    DataRow[] array2 = this._exportData.Tables["TaobaoSku"].Select("ProductId=" + dataRow["Productid"].ToString());
                    if (array2.Length > 0)
                    {
                        if (this._includeStock)
                        {
                            text4 = Convert.ToString(array2[0]["Num"]);
                        }
                        else
                        {
                            text4 = Convert.ToString(array2[0]["Num"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(array2[0]["LocationState"])))
                        {
                            text15 = Convert.ToString(array2[0]["LocationState"]);
                        }
                        else
                        {
                            //text15 = string.Empty;
                            text15 = "浙江";
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(array2[0]["LocationCity"])))
                        {
                            text16 = Convert.ToString(array2[0]["LocationCity"]);
                        }
                        else
                        {
                            //text16 = string.Empty;
                            text16 = "杭州";
                        }

                         

                        //text16 = Convert.ToString(array2[0]["LocationCity"]);
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
                        text27 = Convert.ToString(Math.Round(Convert.ToDecimal(array2[0]["Weight"]) / 1000, 2));
                        text10 = Convert.ToString(array2[0]["Cid"]);
                        text13 = Convert.ToString(array2[0]["PropertyAlias"]);
                        text9 = Convert.ToString(array2[0]["inputpids"]);
                        text11 = Convert.ToString(array2[0]["inputstr"]);
                        text26 = Convert.ToString(array2[0]["Stock"]);
                        //text28 = Convert.ToString(array2[0]["platcid"]);
                        text29 = text10;
                        text24 = array2[0]["MarketPrice"].ToString();
                        string text21 = Convert.ToString(array2[0]["SkuQuantities"]);
                        string text22 = Convert.ToString(array2[0]["skuPrices"]);
                        text23 = Convert.ToString(array2[0]["SkuProperties"]);
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
                    stringBuilder.AppendFormat(format, new object[]
				{
                    this.Trim((string)dataRow["ProductName"])+"\t",          //商品名称
                    text29,                                     //商品类目

				    text,                                   //商品描述
                    0,                             //店铺类目
                   //text100,
                    text20,                         //新旧程度
                    text15,                     //省
                    text16,                      //城市
                    1,                        //出售方式
                    text24,                     //宝贝价格
                    0,                        //加价幅度
                    text4,                      //宝贝数量
                    7,                        //有效期
                    text5,                        //运费承担
                    text6,                      //平邮
                    text8,                      //EMS
                    text7,                      //快递
                    text17,                      //发票
                    text18,                     //保修
                    1,                         //放入仓库
                    1,                         //橱窗推荐 
                    "",                          //开始时间
                    text13,                     //宝贝属性
                    0,                        //邮费模板ID
                    text19,                     //会员打折
                    DateTime.Now.ToString(),                         //修改时间
                    200,                      //上传状态
                    ("2;2;2;2;2;").ToString(),       //图片状态
                    "5",                        //返点比例
                    text3,                         //新图片
                    null,                         //视频
                    text23,                     //销售属性组合
                    text9,                      //用户输入ID串
                    text11,                     //用户输入名-值对
                    text36,                     //商家编码
                    text_propAlias,             //销售属性别名PropertyAlias
                    0,                        //代充类型
                    null,                         //数字ID
                    0,                        //本地ID
                    2,                        //宝贝分类
                    null,     //用户名称
                    2,                        //宝贝状态
                    8,                      //闪电发货
                    243,                      //新品
                    null,                         //食品专项
                    null,                         //尺码库
                    0,                        //采购地
                    -1,                       //库存类型
                    null,                         //国家地区
                    2,                     //库存计数
                    null,                         //物流体积
                    text27,                         //物流重量
                    1,                        //退换货承诺
                    null,                         //定制工具
                    null,                         //无线详情
                    null,                         //条形码
                    null,                         //sku条形码
                    1,                        //7天退货
                    null,                         //宝贝热点
                    text_memo,                  //属性值备注
                    text_custom,                //自定义属性值
                   
                   
				});
                }
            }
            return stringBuilder.Remove(stringBuilder.Length - 2, 2).ToString();
        }
        private string CopyImage(string imageUrl, int index)
        {
            string text = string.Empty;
            //text100 += imageUrl;
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
                        text3 = text3.Replace(fileInfo.Extension.ToLower(), ".tbi");
                        fileInfo.CopyTo(Path.Combine(this._productImagesDir.FullName, text3), true);
                        text100 = Path.Combine(this._productImagesDir.FullName, text3);
                        text += text3.Replace(".tbi", string.Format(":1:{0}:|;", index - 1));
                    }
                }
            }
            else
            {
                //string url = imageUrl;
                //string filepath = "x:\\pic.jpg";
                //WebClient mywebclient = new WebClient();
                //mywebclient.DownloadFile(url, filepath);

                //WebRequest request = WebRequest.Create(imageUrl);
                //WebResponse response = request.GetResponse();

                //Stream reader = response.GetResponseStream();
                //string text110 = "";
                //for (int i = imageUrl.Length - 1; i > 0; i--)
                //{
                //    if (imageUrl[i] == '/')
                //    {
                //        text110 = imageUrl.Substring(i + 1, imageUrl.Length - 1 - i);
                //    }
                //}
                ////string text3 = text110.ToLower();
                ////string text111 = "";
                ////for(int k=text3.Length-1;k>0;k--)
                ////{
                ////    if (text3[k] == '.')
                ////    {
                ////        text111 = text3.Substring(k,text3.Length-k);
                ////    }                
                ////}
                ////if (text3.EndsWith(".jpg") || text3.EndsWith(".gif") || text3.EndsWith(".jpeg") || text3.EndsWith(".png") || text3.EndsWith(".bmp"))
                ////{
                ////    text3 = text3.Replace(text111, ".tbi");                   
                ////    text100 = Path.Combine(this._productImagesDir.FullName, text3);
                ////    text += text3.Replace(".tbi", string.Format(":1:{0}:|;", index - 1));
                ////}
                //string path = Path.Combine(this._productImagesDir.FullName, text110);
                //text100 += path;
                //FileStream writer = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                //byte[] buff = new byte[512];
                //int c = 0; //实际读取的字节数
                //while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                //{
                //    writer.Write(buff, 0, c);
                //}

                //writer.Close();
                //writer.Dispose();
                //reader.Close();
                //reader.Dispose();
                //response.Close();

                ////string text2 = imageUrl;
                ////FileInfo fileInfo = new FileInfo(text2);
                ////string text3 = fileInfo.Name.ToLower();
                ////if (text3.EndsWith(".jpg") || text3.EndsWith(".gif") || text3.EndsWith(".jpeg") || text3.EndsWith(".png") || text3.EndsWith(".bmp"))
                ////{
                ////    text3 = text3.Replace(fileInfo.Extension.ToLower(), ".tbi");
                ////    fileInfo.CopyTo(Path.Combine(this._productImagesDir.FullName, text3), true);
                ////    text100 = Path.Combine(this._productImagesDir.FullName, text3);
                ////    text += text3.Replace(".tbi", string.Format(":1:{0}:|;", index - 1));
                ////}
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
    }
}
