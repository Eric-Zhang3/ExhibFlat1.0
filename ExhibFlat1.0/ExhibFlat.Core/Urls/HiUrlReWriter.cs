﻿namespace ExhibFlat.Core.Urls
{
    using ExhibFlat.Core;
    using System;
    using System.Text.RegularExpressions;

    public class HiUrlReWriter : UrlReWriteProvider
    {
        private static Regex ReWriteFilter = null;

        static HiUrlReWriter()
        {
            ReWriteFilter = new Regex(Globals.GetSiteUrls().LocationFilter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public override string RewriteUrl(string path, string queryString)
        {
            string str = null;
            if (!ReWriteFilter.IsMatch(path))
            {
                Location location = Globals.GetSiteUrls().LocationSet.FindLocationByPath(path);
                if (location != null)
                {
                    str = location.ReWriteUrl(path, queryString);
                }
            }
            return str;
        }
    }
}

