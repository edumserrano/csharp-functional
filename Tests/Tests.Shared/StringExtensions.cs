using System.IO;
using System.Text;

namespace Tests.Shared
{
    internal static class StringExtensions
    {
        public static Stream ToStream(this string str)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(str));
        }
    }
}
