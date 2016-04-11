using Hishop.TransferManager;
using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Globalization;
using Hidistro.Membership.Context;
namespace Hishop.Transfers.PaipaiExporters
{
	public class Yfx1_2_to_Paipai4_0 : ExportAdapter
	{
		private const string ExportVersion = "4.0";
		private const string ProductFilename = "products.csv";
		private readonly Encoding _encoding = Encoding.Unicode;
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
		public Yfx1_2_to_Paipai4_0()
		{
			this._exportTo = new PPTarget("4.0");
			this._source = new YfxTarget("1.2");
		}
		public Yfx1_2_to_Paipai4_0(params object[] exportParams) : this()
		{
			this._exportData = (DataSet)exportParams[0];
			this._includeCostPrice = (bool)exportParams[1];
			this._includeStock = (bool)exportParams[2];
			this._includeImages = (bool)exportParams[3];
			this._url = (string)exportParams[4];
			this._applicationPath = (string)exportParams[5];
			this._baseDir = new DirectoryInfo(HttpContext.Current.Request.MapPath("~/storage/data/paipai"));
			this._flag = DateTime.Now.ToString("yyyyMMddHHmmss");
			this._zipFilename = string.Format("paipai.{0}.{1}.zip", "4.0", this._flag);
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
			string format = "\r\n-1\t\"{0}\"\t\"{1}\"\t{2}\t{3}\t{4}\t{5}\t{6}\t\"{7}\"\t{8}\t{9}\t{10}\t\"{11}\"\t\"{12}\"\t{13}\t{14}\t{15}\t{16}\t\"{17}\"\t{18}\t{19}\t{20}\t{21}\t{22}\t\"{23}\"\t\"{24}\"\t\"{25}\"\t\"{26}\"\t\"{27}\"\t\"{28}\"\t\"{29}\"\t{30}\t{31}\t{32}\t{33}\t{34}\t{35}\t\"{36}\"\t{37}\t\"{38}\"\t{39}\t\"{40}\"";
			stringBuilder.Append("\"id\"\t\"商品名称\"\t\"出售方式\"\t\"商品类目\"\t\"店铺类目\"\t\"商品数量\"\t\"商品重量\"\t\"有效期\"\t\"定时上架\"\t\"新旧程度\"\t\"价格\"\t\"加价幅度\"\t");
			stringBuilder.Append("\"省\"\t\"市\"\t\"运费承担\"\t\"平邮\"\t\"快递\"\t\"EMS\"\t\"购买限制\"\t\"付款方式\"\t\"有发票\"\t\"有保修\"\t\"支持财付通\"\t\"自动重发\"\t\"错误原因\"\t");
			stringBuilder.Append("\"图片\"\t\"图片2\"\t\"图片3\"\t\"图片4\"\t\"图片5\"\t\"商品详情\"\t\"上架选项\"\t\"皮肤风格\"\t\"属性\"\t\"诚保\"\t\"假一陪三\"\t\"橱窗\"\t\"库存属性\"\t");
			stringBuilder.Append("\"产品ID\"\t\"商家编码\"\t\"尺码对照表\"\t\"版本\"");
			foreach (DataRow dataRow in this._exportData.Tables["products"].Rows)
			{
				string text = "{" + Guid.NewGuid().ToString() + "}.htm";
				string path = Path.Combine(this._productImagesDir.FullName, text);
				using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.GetEncoding("gb2312")))
				{
					if (dataRow["Description"] != DBNull.Value)
					{
						string text2 = (string)dataRow["Description"];
						text2 = text2.Replace(string.Format("src=\"{0}/Storage/master/gallery", this._applicationPath), string.Format("src=\"{0}/Storage/master/gallery", this._url));
						streamWriter.Write(text2);
					}
				}
				string text3 = dataRow["ImageUrl1"].ToString();
				if (!text3.StartsWith("http://"))
				{
					string text4 = HttpContext.Current.Request.MapPath("~" + text3);
					if (File.Exists(text4))
					{
						FileInfo fileInfo = new FileInfo(text4);
						text3 = fileInfo.Name.ToLower();
						fileInfo.CopyTo(Path.Combine(this._productImagesDir.FullName, text3), true);
					}
				}
				string text5 = dataRow["ImageUrl2"].ToString();
				if (!text5.StartsWith("http://"))
				{
					string text6 = HttpContext.Current.Request.MapPath("~" + text5);
					if (File.Exists(text6))
					{
						FileInfo fileInfo2 = new FileInfo(text6);
						text5 = fileInfo2.Name.ToLower();
						fileInfo2.CopyTo(Path.Combine(this._productImagesDir.FullName, text5), true);
					}
				}
				string text7 = dataRow["ImageUrl3"].ToString();
				if (!text7.StartsWith("http://"))
				{
					string text8 = HttpContext.Current.Request.MapPath("~" + text7);
					if (File.Exists(text8))
					{
						FileInfo fileInfo3 = new FileInfo(text8);
						text7 = fileInfo3.Name.ToLower();
						fileInfo3.CopyTo(Path.Combine(this._productImagesDir.FullName, text7), true);
					}
				}
				string text9 = dataRow["ImageUrl4"].ToString();
				if (!text9.StartsWith("http://"))
				{
					string text10 = HttpContext.Current.Request.MapPath("~" + text9);
					if (File.Exists(text10))
					{
						FileInfo fileInfo4 = new FileInfo(text10);
						text9 = fileInfo4.Name.ToLower();
						fileInfo4.CopyTo(Path.Combine(this._productImagesDir.FullName, text9), true);
					}
				}
				string text11 = dataRow["ImageUrl5"].ToString();
				if (!text11.StartsWith("http://"))
				{
					string text12 = HttpContext.Current.Request.MapPath("~" + text11);
					if (File.Exists(text12))
					{
						FileInfo fileInfo5 = new FileInfo(text12);
						text11 = fileInfo5.Name.ToLower();
						fileInfo5.CopyTo(Path.Combine(this._productImagesDir.FullName, text11), true);
					}
				}
				DataRow[] array = this._exportData.Tables["skus"].Select("ProductId=" + dataRow["ProductId"].ToString(), "SalePrice desc");
				int num = 0;
				if (this._includeStock)
				{
					DataRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						DataRow dataRow2 = array2[i];
						num += (int)dataRow2["Stock"];
					}
				}
				stringBuilder.AppendFormat(format, new object[]
				{
					dataRow["ProductName"],
					"b",
					"0",
					"0",
					num,
					array[0]["Weight"],
					"7",
					"1970-1-1  8:00:00",
					"1",
					array[0]["SalePrice"],
					"",
					"",
					"",
					"1",
					"0.00",
					"0.00",
					"0.00",
					"",
					"0",
					"2",
					"2",
					"1",
					"0",
					"",
					text3,
					text5,
					text7,
					text9,
					text11,
					text,
					"2",
					"0",
					"0",
					"0",
					"0",
					"1",
					"",
					"0",
					dataRow["ProductCode"],
					"0",
					"拍拍助理-商品管理 4.0 [54]"
				});
			}
			return stringBuilder.ToString();
		}

        public override void DoExport(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
