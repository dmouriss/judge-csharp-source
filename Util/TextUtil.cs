using System;
namespace judge_c_sharp.Util
{
    public static class TextUtil
    {
        public static string Pluralize(long amount, string singular, string plural)
        {
            return amount == 1 ? singular : plural;
        }
    }
}
