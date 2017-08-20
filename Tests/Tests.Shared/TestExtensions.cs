using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tests.Shared
{
    public static class TestExtensions
    {
        public static Stream ToStream(this string str)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(str));
        }

        public static string ReadAsString(this byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray);
        }

        public static string ReadAsString(this Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true))
            {
                return reader.ReadToEnd();
            }
        }        
    }
}
