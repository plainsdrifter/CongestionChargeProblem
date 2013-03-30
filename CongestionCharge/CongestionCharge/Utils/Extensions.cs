using System;

namespace CongestionCharge.Utils
{
    public static class Extensions
    {
        public static bool IsWeekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }

        public static bool IsFreeOfCharge(this DateTime date)
        {
            return (date.Hour >= 19 || date.Hour < 7);
        }

        public static bool InAmRate(this DateTime date)
        {
            return (date.Hour >= 7 && date.Hour < 12);
        }

        public static bool InPmRate(this DateTime date)
        {
            return (date.Hour >= 12 && date.Hour < 19);
        }
    }
}
