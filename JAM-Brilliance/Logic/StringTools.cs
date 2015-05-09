namespace JAM.Dating.Logic
{
    public enum StringLength
    {
        Short = 20,
        Long = 50
    }

    public static class StringTools
    {
        public static string GetBeginning(this string str, StringLength maxLength)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var length = (int)maxLength;
                int index = str.IndexOfAny(new[] { '\r', '\n' });
                string firstline = index == -1 ? str : str.Substring(0, index);
                if (firstline.Length > length)
                {
                    firstline = firstline.Substring(0, length);
                    firstline = string.Concat(firstline, "...");
                }

                return firstline;
            }
            else
            {
                return "-";
            }
        }
    }
}