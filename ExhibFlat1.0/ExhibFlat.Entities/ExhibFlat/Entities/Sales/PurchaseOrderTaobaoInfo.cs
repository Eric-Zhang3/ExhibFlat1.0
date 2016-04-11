namespace ExhibFlat.Entities.Sales
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Text;

    [DataContract]
    public class PurchaseOrderTaobaoInfo
    {
        public PurchaseOrderTaobaoInfo()
        {
            this.created = "false";
            this.expire_time = "0";
            this.isPart = "false";
            this.is_delivery = "false";
            this.logi_name = "";
            this.login_no = "";
            this.order_id = "";
            this.status = "未下单";
            this.time = "";
        }

        public string ToJson()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"order_id\":\"");
            builder.Append(this.order_id);
            builder.Append("\",\"created\":\"");
            builder.Append(this.created);
            builder.Append("\",\"expire_time\":\"");
            builder.Append(this.expire_time);
            builder.Append("\",\"isPart\":\"");
            builder.Append(this.isPart);
            builder.Append("\",\"is_delivery\":\"");
            builder.Append(this.is_delivery);
            builder.Append("\",\"logi_name\":\"");
            builder.Append(this.logi_name);
            builder.Append("\",\"login_no\":\"");
            builder.Append(this.login_no);
            builder.Append("\",\"status\":\"");
            builder.Append(this.status);
            builder.Append("\",\"time\":\"");
            builder.Append(this.time);
            builder.Append("\"}");
            return builder.ToString();
        }

        [DataMember]
        public string created { get; set; }

        [DataMember]
        public string expire_time { get; set; }

        [DataMember]
        public string is_delivery { get; set; }

        [DataMember]
        public string isPart { get; set; }

        [DataMember]
        public string logi_name { get; set; }

        [DataMember]
        public string login_no { get; set; }

        [DataMember]
        public string order_id { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string time { get; set; }
    }
}

