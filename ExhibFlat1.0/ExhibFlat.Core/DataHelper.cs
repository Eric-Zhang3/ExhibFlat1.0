namespace ExhibFlat.Core
{
    using ExhibFlat.Core.Entities;
    using ExhibFlat.Core.Enums;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class DataHelper
    {

        public static string BuildNotinQuery(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string key, string filter, string selectFields)
        {
            string str = string.IsNullOrEmpty(filter) ? "" : ("WHERE " + filter);
            string str2 = string.IsNullOrEmpty(filter) ? "" : ("AND " + filter);
            string str3 = string.IsNullOrEmpty(sortBy) ? "" : ("ORDER BY " + sortBy + " " + sortOrder.ToString());
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP {0} {1} FROM {2} ", pageSize.ToString(CultureInfo.InvariantCulture), selectFields, table);
            if (pageIndex == 1)
            {
                builder.AppendFormat("{0} {1}", str, str3);
            }
            else
            {
                int num = (pageIndex - 1) * pageSize;
                builder.AppendFormat("WHERE {0} NOT IN (SELECT TOP {1} {0} FROM {2} {3} {4}) {5} {4}", new object[] { key, num, table, str, str3, str2 });
            }
            if (isCount)
            {
                builder.AppendFormat(";SELECT COUNT({0}) FROM {1} {2}", key, table, str);
            }
            return builder.ToString();
        }


        private static string BuildTopQuery(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {
            string str = string.IsNullOrEmpty(sortBy) ? pk : sortBy;
            string str2 = string.IsNullOrEmpty(filter) ? "" : ("WHERE " + filter);
            string str3 = string.IsNullOrEmpty(filter) ? "" : ("AND " + filter);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP {0} {1} FROM {2} ", pageSize.ToString(CultureInfo.InvariantCulture), selectFields, table);
            if (table.Equals("ExhibFlat_Orders"))
            {
                if (pageIndex == 1)
                {
                    builder.AppendFormat("{0} and JSC_CODE is null ORDER BY {1} {2}", str2, str, sortOrder.ToString());
                }
                else
                {
                    int num = (pageIndex - 1) * pageSize;
                    if (sortOrder == SortAction.Asc)
                    {
                        builder.AppendFormat("WHERE {0} > (SELECT MAX({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} ORDER BY {0} ASC) AS TMP) {4} and JSC_CODE is null ORDER BY {0} ASC", new object[] { str, num, table, str2, str3 });
                    }
                    else
                    {
                        builder.AppendFormat("WHERE {0} < (SELECT MIN({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} and JSC_CODE is  null ORDER BY {0} DESC) AS TMP) {4} and JSC_CODE is null ORDER BY {0} DESC", new object[] { str, num, table, str2, str3 });
                    }
                }
                if (isCount)
                {
                    builder.AppendFormat(";SELECT COUNT({0}) FROM {1} {2} and JSC_CODE is null ", str, table, str2);
                }
            }
            else
            {
                if (pageIndex == 1)
                {
                    builder.AppendFormat("{0} ORDER BY {1} {2}", str2, str, sortOrder.ToString());
                }
                else
                {
                    int num = (pageIndex - 1) * pageSize;
                    if (sortOrder == SortAction.Asc)
                    {
                        builder.AppendFormat("WHERE {0} > (SELECT MAX({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} ORDER BY {0} ASC) AS TMP) {4}  ORDER BY {0} ASC", new object[] { str, num, table, str2, str3 });
                    }
                    else
                    {
                        builder.AppendFormat("WHERE {0} < (SELECT MIN({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} ORDER BY {0} DESC) AS TMP) {4}  ORDER BY {0} DESC", new object[] { str, num, table, str2, str3 });
                    }
                }
                if (isCount)
                {
                    builder.AppendFormat(";SELECT COUNT({0}) FROM {1} {2}  ", str, table, str2);
                }
            }


            return builder.ToString();
        }

        //private Database database = DatabaseFactory.CreateDatabase();
        private static string BuildTopQuery2(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {

            DbCommand sqlStringCommand = DatabaseFactory.CreateDatabase().GetSqlStringCommand(@"select t0.OrderId,t0.Remark,t1.SKU,t2.ProductName,t0.BundlingNum,t3.CompanyName,t0.ShipOrderNumber,t0.OrderStatus,t0.Amount
                                ,t1.ItemListPrice,t0.ShipTo,t0.TelPhone,t0.Address,t0.ShipToDate,t0.Remark,t0.OrderTotal,t0.UserId,t0.UserName,
                                t0.Freight,t0.OrderDate,t0.PayDate,t0.ShippingDate,t0.FinishDate,t0.ShipToDate,t0.BreachStatus,t0.JSC_Code                                
                                 from ExhibFlat_Orders t0 join  ExhibFlat_OrderItems t1 on t0.OrderId=t1.OrderId
								join ExhibFlat_Products t2 on t1.ProductId = t2.ProductId
								join ExhibFlat_Suppliers t3 on t2.SupplierId=t3.SupplierId");
            DataSet ds = DatabaseFactory.CreateDatabase().ExecuteDataSet(sqlStringCommand);
            DataTable dt = ds.Tables[0];
            string str = string.IsNullOrEmpty(sortBy) ? pk : sortBy;
            string str2 = string.IsNullOrEmpty(filter) ? "" : (" WHERE t0." + filter);
            string str3 = string.IsNullOrEmpty(filter) ? "" : ("AND t0." + filter);
            string str4 = string.IsNullOrEmpty(filter) ? "" : ("where" + filter);
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(@"select top {0} t0.OrderId,t0.Remark,t1.SKU,t2.ProductName,t0.BundlingNum,t3.CompanyName,t0.ShipOrderNumber,t0.OrderStatus,t0.Amount ,t1.ItemListPrice,t0.ShipTo,t0.TelPhone,t0.Address,t0.ShipToDate,t0.OrderTotal,t0.UserId,t0.UserName, t0.Freight,t0.OrderDate,t0.PayDate,t0.ShippingDate,t0.FinishDate,t0.BreachStatus,t0.JSC_Code  from ExhibFlat_Orders t0 join  ExhibFlat_OrderItems t1 on t0.OrderId=t1.OrderId 
                                    join ExhibFlat_Products t2 on t1.ProductId = t2.ProductId 	join ExhibFlat_Suppliers t3 on t2.SupplierId=t3.SupplierId ", pageSize.ToString(CultureInfo.InvariantCulture));
            //SELECT TOP {0} {1} FROM {2} ", pageSize.ToString(CultureInfo.InvariantCulture), selectFields, "()");
            if (pageIndex == 1)
            {
                builder.AppendFormat("{0} ORDER BY {1} {2}", str2, str, sortOrder.ToString());
                // builder.AppendFormat("ORDER BY {1} {2}", str2, str, sortOrder.ToString());
            }
            else
            {
                int num = (pageIndex - 1) * pageSize;
                if (sortOrder == SortAction.Asc)
                {
                    builder.AppendFormat("WHERE {0} > (SELECT MAX({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} ORDER BY {0} ASC) AS TMP) {4} ORDER BY {0} ASC", new object[] { str, num, table, str2, str3 });
                }
                else
                {
                    builder.AppendFormat("WHERE {0} < (SELECT MIN({0}) FROM (SELECT TOP {1} {0} FROM {2} {3} ORDER BY {0} DESC) AS TMP) {4} ORDER BY {0} DESC", new object[] { str, num, table, str2, str3 });
                }
            }
            if (isCount)
            {
                builder.AppendFormat(";SELECT COUNT({0}) FROM {1} {2}", str, table, str4);
            }
            return builder.ToString();
        }

        public static string CleanSearchString(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return null;
            }
            searchString = searchString.Replace("*", "%");
            searchString = Globals.StripHtmlXmlTags(searchString);
            searchString = Regex.Replace(searchString, "--|;|'|\"", " ", RegexOptions.Compiled | RegexOptions.Multiline);
            searchString = Regex.Replace(searchString, " {1,}", " ", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return searchString;
        }

        public static DataTable ConverDataReaderToDataTable(IDataReader reader)
        {
            if (null == reader)
            {
                return null;
            }
            DataTable table = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            int fieldCount = reader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }
            table.BeginLoadData();
            object[] values = new object[fieldCount];
            while (reader.Read())
            {
                reader.GetValues(values);
                table.LoadDataRow(values, true);
            }
            table.EndLoadData();
            return table;
        }

        public static string DateComparerString(int dateComparer)
        {
            switch (dateComparer)
            {
                case -1:
                    return "<";

                case 0:
                    return "=";

                case 1:
                    return ">";
            }
            return "=";
        }

        public static string GetSafeDateTimeFormat(DateTime date)
        {
            return date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.SortableDateTimePattern, CultureInfo.InvariantCulture);
        }

        //书签
        public static DbQueryResult PagingByRownumber(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {
            return PagingByRownumber(pageIndex, pageSize, sortBy, sortOrder, isCount, table, pk, filter, selectFields, 0);
        }

        //书签
        public static DbQueryResult PagingByRownumber(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields, int partitionSize)
        {
            if (string.IsNullOrEmpty(table))
            {
                return null;
            }
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(pk))
            {
                return null;
            }
            if (string.IsNullOrEmpty(selectFields))
            {
                selectFields = "*";
            }
            string query = BuildRownumberQuery(sortBy, sortOrder, isCount, table, pk, filter, selectFields, partitionSize);
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = (num + pageSize) - 1;
            DbQueryResult result = new DbQueryResult();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            database.AddInParameter(sqlStringCommand, "StartNumber", DbType.Int32, num);
            database.AddInParameter(sqlStringCommand, "EndNumber", DbType.Int32, num2);
            using (IDataReader reader = database.ExecuteReader(sqlStringCommand))
            {
                result.Data = ConverDataReaderToDataTable(reader);
                if ((isCount && (partitionSize == 0)) && reader.NextResult())
                {
                    reader.Read();
                    result.TotalRecords = reader.GetInt32(0);
                }
            }
            return result;
        }


        private static string BuildRownumberQuery(string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields, int partitionSize)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.IsNullOrEmpty(filter) ? "" : ("WHERE " + filter);
            if (partitionSize > 0)
            {
                builder.AppendFormat("SELECT TOP {0} {1}, ROW_NUMBER() OVER (ORDER BY ", partitionSize.ToString(CultureInfo.InvariantCulture), selectFields);
            }
            else
            {
                builder.AppendFormat("SELECT {0} , ROW_NUMBER() OVER (ORDER BY ", selectFields);
            }
            builder.AppendFormat("{0} {1}", string.IsNullOrEmpty(sortBy) ? pk : sortBy, sortOrder.ToString());
            builder.AppendFormat(") AS RowNumber FROM {0} {1}", table, str);
            builder.Insert(0, "SELECT * FROM (").Append(") T WHERE T.RowNumber BETWEEN @StartNumber AND @EndNumber");
            if (isCount && (partitionSize == 0))
            {
                builder.AppendFormat(";SELECT COUNT({0}) FROM {1} {2}", string.IsNullOrEmpty(sortBy) ? pk : sortBy, table, str);
            }
            return builder.ToString();
        }



        public static DbQueryResult PagingByTopnotin(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string key, string filter, string selectFields)
        {
            if (string.IsNullOrEmpty(table))
            {
                return null;
            }
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            if (string.IsNullOrEmpty(selectFields))
            {
                selectFields = "*";
            }
            string query = BuildNotinQuery(pageIndex, pageSize, sortBy, sortOrder, isCount, table, key, filter, selectFields);
            DbQueryResult result = new DbQueryResult();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            using (IDataReader reader = database.ExecuteReader(sqlStringCommand))
            {
                result.Data = ConverDataReaderToDataTable(reader);
                if (isCount && reader.NextResult())
                {
                    reader.Read();
                    result.TotalRecords = reader.GetInt32(0);
                }
            }
            return result;
        }

        public static DbQueryResult PagingByTopsort(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {
            if (string.IsNullOrEmpty(table))
            {
                return null;
            }
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(pk))
            {
                return null;
            }
            if (string.IsNullOrEmpty(selectFields))
            {
                selectFields = "*";
            }
            string query = BuildTopQuery(pageIndex, pageSize, sortBy, sortOrder, isCount, table, pk, filter, selectFields);
            DbQueryResult result = new DbQueryResult();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            using (IDataReader reader = database.ExecuteReader(sqlStringCommand))
            {
                result.Data = ConverDataReaderToDataTable(reader);
                if (isCount && reader.NextResult())
                {
                    reader.Read();
                    result.TotalRecords = reader.GetInt32(0);
                }
            }
            return result;
        }
        public static DbQueryResult PagingByTopsort2(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {
            if (string.IsNullOrEmpty(table))
            {
                return null;
            }
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(pk))
            {
                return null;
            }
            if (string.IsNullOrEmpty(selectFields))
            {
                selectFields = "*";
            }

            string query = BuildTopQuery2(pageIndex, pageSize, sortBy, sortOrder, isCount, table, pk, filter, selectFields);
            DbQueryResult result = new DbQueryResult();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            using (IDataReader reader = database.ExecuteReader(sqlStringCommand))
            {
                result.Data = ConverDataReaderToDataTable(reader);
                if (isCount && reader.NextResult())
                {
                    reader.Read();
                    result.TotalRecords = reader.GetInt32(0);
                }
            }
            return result;
        }
        public static bool SwapSequence(string table, string keyField, string sequenceField, int key, int replaceKey, int sequence, int replaceSequence)
        {
            string query = string.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}", new object[] { table, sequenceField, replaceSequence, keyField, key }) + string.Format(" UPDATE {0} SET {1} = {2} WHERE {3} = {4}", new object[] { table, sequenceField, sequence, keyField, replaceKey });
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            return (database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        /// <summary>
        /// 获取需要结算佣金的渠道商数据
        /// </summary>
        /// <param name="strUserIDs">需要结算的渠道商ID</param>
        /// <param name="strDateLimit">需要结算佣金产生的日期</param>
        /// <returns>DataTable</returns>
        public static DataTable GetSettleData(string strUserIDs, string strDateLimit)
        {
            string strDates = string.Empty;
            if (string.IsNullOrEmpty(strUserIDs))
            {
                throw new Exception("结算用户ID不能为空");
            }
            if (!string.IsNullOrEmpty(strDateLimit))
            {
                List<string> limitDates = new List<string>();
                string[] arrDateLimit = strDateLimit.Split(',');
                for (int i = 0; i < arrDateLimit.Length; i++)
                {
                    if (!limitDates.Contains("'" + arrDateLimit[i] + "'"))
                    {
                        limitDates.Add("'" + arrDateLimit[i] + "'");
                    }
                }
                strDates = string.Join(",", limitDates.ToArray());
            }
            else
            {
                throw new Exception("需要结算的日期不能为空");
            }

            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select [UserID],[ChannelName],sum([EarningTotal]) as EarningTotal FROM [ExhibFlat_ChannelRpt] where 1=1 ");
            sbSql.Append(" and [UserID] in (" + strUserIDs + ")");
            sbSql.Append(" and CONVERT( varchar(10),[CreateDate],23) in(" + strDates + ")");
            sbSql.Append(" and  [PayStatus]=0 group by [UserID],[ChannelName]");
            Database database = DatabaseFactory.CreateDatabase();
            return database.ExecuteDataSet(CommandType.Text, sbSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取需要结算佣金的渠道商数据
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetSettleData(string IDList)
        {
            string strDates = string.Empty;
            if (string.IsNullOrEmpty(IDList))
            {
                throw new Exception("结算记录ID不能为空");
            }

            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select [UserID],[ChannelName],[EarningTotal] FROM [ExhibFlat_ChannelRpt] where 1=1 ");
            sbSql.Append(" and [ID] in (" + IDList + ")");
            sbSql.Append(" and [PayStatus]=0");
            Database database = DatabaseFactory.CreateDatabase();
            return database.ExecuteDataSet(CommandType.Text, sbSql.ToString()).Tables[0];
        }

        public static DbQueryResult CteRownumber(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields)
        {
            return CteRownumber(pageIndex, pageSize, sortBy, sortOrder, isCount, table, pk, filter, selectFields, 0);
        }

        public static DbQueryResult CteRownumber(int pageIndex, int pageSize, string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields, int partitionSize)
        {
            if (string.IsNullOrEmpty(table))
            {
                return null;
            }
            if (string.IsNullOrEmpty(sortBy) && string.IsNullOrEmpty(pk))
            {
                return null;
            }
            if (string.IsNullOrEmpty(selectFields))
            {
                selectFields = "*";
            }
            string Select = CteRownumberQuery(sortBy, sortOrder, isCount, table, pk, filter, selectFields, partitionSize);
            string query = ";with cte as(select convert(nvarchar(50), UserID) as sortStr,* from vw_aspnet_Chanels where ParentID=0 or ParentID is null union all select convert(nvarchar(50),convert(nvarchar(6), isnull(a.ParentID,0))+'-'+convert(nvarchar(6),isnull(a.UserID,0))) as sortStr,a.* from vw_aspnet_Chanels a join cte b on a.ParentID=b.UserID) " + Select;

            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = (num + pageSize) - 1;
            DbQueryResult result = new DbQueryResult();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            database.AddInParameter(sqlStringCommand, "StartNumber", DbType.Int32, num);
            database.AddInParameter(sqlStringCommand, "EndNumber", DbType.Int32, num2);
            using (IDataReader reader = database.ExecuteReader(sqlStringCommand))
            {
                result.Data = ConverDataReaderToDataTable(reader);
                if ((isCount && (partitionSize == 0)) && reader.NextResult())
                {
                    reader.Read();
                    result.TotalRecords = reader.GetInt32(0);
                }
            }
            return result;
        }

        private static string CteRownumberQuery(string sortBy, SortAction sortOrder, bool isCount, string table, string pk, string filter, string selectFields, int partitionSize)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.IsNullOrEmpty(filter) ? "" : ("WHERE " + filter);
            if (partitionSize > 0)
            {
                builder.AppendFormat("SELECT TOP {0} {1}, ROW_NUMBER() OVER (ORDER BY ", partitionSize.ToString(CultureInfo.InvariantCulture), selectFields);
            }
            else
            {
                builder.AppendFormat("SELECT {0} , ROW_NUMBER() OVER (ORDER BY ", selectFields);
            }
            builder.AppendFormat("{0} {1}", string.IsNullOrEmpty(sortBy) ? pk : sortBy, sortOrder.ToString());
            builder.AppendFormat(") AS RowNumber FROM {0} {1}", table, str);
            builder.Insert(0, "SELECT * FROM (").Append(") T WHERE T.RowNumber BETWEEN @StartNumber AND @EndNumber ORDER BY sortStr");
            if (isCount && (partitionSize == 0))
            {
                builder.AppendFormat(@";with cte as(select convert(nvarchar(50), UserID) as sortStr,* from vw_aspnet_Chanels 
                                        where ParentID=0 or ParentID is null union
                                         all select convert(nvarchar(50),convert(nvarchar(6), isnull(a.ParentID,0))+'-'+convert(nvarchar(6),isnull(a.UserID,0))) as sortStr,a.* 
                                         from vw_aspnet_Chanels a join cte b on a.ParentID=b.UserID) SELECT COUNT({0}) FROM cte {1}", string.IsNullOrEmpty(sortBy) ? pk : sortBy, str);
            }
            return builder.ToString();
        }

        public static string GetSupplierAccountDetailsQuery(int pageindex, int pagesize, string chanelname /*渠道商名称*/ , string suppliername /*联盟商户名称*/)
        {
            //条件1
            StringBuilder condition = new StringBuilder();
            //条件2->用于插叙数据总数的条件。
            StringBuilder condition2 = new StringBuilder();

            //数据查询
            StringBuilder query = new StringBuilder(" SELECT TOP " + pagesize + " * FROM vw_ExhibFlat_SupplierAcountDetails ");
            //+ " WHERE rowId > (SELECT max(rowId) FROM (SELECT TOP 10 rowId FROM vw_ExhibFlat_SupplierAcountDetails  ) AS TMP)"
            //+ " select count(*) from vw_ExhibFlat_SupplierAcountDetails ");
            StringBuilder amountQuery = new StringBuilder(" select count(*) from vw_ExhibFlat_SupplierAcountDetails ");
            //没有查询条件
            if (chanelname == "" && suppliername == "" && pageindex == 1)
            {
                query.Append(";");
            }
            else
            {
                //渠道商不为空
                if (chanelname != "") 
                {
                    if (pageindex == 1) 
                    {
                        condition.Append(string.Format("where chanelname='{0}'", chanelname));
                    }
                    else 
                    {
                        condition.Append(string.Format("AND chanelname='{0}'", chanelname));
                    }
                    condition2.Append(string.Format("where chanelname='{0}'", chanelname));
                } 
                //联盟商户不为空
                if (suppliername != "")
                {
                    //渠道商不为空
                    if (chanelname != "")
                    {
                        condition = condition.Append(string.Format("  AND suppliername='{0}';", suppliername.ToString()));
                        condition2 = condition2.Append(string.Format("  AND suppliername='{0}';", suppliername.ToString()));
                    }
                    else
                    //渠道商为空
                    {
                        if (pageindex == 1) 
                        {
                            condition = condition.Append(string.Format("  WHERE suppliername='{0}';", suppliername.ToString()));
                        }
                        else 
                        {
                            condition = condition.Append(string.Format("  AND suppliername='{0}';", suppliername.ToString()));
                        }
                        condition2 = condition2.Append(string.Format("  Where suppliername='{0}';", suppliername.ToString()));
                    }
                }
                amountQuery.Append(condition2.ToString());
            }
            //第一页
            if (pageindex == 1)
            {
                query.Append(condition.ToString());
                query.Append(amountQuery.ToString());
            }
            else
            {
                if (chanelname == "" && suppliername == "") 
                {
                    query.Append(String.Format(" WHERE rowId > (SELECT max(rowId) FROM (SELECT TOP {0} rowId FROM vw_ExhibFlat_SupplierAcountDetails  ) AS TMP);", ((pageindex - 1) * pagesize).ToString()));
                }
                else 
                {
                    query.Append(String.Format(" WHERE rowId > (SELECT max(rowId) FROM (SELECT TOP {0} rowId FROM vw_ExhibFlat_SupplierAcountDetails  ) AS TMP) ", ((pageindex - 1) * pagesize).ToString()));
                }
                query.Append(condition.ToString());
                query.Append(amountQuery.ToString());
            }
            return query.ToString();

        }

    }
}

