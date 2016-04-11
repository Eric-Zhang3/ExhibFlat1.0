namespace ExhibFlat.Entities.Sales
{
    using ExhibFlat.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SupplierAccountQuery : Pagination
    {
        private String chanelname = String.Empty;

        private String suppliername = String.Empty;

        public int PageSizeer = 1;

        public string ChanelName
        {
            get
            {
                if (chanelname == String.Empty || chanelname == null)
                {
                    return "";
                }
                else { return chanelname; }
            }

            set
            {
                chanelname = value;
            }
        }

        public string SupplierName
        {
            get
            {
                if (suppliername == String.Empty || suppliername == null)
                {
                    return "";
                }
                else { return suppliername; }
            }
            set
            {
                suppliername = value;
            }
        }

    }
}

