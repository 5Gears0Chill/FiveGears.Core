using System;

namespace FiveGears.Core.Extensions.UtilityExtensions
{
    public static class IsNullExtension
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static bool IsDefualtDate(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue;
        }

        public static bool IsNotDefualtDate(this DateTime dateTime)
        {
            return !dateTime.IsDefualtDate();
        }
    }
}
