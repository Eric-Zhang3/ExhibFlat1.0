﻿namespace ExhibFlat.Membership.Context
{
    using ExhibFlat.Core;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;

    public static class LicenseChecker
    {
        private const string CacheCommercialKey = "FileCache_CommercialLicenser";

        public static void Check(out bool isValid, out bool expired, out int siteQty)
        {
            //直接授权
            isValid = true;
            expired = false;
            siteQty = 100000000;
        }
    }
}

