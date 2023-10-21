namespace microscore.infrastructure.utils
{
    public static class DateTimeExtension
    {
        public static DateTime NowEC(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().AddHours(-5);
        }
    }
}
