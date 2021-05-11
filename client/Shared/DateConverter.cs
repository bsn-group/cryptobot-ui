using System;

namespace CryptobotUi.Client.Model.Extensions
{
    public static class DateConverter
    {
        public static DateTime? ToLocalTime(this DateTime? possiblyUtc)
        {
            if (possiblyUtc == null) return null;
            if (possiblyUtc.Value.Kind == DateTimeKind.Local) return possiblyUtc;
            return possiblyUtc.Value.ToLocalTime();
        }
    }
    
}