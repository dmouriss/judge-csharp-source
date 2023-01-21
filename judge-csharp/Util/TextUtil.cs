using System;
namespace judge_csharp.Util
{
    public static class TextUtil
    {
        public static string Pluralize(long amount, string singular, string plural)
        {
            return amount == 1 ? singular : plural;
        }
    }
}
