namespace SigortaApp.Extensions
{
    public static class Extensions
    {
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static string TrimSafe(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty : str.Trim();
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsEmpty<T>(this List<T> list)
        {
            return list != null && list.Count == 0;
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (list.IsNullOrEmpty())
                throw new ArgumentException("list is null or empty");

            Random rnd = new Random();
            return list[rnd.Next(list.Count)];
        }

        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static int DaysBetween(this DateTime from, DateTime to)
        {
            return (to - from).Days;
        }

        public static string ToPercent(this double value)
        {
            return (value * 100).ToString("0.##") + "%";
        }

        public static bool IsNegative(this int value)
        {
            return value < 0;
        }

        public static bool IsPositive(this int value)
        {
            return value > 0;
        }
    }
}
