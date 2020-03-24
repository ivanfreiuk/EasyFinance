namespace EasyFinance.Constans
{
    public static class RegularExpressions
    {
        public const string DATETIME_PATTERN = @"(\d+)[-.\/](\d+)[-.\/](\d+)\s+((?:0?[0-9]|1[0-9]|2[0-3])[.:][0-5][0-9])?";

        public const string TOTAL_PATTERN = @"(С[аоу]м[а]|Карта)(:?[\s+])(\d+\s?.\s?(\d{2})$)";
    }
}
