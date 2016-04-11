using Hishop.TransferManager;
using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using Hidistro.Membership.Context;
namespace Hishop.Transfers.YfxExporters
{

	public class Yfx1_2_to_Yfx1_2 : ExportAdapter
	{
		private const string ExportVersion = "1.2";
		private const string IndexFilename = "indexes.xml";
		private const string ProductFilename = "products.xml";
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
		private DirectoryInfo _typeImagesDir;
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
		public Yfx1_2_to_Yfx1_2()
		{
			this._exportTo = new YfxTarget("1.2");
			this._source = new YfxTarget("1.2");
		}
		public Yfx1_2_to_Yfx1_2(params object[] exportParams) : this()
		{
			this._exportData = (DataSet)exportParams[0];
			this._includeCostPrice = (bool)exportParams[1];
			this._includeStock = (bool)exportParams[2];
			this._includeImages = (bool)exportParams[3];
			this._url = (string)exportParams[4];
			this._applicationPath = (string)exportParams[5];
			this._baseDir = new DirectoryInfo(HttpContext.Current.Request.MapPath("~/storage/data/yfx"));
			this._flag = DateTime.Now.ToString("yyyyMMddHHmmss");
			this._zipFilename = string.Format("YFX.{0}.{1}.zip", "1.2", this._flag);
		}
		public override void DoExport()
		{
			this._workDir = this._baseDir.CreateSubdirectory(this._flag);
			this._typeImagesDir = this._workDir.CreateSubdirectory("images1");
			this._productImagesDir = this._workDir.CreateSubdirectory("images2");
			string text = Path.Combine(this._workDir.FullName, "indexes.xml");
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
			{
				XmlWriter xmlWriter = new XmlTextWriter(fileStream, this._encoding);
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("indexes");
				xmlWriter.WriteAttributeString("version", "1.2");
				xmlWriter.WriteAttributeString("QTY", this._exportData.Tables["products"].Rows.Count.ToString(CultureInfo.InvariantCulture));
				xmlWriter.WriteAttributeString("includeCostPrice", this._includeCostPrice.ToString());
				xmlWriter.WriteAttributeString("includeStock", this._includeStock.ToString());
				xmlWriter.WriteAttributeString("includeImages", this._includeImages.ToString());
				this.WriteIndexes(xmlWriter);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndDocument();
				xmlWriter.Close();
			}
			string text2 = Path.Combine(this._workDir.FullName, "products.xml");
			using (FileStream fileStream2 = new FileStream(text2, FileMode.Create, FileAccess.Write))
			{
				XmlWriter xmlWriter2 = new XmlTextWriter(fileStream2, this._encoding);
				xmlWriter2.WriteStartDocument();
				xmlWriter2.WriteStartElement("products");
				this.WriteProducts(xmlWriter2);
				xmlWriter2.WriteEndElement();
				xmlWriter2.WriteEndDocument();
				xmlWriter2.Close();
			}
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile.CompressionLevel = CompressionLevel.Default;
				zipFile.AddFile(text, "");
				zipFile.AddFile(text2, "");
				zipFile.AddDirectory(this._typeImagesDir.FullName, this._typeImagesDir.Name);
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
		private void WriteIndexes(XmlWriter indexWriter)
		{
			indexWriter.WriteStartElement("types");
			foreach (DataRow dataRow in this._exportData.Tables["types"].Rows)
			{
				indexWriter.WriteStartElement("type");
				indexWriter.WriteElementString("typeId", dataRow["TypeId"].ToString());
				TransferHelper.WriteCDataElement(indexWriter, "typeName", dataRow["TypeName"].ToString());
				TransferHelper.WriteCDataElement(indexWriter, "remark", dataRow["Remark"].ToString());
				indexWriter.WriteStartElement("attributes");
				DataRow[] array = this._exportData.Tables["attributes"].Select("TypeId=" + dataRow["TypeId"].ToString());
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow2 = array2[i];
					indexWriter.WriteStartElement("attribute");
					indexWriter.WriteElementString("attributeId", dataRow2["AttributeId"].ToString());
					TransferHelper.WriteCDataElement(indexWriter, "attributeName", dataRow2["AttributeName"].ToString());
					indexWriter.WriteElementString("displaySequence", dataRow2["DisplaySequence"].ToString());
					indexWriter.WriteElementString("usageMode", dataRow2["UsageMode"].ToString());
					indexWriter.WriteElementString("useAttributeImage", dataRow2["UseAttributeImage"].ToString());
					indexWriter.WriteStartElement("values");
					DataRow[] array3 = this._exportData.Tables["values"].Select("AttributeId=" + dataRow2["AttributeId"].ToString());
					DataRow[] array4 = array3;
					for (int j = 0; j < array4.Length; j++)
					{
						DataRow dataRow3 = array4[j];
						indexWriter.WriteStartElement("value");
						indexWriter.WriteElementString("valueId", dataRow3["ValueId"].ToString());
						indexWriter.WriteElementString("displaySequence", dataRow3["DisplaySequence"].ToString());
						TransferHelper.WriteCDataElement(indexWriter, "valueStr", dataRow3["ValueStr"].ToString());
						TransferHelper.WriteImageElement(indexWriter, "image", this._includeImages, dataRow3["ImageUrl"].ToString(), this._typeImagesDir);
						indexWriter.WriteEndElement();
					}
					indexWriter.WriteEndElement();
					indexWriter.WriteEndElement();
				}
				indexWriter.WriteEndElement();
				indexWriter.WriteEndElement();
			}
			indexWriter.WriteEndElement();
			this._exportData.Tables.Remove("values");
			this._exportData.Tables.Remove("attributes");
			this._exportData.Tables.Remove("types");
		}
		private void WriteProducts(XmlWriter productWriter)
		{
			productWriter.WriteStartElement("products");
			foreach (DataRow dataRow in this._exportData.Tables["products"].Rows)
			{
				productWriter.WriteStartElement("product");
				productWriter.WriteElementString("productId", dataRow["ProductId"].ToString());
				productWriter.WriteElementString("typeId", dataRow["TypeId"].ToString());
				TransferHelper.WriteCDataElement(productWriter, "productName", dataRow["ProductName"].ToString());
				TransferHelper.WriteCDataElement(productWriter, "productCode", dataRow["ProductCode"].ToString());
				TransferHelper.WriteCDataElement(productWriter, "shortDescription", dataRow["ShortDescription"].ToString());
				productWriter.WriteElementString("unit", dataRow["Unit"].ToString());
				TransferHelper.WriteCDataElement(productWriter, "description", dataRow["Description"].ToString().Replace(string.Format("src=\"{0}/Storage/master/gallery", this._applicationPath), string.Format("src=\"{0}/Storage/master/gallery", this._url)));
				TransferHelper.WriteCDataElement(productWriter, "title", dataRow["Title"].ToString());
				TransferHelper.WriteCDataElement(productWriter, "meta_Description", dataRow["Meta_Description"].ToString());
				TransferHelper.WriteCDataElement(productWriter, "meta_Keywords", dataRow["Meta_Keywords"].ToString());
				productWriter.WriteElementString("saleStatus", dataRow["SaleStatus"].ToString());
				TransferHelper.WriteImageElement(productWriter, "image1", this._includeImages, dataRow["ImageUrl1"].ToString(), this._productImagesDir);
				TransferHelper.WriteImageElement(productWriter, "image2", this._includeImages, dataRow["ImageUrl2"].ToString(), this._productImagesDir);
				TransferHelper.WriteImageElement(productWriter, "image3", this._includeImages, dataRow["ImageUrl3"].ToString(), this._productImagesDir);
				TransferHelper.WriteImageElement(productWriter, "image4", this._includeImages, dataRow["ImageUrl4"].ToString(), this._productImagesDir);
				TransferHelper.WriteImageElement(productWriter, "image5", this._includeImages, dataRow["ImageUrl5"].ToString(), this._productImagesDir);
				productWriter.WriteElementString("marketPrice", dataRow["MarketPrice"].ToString());
				productWriter.WriteElementString("lowestSalePrice", dataRow["LowestSalePrice"].ToString());
				productWriter.WriteElementString("penetrationStatus", dataRow["PenetrationStatus"].ToString());
				productWriter.WriteElementString("hasSKU", dataRow["HasSKU"].ToString());
				DataRow[] array = this._exportData.Tables["productAttributes"].Select("ProductId=" + dataRow["ProductId"].ToString());
				productWriter.WriteStartElement("attributes");
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow2 = array2[i];
					productWriter.WriteStartElement("attribute");
					productWriter.WriteElementString("attributeId", dataRow2["AttributeId"].ToString());
					productWriter.WriteElementString("valueId", dataRow2["ValueId"].ToString());
					productWriter.WriteEndElement();
				}
				productWriter.WriteEndElement();
				DataRow[] array3 = this._exportData.Tables["skus"].Select("ProductId=" + dataRow["ProductId"].ToString());
				productWriter.WriteStartElement("skus");
				DataRow[] array4 = array3;
				for (int j = 0; j < array4.Length; j++)
				{
					DataRow dataRow3 = array4[j];
					productWriter.WriteStartElement("sku");
					productWriter.WriteElementString("skuId", dataRow3["SkuId"].ToString());
					productWriter.WriteElementString("sKU", dataRow3["SKU"].ToString());
					if (this._includeCostPrice)
					{
						productWriter.WriteElementString("costPrice", dataRow3["CostPrice"].ToString());
					}
					productWriter.WriteElementString("weight", dataRow3["Weight"].ToString());
					if (this._includeStock)
					{
						productWriter.WriteElementString("stock", dataRow3["Stock"].ToString());
					}
					productWriter.WriteElementString("alertStock", dataRow3["AlertStock"].ToString());
					productWriter.WriteElementString("salePrice", dataRow3["SalePrice"].ToString());
					productWriter.WriteElementString("purchasePrice", dataRow3["PurchasePrice"].ToString());
					DataRow[] array5 = this._exportData.Tables["skuItems"].Select("SkuId='" + dataRow3["SkuId"].ToString() + "'");
					productWriter.WriteStartElement("skuItems");
					DataRow[] array6 = array5;
					for (int k = 0; k < array6.Length; k++)
					{
						DataRow dataRow4 = array6[k];
						productWriter.WriteStartElement("skuItem");
						productWriter.WriteElementString("skuId", dataRow4["SkuId"].ToString());
						productWriter.WriteElementString("attributeId", dataRow4["AttributeId"].ToString());
						productWriter.WriteElementString("valueId", dataRow4["ValueId"].ToString());
						productWriter.WriteEndElement();
					}
					productWriter.WriteEndElement();
					productWriter.WriteEndElement();
				}
				productWriter.WriteEndElement();
				productWriter.WriteEndElement();
			}
			productWriter.WriteEndElement();
			this._exportData.Tables.Remove("skuItems");
			this._exportData.Tables.Remove("skus");
			this._exportData.Tables.Remove("products");
			this._exportData.Tables.Remove("productAttributes");
		}

        public override void DoExport(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
