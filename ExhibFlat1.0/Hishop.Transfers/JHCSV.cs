using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Hishop.Transfers
{
    public class JHCSV
    {
        public static DataTable LoadCsv(string filename,System.Text.Encoding encoder,char pfix, int headerLineIndex)
        {
            List<string> dataList = getData(filename, encoder);

            DataTable dt = Getdt(dataList, headerLineIndex, pfix);

            for (int i = headerLineIndex + 1; i < dataList.Count; i++)
            {
                if (dataList[i].Trim().Equals(""))
                {
                    continue;
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    ReadLine(ref dr, dataList[i], pfix);
                    dt.Rows.Add(dr);
                }
            }

            return dt;

        }

        private static DataRow ReadLine(ref DataRow dr, string line, char pfix)
        {
            string[] data = line.Split(new char[] { pfix });

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                if (i >= data.Length)
                {
                    dr[i] = "";
                }
                else
                {
                    dr[i] = data[i].TrimStart('"').TrimEnd('"');
                }
            }

            return dr;

        }


        private static DataTable Getdt(List<string> data, int headIndex,char pfix)
        {
            DataTable dt = new DataTable();
            string[] columns = data[headIndex].Split(new char[] { pfix });
            foreach (string columnname in columns)
            {
                if (columnname.Equals(""))
                {
                    continue;
                }
                else
                {
                    if (dt.Columns[columnname] != null)
                    {
                        dt.Columns.Add(columnname + "1");
                    }
                    else
                    {
                        dt.Columns.Add(columnname);
                    }
                  
                }
            }

            return dt;
        }


        private static List<string> getData(string filename, System.Text.Encoding encoder)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(filename, encoder);
            List<string> dataList = new List<string>();
            string line = sr.ReadLine();
            while (line != null)
            {
                dataList.Add(line);
                line = sr.ReadLine();
            }

            sr.Close();

            return dataList;
        }
    }
}
