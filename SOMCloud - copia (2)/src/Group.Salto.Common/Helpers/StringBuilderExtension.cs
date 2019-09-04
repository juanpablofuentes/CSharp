namespace System.Text
{
    public static class StringBuilderExtension
    {
        public static StringBuilder AppendComa(this StringBuilder sb)
        {
            if (sb.Length > 0 && !sb.ToString().EndsWith(","))
            {
                sb.Append(",");
            }

            return sb;
        }

        public static StringBuilder RemoveLastComa(this StringBuilder sb)
        {
            if (sb.Length > 0 && sb.ToString().EndsWith(","))
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb;
        }
    }
}