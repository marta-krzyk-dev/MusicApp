﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Helpers
{
    public static class HtmlHelpers
    {
        public static string Truncate(this HtmlHelper helper, string input, int length=25)
        {
            if (input.Length > length)
            {
                return input.Substring(0, length) + "...";
            }
            else
            {
                return input;
            }
        }
    }
}