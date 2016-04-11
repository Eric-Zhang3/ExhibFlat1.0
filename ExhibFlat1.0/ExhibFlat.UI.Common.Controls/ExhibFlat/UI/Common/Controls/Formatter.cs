﻿namespace ExhibFlat.UI.Common.Controls
{
    using System;
    using System.Globalization;

    public sealed class Formatter
    {
        private Formatter()
        {
        }

        public static string FormatErrorMessage(string msg)
        {
            return string.Format(CultureInfo.InvariantCulture, "<li>{0}", new object[] { msg });
        }
    }
}

