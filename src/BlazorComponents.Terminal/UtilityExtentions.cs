using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponents.Terminal
{
    public static class UtilityExtentions
    {
        public static bool IsEmtpyOrWhiteSpace(this Char c)
        {
            return string.IsNullOrWhiteSpace(c.ToString());
        }

        public static MarkupString ToRawHtml(this string html)
        {
            return (MarkupString)html;
        }

        public static string Repeat(this string str,int count)
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            while (i <= count)
            {
                sb.Append(str);
                i++;
            };

            return sb.ToString();

        }

    }
}
