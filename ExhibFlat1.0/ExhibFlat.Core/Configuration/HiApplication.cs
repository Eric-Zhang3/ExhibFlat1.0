namespace ExhibFlat.Core.Configuration
{
    using ExhibFlat.Core.Enums;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    internal class HiApplication
    {
        private ExhibFlat.Core.Enums.ApplicationType _appType = ExhibFlat.Core.Enums.ApplicationType.Common;
        private string _name;
        private Regex _regex;

        internal HiApplication(string pattern, string name, ExhibFlat.Core.Enums.ApplicationType appType)
        {
            this._name = name.ToLower(CultureInfo.InvariantCulture);
            this._appType = appType;
            this._regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public bool IsMatch(string url)
        {
            return this._regex.IsMatch(url);
        }

        public ExhibFlat.Core.Enums.ApplicationType ApplicationType
        {
            get
            {
                return this._appType;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }
    }
}

