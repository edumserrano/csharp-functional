using System.Collections.Generic;
using System.Linq;

namespace HttpResultMonad.State
{
    public static class StateExtensions
    {
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

        public static int GetHashCodeForHeaders(this List<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            var hashCode = 1;
            foreach (var header in headers)
            {
                var key = header.Key;
                hashCode = (hashCode * 397) ^ key.GetHashCode();
                foreach (var value in header.Value)
                {
                    hashCode = (hashCode * 397) ^ value.GetHashCode();
                }
            }
            return hashCode;
        }
    }
}
