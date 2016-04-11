using Hishop.TransferManager;
using Ionic.Zip;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Xml;
using Hidistro.Membership.Context;
namespace Hishop.Transfers.YfxImporters
{
    public class Yfx1_2_from_Yfx1_2 : ImportAdapter
    {
        private const string IndexFilename = "indexes.xml";
        private const string ProductFilename = "products.xml";
        private readonly Target _importTo;
        private readonly Target _source;
        private readonly DirectoryInfo _baseDir;
        private DirectoryInfo _workDir;
        private DirectoryInfo _typeImagesDir;
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
        public Yfx1_2_from_Yfx1_2()
        {
            this._importTo = new YfxTarget("1.2");
            this._source = new YfxTarget("1.2");
            string tempPath = "~/storage/data/yfx/" + HiContext.Current.User.Username.ToString();
            if (!Directory.Exists(HttpContext.Current.Request.MapPath(tempPath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Request.MapPath(tempPath));
            }
            this._baseDir = new DirectoryInfo(HttpContext.Current.Request.MapPath(tempPath));
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
            XmlDocument xmlDocument = (XmlDocument)initParams[0];
            string text = (string)initParams[1];
            DataSet mappingSet = this.GetMappingSet();
            XmlDocument xmlDocument2 = new XmlDocument();
            xmlDocument2.Load(Path.Combine(text, "indexes.xml"));
            XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("//type");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                DataRow dataRow = mappingSet.Tables["types"].NewRow();
                int num = int.Parse(xmlNode.Attributes["mappedTypeId"].Value);
                int num2 = int.Parse(xmlNode.Attributes["selectedTypeId"].Value);
                dataRow["MappedTypeId"] = num;
                dataRow["SelectedTypeId"] = num2;
                if (num2 == 0)
                {
                    XmlNode xmlNode2 = xmlDocument2.SelectSingleNode("//type[typeId[text()='" + num + "']]");
                    dataRow["TypeName"] = xmlNode2.SelectSingleNode("typeName").InnerText;
                    dataRow["Remark"] = xmlNode2.SelectSingleNode("remark").InnerText;
                }
                mappingSet.Tables["types"].Rows.Add(dataRow);
                XmlNodeList attributeNodeList = xmlNode.SelectNodes("attributes/attribute");
                this.MappingAttributes(num, mappingSet, attributeNodeList, xmlDocument2, text);
            }
            mappingSet.AcceptChanges();
            return new object[]
			{
				mappingSet
			};
        }
        private void MappingAttributes(int mappedTypeId, DataSet mappingSet, XmlNodeList attributeNodeList, XmlDocument indexesDoc, string workDir)
        {
            if (attributeNodeList == null || attributeNodeList.Count == 0)
            {
                return;
            }
            foreach (XmlNode xmlNode in attributeNodeList)
            {
                DataRow dataRow = mappingSet.Tables["attributes"].NewRow();
                int num = int.Parse(xmlNode.Attributes["mappedAttributeId"].Value);
                int num2 = int.Parse(xmlNode.Attributes["selectedAttributeId"].Value);
                dataRow["MappedAttributeId"] = num;
                dataRow["SelectedAttributeId"] = num2;
                dataRow["MappedTypeId"] = mappedTypeId;
                if (num2 == 0)
                {
                    XmlNode xmlNode2 = indexesDoc.SelectSingleNode("//attribute[attributeId[text()='" + num + "']]");
                    dataRow["AttributeName"] = xmlNode2.SelectSingleNode("attributeName").InnerText;
                    dataRow["DisplaySequence"] = xmlNode2.SelectSingleNode("displaySequence").InnerText;
                    dataRow["UsageMode"] = xmlNode2.SelectSingleNode("usageMode").InnerText;
                    dataRow["UseAttributeImage"] = xmlNode2.SelectSingleNode("useAttributeImage").InnerText;
                }
                mappingSet.Tables["attributes"].Rows.Add(dataRow);
                XmlNodeList valueNodeList = xmlNode.SelectNodes("values/value");
                this.MappingValues(num, num2, mappingSet, valueNodeList, indexesDoc, workDir);
            }
        }
        private void MappingValues(int mappedAttributeId, int selectedAttributeId, DataSet mappingSet, XmlNodeList valueNodeList, XmlDocument indexesDoc, string workDir)
        {
            if (valueNodeList == null || valueNodeList.Count == 0)
            {
                return;
            }
            foreach (XmlNode xmlNode in valueNodeList)
            {
                DataRow dataRow = mappingSet.Tables["values"].NewRow();
                int num = int.Parse(xmlNode.Attributes["mappedValueId"].Value);
                int num2 = int.Parse(xmlNode.Attributes["selectedValueId"].Value);
                dataRow["MappedValueId"] = num;
                dataRow["SelectedValueId"] = num2;
                dataRow["MappedAttributeId"] = mappedAttributeId;
                dataRow["SelectedAttributeId"] = selectedAttributeId;
                if (num2 == 0)
                {
                    XmlNode xmlNode2 = indexesDoc.SelectSingleNode("//value[valueId[text()='" + num + "']]");
                    dataRow["DisplaySequence"] = xmlNode2.SelectSingleNode("displaySequence").InnerText;
                    dataRow["ValueStr"] = xmlNode2.SelectSingleNode("valueStr").InnerText;
                    string innerText = xmlNode2.SelectSingleNode("image").InnerText;
                    if (innerText.Length > 0 && File.Exists(Path.Combine(workDir + "\\images1", innerText)))
                    {
                        File.Copy(Path.Combine(workDir + "\\images1", innerText), HttpContext.Current.Request.MapPath("~/Storage/master/sku/" + innerText), true);
                        dataRow["ImageUrl"] = "/Storage/master/sku/" + innerText;
                    }
                }
                mappingSet.Tables["values"].Rows.Add(dataRow);
            }
        }
        private DataSet GetMappingSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("types");
            dataTable.Columns.Add(new DataColumn("MappedTypeId")
            {
                Unique = true,
                DataType = Type.GetType("System.Int32")
            });
            dataTable.Columns.Add(new DataColumn("SelectedTypeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable.Columns.Add(new DataColumn("TypeName"));
            dataTable.Columns.Add(new DataColumn("Remark"));
            dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["MappedTypeId"]
			};
            DataTable dataTable2 = new DataTable("attributes");
            dataTable2.Columns.Add(new DataColumn("MappedAttributeId")
            {
                Unique = true,
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.Columns.Add(new DataColumn("SelectedAttributeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.Columns.Add(new DataColumn("AttributeName"));
            dataTable2.Columns.Add(new DataColumn("DisplaySequence"));
            dataTable2.Columns.Add(new DataColumn("MappedTypeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.Columns.Add(new DataColumn("UsageMode"));
            dataTable2.Columns.Add(new DataColumn("UseAttributeImage"));
            dataTable2.PrimaryKey = new DataColumn[]
			{
				dataTable2.Columns["MappedAttributeId"]
			};
            DataTable dataTable3 = new DataTable("values");
            dataTable3.Columns.Add(new DataColumn("MappedValueId")
            {
                Unique = true,
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("SelectedValueId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("MappedAttributeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("SelectedAttributeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("DisplaySequence"));
            dataTable3.Columns.Add(new DataColumn("ValueStr"));
            dataTable3.Columns.Add(new DataColumn("ImageUrl"));
            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(dataTable2);
            dataSet.Tables.Add(dataTable3);
            return dataSet;
        }
        public override object[] ParseIndexes(params object[] importParams)
        {
            string text = (string)importParams[0];
            if (!Directory.Exists(text))
            {
                throw new DirectoryNotFoundException("directory:" + text + " does not found");
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(text, "indexes.xml"));
            XmlNode xmlNode = xmlDocument.DocumentElement.SelectSingleNode("/indexes");
            string value = xmlNode.Attributes["version"].Value;
            int num = int.Parse(xmlNode.Attributes["QTY"].Value);
            bool flag = bool.Parse(xmlNode.Attributes["includeCostPrice"].Value);
            bool flag2 = bool.Parse(xmlNode.Attributes["includeStock"].Value);
            bool flag3 = bool.Parse(xmlNode.Attributes["includeImages"].Value);
            string text2 = "<xml>" + xmlNode.OuterXml + "</xml>";
            return new object[]
			{
				value,
				num,
				flag,
				flag2,
				flag3,
				text2
			};
        }
        public override object[] ParseProductData(params object[] importParams)
        {
            DataSet dataSet = (DataSet)importParams[0];
            string text = (string)importParams[1];
            bool includeCostPrice = (bool)importParams[2];
            bool includeStock = (bool)importParams[3];
            bool flag = (bool)importParams[4];
            HttpContext current = HttpContext.Current;
            DataSet productSet = this.GetProductSet();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(text, "products.xml"));
            XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("//product");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                DataRow dataRow = productSet.Tables["products"].NewRow();
                int num = int.Parse(xmlNode.SelectSingleNode("productId").InnerText);
                int num2 = 0;
                int num3 = 0;
                if (xmlNode.SelectSingleNode("typeId").InnerText.Length > 0)
                {
                    num2 = int.Parse(xmlNode.SelectSingleNode("typeId").InnerText);
                    if (num2 != 0)
                    {
                        num3 = (int)dataSet.Tables["types"].Select("MappedTypeId=" + num2.ToString(CultureInfo.InvariantCulture))[0]["SelectedTypeId"];
                    }
                }
                bool flag2 = bool.Parse(xmlNode.SelectSingleNode("hasSKU").InnerText);
                dataRow["ProductId"] = num;
                dataRow["SelectedTypeId"] = num3;
                dataRow["MappedTypeId"] = num2;
                dataRow["ProductName"] = xmlNode.SelectSingleNode("productName").InnerText;
                dataRow["ProductCode"] = xmlNode.SelectSingleNode("productCode").InnerText;
                dataRow["ShortDescription"] = xmlNode.SelectSingleNode("shortDescription").InnerText;
                dataRow["Unit"] = xmlNode.SelectSingleNode("unit").InnerText;
                dataRow["Description"] = xmlNode.SelectSingleNode("description").InnerText;
                dataRow["Title"] = xmlNode.SelectSingleNode("title").InnerText;
                dataRow["Meta_Description"] = xmlNode.SelectSingleNode("meta_Description").InnerText;
                dataRow["Meta_Keywords"] = xmlNode.SelectSingleNode("meta_Keywords").InnerText;
                dataRow["SaleStatus"] = int.Parse(xmlNode.SelectSingleNode("saleStatus").InnerText);
                string innerText = xmlNode.SelectSingleNode("image1").InnerText;
                string innerText2 = xmlNode.SelectSingleNode("image2").InnerText;
                string innerText3 = xmlNode.SelectSingleNode("image3").InnerText;
                string innerText4 = xmlNode.SelectSingleNode("image4").InnerText;
                string innerText5 = xmlNode.SelectSingleNode("image5").InnerText;
                dataRow["ImageUrl1"] = innerText;
                dataRow["ImageUrl2"] = innerText2;
                dataRow["ImageUrl3"] = innerText3;
                dataRow["ImageUrl4"] = innerText4;
                dataRow["ImageUrl5"] = innerText5;
                if (flag)
                {
                    if (innerText.Length > 0 && File.Exists(Path.Combine(text + "\\images2", innerText)))
                    {
                        File.Copy(Path.Combine(text + "\\images2", innerText), current.Request.MapPath("~/Storage/master/product/images/" + innerText), true);
                        dataRow["ImageUrl1"] = "/Storage/master/product/images/" + innerText;
                    }
                    if (innerText2.Length > 0 && File.Exists(Path.Combine(text + "\\images2", innerText2)))
                    {
                        File.Copy(Path.Combine(text + "\\images2", innerText2), current.Request.MapPath("~/Storage/master/product/images/" + innerText2), true);
                        dataRow["ImageUrl2"] = "/Storage/master/product/images/" + innerText2;
                    }
                    if (innerText3.Length > 0 && File.Exists(Path.Combine(text + "\\images2", innerText3)))
                    {
                        File.Copy(Path.Combine(text + "\\images2", innerText3), current.Request.MapPath("~/Storage/master/product/images/" + innerText3), true);
                        dataRow["ImageUrl3"] = "/Storage/master/product/images/" + innerText3;
                    }
                    if (innerText4.Length > 0 && File.Exists(Path.Combine(text + "\\images2", innerText4)))
                    {
                        File.Copy(Path.Combine(text + "\\images2", innerText4), current.Request.MapPath("~/Storage/master/product/images/" + innerText4), true);
                        dataRow["ImageUrl4"] = "/Storage/master/product/images/" + innerText4;
                    }
                    if (innerText5.Length > 0 && File.Exists(Path.Combine(text + "\\images2", innerText5)))
                    {
                        File.Copy(Path.Combine(text + "\\images2", innerText5), current.Request.MapPath("~/Storage/master/product/images/" + innerText5), true);
                        dataRow["ImageUrl5"] = "/Storage/master/product/images/" + innerText5;
                    }
                }
                if (xmlNode.SelectSingleNode("marketPrice").InnerText.Length > 0)
                {
                    dataRow["MarketPrice"] = decimal.Parse(xmlNode.SelectSingleNode("marketPrice").InnerText);
                }
                dataRow["LowestSalePrice"] = decimal.Parse(xmlNode.SelectSingleNode("lowestSalePrice").InnerText);
                dataRow["PenetrationStatus"] = int.Parse(xmlNode.SelectSingleNode("penetrationStatus").InnerText);
                dataRow["HasSKU"] = flag2;
                productSet.Tables["products"].Rows.Add(dataRow);
                XmlNodeList attributeNodeList = xmlNode.SelectNodes("attributes/attribute");
                this.loadProductAttributes(num, attributeNodeList, productSet, dataSet);
                XmlNodeList valueNodeList = xmlNode.SelectNodes("skus/sku");
                this.loadProductSkus(num, flag2, valueNodeList, productSet, dataSet, includeCostPrice, includeStock);
            }
            return new object[]
			{
				productSet
			};
        }
        private void loadProductSkus(int productId, bool hasSku, XmlNodeList valueNodeList, DataSet productSet, DataSet mappingSet, bool includeCostPrice, bool includeStock)
        {
            if (valueNodeList == null || valueNodeList.Count == 0)
            {
                return;
            }
            foreach (XmlNode xmlNode in valueNodeList)
            {
                DataRow dataRow = productSet.Tables["skus"].NewRow();
                string innerText = xmlNode.SelectSingleNode("skuId").InnerText;
                dataRow["MappedSkuId"] = innerText;
                dataRow["ProductId"] = productId;
                dataRow["SKU"] = xmlNode.SelectSingleNode("sKU").InnerText;
                if (xmlNode.SelectSingleNode("weight").InnerText.Length > 0)
                {
                    dataRow["Weight"] = int.Parse(xmlNode.SelectSingleNode("weight").InnerText);
                }
                if (includeStock)
                {
                    dataRow["Stock"] = int.Parse(xmlNode.SelectSingleNode("stock").InnerText);
                }
                dataRow["AlertStock"] = xmlNode.SelectSingleNode("alertStock").InnerText;
                if (includeCostPrice && xmlNode.SelectSingleNode("costPrice").InnerText.Length > 0)
                {
                    dataRow["CostPrice"] = xmlNode.SelectSingleNode("costPrice").InnerText;
                }
                dataRow["SalePrice"] = xmlNode.SelectSingleNode("salePrice").InnerText;
                dataRow["PurchasePrice"] = xmlNode.SelectSingleNode("purchasePrice").InnerText;
                XmlNodeList itemNodeList = xmlNode.SelectNodes("skuItems/skuItem");
                string text = this.loadSkuItems(innerText, productId, itemNodeList, productSet, mappingSet);
                dataRow["NewSkuId"] = (hasSku ? text : "0");
                productSet.Tables["skus"].Rows.Add(dataRow);
            }
        }
        private string loadSkuItems(string mappedSkuId, int mappedProductId, XmlNodeList itemNodeList, DataSet productSet, DataSet mappingSet)
        {
            if (itemNodeList == null || itemNodeList.Count == 0)
            {
                return "0";
            }
            string text = "";
            foreach (XmlNode xmlNode in itemNodeList)
            {
                text = text + mappingSet.Tables["values"].Select("MappedValueId=" + xmlNode.SelectSingleNode("valueId").InnerText)[0]["SelectedValueId"].ToString() + "_";
            }
            text = text.Substring(0, text.Length - 1);
            foreach (XmlNode xmlNode2 in itemNodeList)
            {
                int num = int.Parse(xmlNode2.SelectSingleNode("attributeId").InnerText);
                int num2 = int.Parse(xmlNode2.SelectSingleNode("valueId").InnerText);
                DataRow[] array = mappingSet.Tables["attributes"].Select("MappedAttributeId=" + num);
                DataRow[] array2 = mappingSet.Tables["values"].Select("MappedValueId=" + num2);
                if (array != null && array.Length > 0 && array2 != null && array2.Length > 0)
                {
                    int num3 = (int)array[0]["SelectedAttributeId"];
                    int num4 = (int)array2[0]["SelectedValueId"];
                    DataRow dataRow = productSet.Tables["skuItems"].NewRow();
                    dataRow["MappedProductId"] = mappedProductId;
                    dataRow["NewSkuId"] = text;
                    dataRow["MappedSkuId"] = mappedSkuId;
                    dataRow["SelectedAttributeId"] = num3;
                    dataRow["MappedAttributeId"] = num;
                    dataRow["SelectedValueId"] = num4;
                    dataRow["MappedValueId"] = num2;
                    productSet.Tables["skuItems"].Rows.Add(dataRow);
                }
            }
            return text;
        }
        private void loadProductAttributes(int productId, XmlNodeList attributeNodeList, DataSet productSet, DataSet mappingSet)
        {
            if (attributeNodeList == null || attributeNodeList.Count == 0)
            {
                return;
            }
            foreach (XmlNode xmlNode in attributeNodeList)
            {
                int num = int.Parse(xmlNode.SelectSingleNode("attributeId").InnerText);
                int num2 = int.Parse(xmlNode.SelectSingleNode("valueId").InnerText);
                DataRow[] array = mappingSet.Tables["attributes"].Select("MappedAttributeId=" + num);
                DataRow[] array2 = mappingSet.Tables["values"].Select("MappedValueId=" + num2);
                if (array != null && array.Length > 0 && array2 != null && array2.Length > 0)
                {
                    int num3 = (int)array[0]["SelectedAttributeId"];
                    int num4 = (int)array2[0]["SelectedValueId"];
                    DataRow dataRow = productSet.Tables["attributes"].NewRow();
                    dataRow["ProductId"] = productId;
                    dataRow["SelectedAttributeId"] = num3;
                    dataRow["MappedAttributeId"] = num;
                    dataRow["SelectedValueId"] = num4;
                    dataRow["MappedValueId"] = num2;
                    productSet.Tables["attributes"].Rows.Add(dataRow);
                }
            }
        }
        private DataSet GetProductSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("products");
            dataTable.Columns.Add(new DataColumn("ProductId")
            {
                Unique = true,
                DataType = Type.GetType("System.Int32")
            });
            dataTable.Columns.Add(new DataColumn("SelectedTypeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable.Columns.Add(new DataColumn("MappedTypeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable.Columns.Add(new DataColumn("ProductName")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("ProductCode")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("ShortDescription")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("Unit")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("Description")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("Title")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("Meta_Description")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("Meta_Keywords")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("SaleStatus")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable.Columns.Add(new DataColumn("Image")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("ImageUrl1")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("ImageUrl2")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("ImageUrl3")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("ImageUrl4")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("ImageUrl5")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable.Columns.Add(new DataColumn("MarketPrice")
            {
                DataType = Type.GetType("System.Decimal")
            });
            dataTable.Columns.Add(new DataColumn("LowestSalePrice")
            {
                DataType = Type.GetType("System.Decimal")
            });
            dataTable.Columns.Add(new DataColumn("PenetrationStatus")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable.Columns.Add(new DataColumn("HasSKU")
            {
                DataType = Type.GetType("System.Boolean")
            });
            dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ProductId"]
			};
            DataTable dataTable2 = new DataTable("attributes");
            dataTable2.Columns.Add(new DataColumn("ProductId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.Columns.Add(new DataColumn("SelectedAttributeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.Columns.Add(new DataColumn("MappedAttributeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.Columns.Add(new DataColumn("SelectedValueId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.Columns.Add(new DataColumn("MappedValueId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable2.PrimaryKey = new DataColumn[]
			{
				dataTable2.Columns["ProductId"],
				dataTable2.Columns["MappedAttributeId"],
				dataTable2.Columns["MappedValueId"]
			};
            DataTable dataTable3 = new DataTable("skus");
            dataTable3.Columns.Add(new DataColumn("MappedSkuId")
            {
                Unique = true,
                DataType = Type.GetType("System.String")
            });
            dataTable3.Columns.Add(new DataColumn("NewSkuId")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable3.Columns.Add(new DataColumn("ProductId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("SKU")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable3.Columns.Add(new DataColumn("Weight")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("Stock")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("AlertStock")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable3.Columns.Add(new DataColumn("CostPrice")
            {
                DataType = Type.GetType("System.Decimal")
            });
            dataTable3.Columns.Add(new DataColumn("SalePrice")
            {
                DataType = Type.GetType("System.Decimal")
            });
            dataTable3.Columns.Add(new DataColumn("PurchasePrice")
            {
                DataType = Type.GetType("System.Decimal")
            });
            dataTable3.PrimaryKey = new DataColumn[]
			{
				dataTable3.Columns["MappedSkuId"]
			};
            DataTable dataTable4 = new DataTable("skuItems");
            dataTable4.Columns.Add(new DataColumn("MappedSkuId")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable4.Columns.Add(new DataColumn("NewSkuId")
            {
                DataType = Type.GetType("System.String")
            });
            dataTable4.Columns.Add(new DataColumn("MappedProductId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable4.Columns.Add(new DataColumn("SelectedAttributeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable4.Columns.Add(new DataColumn("MappedAttributeId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable4.Columns.Add(new DataColumn("SelectedValueId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable4.Columns.Add(new DataColumn("MappedValueId")
            {
                DataType = Type.GetType("System.Int32")
            });
            dataTable4.PrimaryKey = new DataColumn[]
			{
				dataTable4.Columns["MappedSkuId"],
				dataTable4.Columns["MappedAttributeId"]
			};
            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(dataTable2);
            dataSet.Tables.Add(dataTable3);
            dataSet.Tables.Add(dataTable4);
            return dataSet;
        }
    }
}
