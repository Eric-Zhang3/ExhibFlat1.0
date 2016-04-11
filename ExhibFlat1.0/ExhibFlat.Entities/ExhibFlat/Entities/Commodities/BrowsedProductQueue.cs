namespace ExhibFlat.Entities.Commodities
{
    using ExhibFlat.Core;
    
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class BrowsedProductQueue
    {
        private static string browedProductList = "BrowedProductList-Admin";
        private static string browedProductListMoblie = "BrowedProductList_Moblie";

        static BrowsedProductQueue()
        {
           
        }

        public static void ClearQueue()
        {
            SaveCookie(null);
        }

        public static void EnQueue(int productId)
        {
            IList<int> browedProductList = GetBrowedProductList();
            int index = 0;
            foreach (int num2 in browedProductList)
            {
                if (productId == num2)
                {
                    browedProductList.RemoveAt(index);
                    break;
                }
                index++;
            }
            if (browedProductList.Count <= 20)
            {
                browedProductList.Add(productId);
            }
            else
            {
                browedProductList.RemoveAt(0);
                browedProductList.Add(productId);
            }
            SaveCookie(browedProductList);
        }

        public static IList<int> GetBrowedProductList()
        {
            IList<int> list = new List<int>();

          
            return list;
        }

        public static IList<int> GetMobileProductList(HttpCookie cookie)
        {
            string str = cookie.Value;
            string strEm = str.Replace("#","");
            string[] items = strEm.Split(new char[] { ',' });
            List<int> products = new List<int>();
            foreach(string ids in items)
            {
                if (!ids.Equals(""))
                {
                    try
                    {
                        products.Add(int.Parse(ids));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return products;

        }

        public static IList<int> GetBrowedProductList(int maxNum)
        {
            IList<int> browedProductList = GetBrowedProductList();
            int count = browedProductList.Count;
            if (browedProductList.Count > maxNum)
            {
                for (int i = 0; i < (count - maxNum); i++)
                {
                    browedProductList.RemoveAt(0);
                }
            }
            return browedProductList;
        }

        private static void SaveCookie(IList<int> productIdList)
        {
           
           
           
        }
    }
}

