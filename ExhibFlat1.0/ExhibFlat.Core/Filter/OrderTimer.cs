using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hidistro.Entities.Sales;
using Hidistro.AccountCenter.Business;
using Hidistro.Core.Entities;
using Hidistro.Membership.Context;
using Hidistro.Membership.Core.Enums;
using Hidistro.Entities.JSC;
using Hidistro.Entities.Sales;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.Core.Entities;
using System.Data.SqlClient;
using Hidistro.UI.SaleSystem.Tags;
using Hidistro.Entities.JSC;
using Hidistro.ControlPanel.Sales;
namespace Hidistro.UI.Web.Filter
{
    public class OrderTimer
    {
        //JS_DealMainIBLL dealmain = new JS_DealMainBLL();

        private System.Collections.Generic.List<OrderInfo> list; 

        private static OrderTimer Instance;

        private OrderComp oc;

        private System.Threading.Thread thr;

        private bool bRun = true;

        private OrderTimer()
        {
            list = new List<OrderInfo>();
            oc= new OrderComp();
        }



        public static OrderTimer GetInstace()
        {
            if (Instance == null)
            {
                Instance = new OrderTimer();
                Instance.init();
            }

            return Instance;
        }


        private void init()
        {

            foreach (OrderInfo item in SalesHelper.GetOrderWaitPay())
            {
                addOrderItem(item);
                //return Content(JsonHelper.ObjectToJson(item));
            }
            //读未支付订单记录
            //将订单记录形成 orderItem装载到  list中。
                list.Sort(oc);
        }


        /// <summary>
        /// 启动定时器
        /// </summary>
        public void Start()
        {
            thr = new System.Threading.Thread(new System.Threading.ThreadStart(this.run));
            thr.Start();
        }


        private void run()
        {
            lock (this) 
            { 
            while (bRun)
            {
                if (list.Count == 0)
                {
                    System.Threading.Monitor.Wait(this);
                }
                else
                {
                    int it = (list[0].OrderDate.AddMinutes(20) - DateTime.Now).Milliseconds;
                    if (it > 0)
                    {
                        System.Threading.Monitor.Wait(this,it);
                        continue;
                    }

                    //执行
                    OrderInfo item = list[0];
                    updateOrder(item.OrderId);
                    list.RemoveAt(0);

                }
            }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="orderNo"></param>
        private void updateOrder(string orderNo)
        {
            int i = TradeHelper.GetOrderInfo(orderNo).OrderStatusInt;
            if (i == 1)
            {
                OrderHelper.UpdateOrders(orderNo, 4, "订单未按时完成付款");
                //dealmain.UpdateExecStatusByWait(orderNo);
            }
        }

        public void addOrderItem(OrderInfo item)
        {
            lock (this)
            {
                list.Add(item);
                list.Sort(oc);

                System.Threading.Monitor.PulseAll(this);
            }
        }


        public void Close()
        {
            lock (this)
            {
                bRun = false;
                System.Threading.Monitor.PulseAll(this);
            }
        }


    }
}