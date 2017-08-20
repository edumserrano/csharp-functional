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

        public static bool HeadersEquals(
            this List<KeyValuePair<string, IEnumerable<string>>> headers,
            List<KeyValuePair<string, IEnumerable<string>>> otherHeaders)
        {
            if (headers.Count != otherHeaders.Count)
            {
                return false;
            }

            foreach (var requestHeader in headers)
            {
                var keyMatched = false;

                var key = requestHeader.Key;
                var values = requestHeader.Value.ToList();

                foreach (var otherRequestHeader in otherHeaders)
                {
                    var otherKey = otherRequestHeader.Key;
                    if (string.Equals(key, otherKey))
                    {
                        keyMatched = true;
                        var otherValues = otherRequestHeader.Value.ToList();
                        if (!values.SequenceEqual(otherValues))
                        {
                            return false;
                        }
                        break;
                    }
                }

                if (!keyMatched)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
