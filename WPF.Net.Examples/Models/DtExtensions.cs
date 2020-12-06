using System;

namespace WPF.Net.Examples.Models
{
    public static class DtExtensions
    {
        public static string LogFormat(this DateTime dt)
        {
            dt = DateTime.Now;
            return $"[{dt.Year:D2}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}:{dt.Second:D3}]";
        }
    }
}
