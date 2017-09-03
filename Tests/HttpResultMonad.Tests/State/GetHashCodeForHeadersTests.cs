using System.Collections.Generic;
using Xunit;
using HttpResultMonad.State;
using Shouldly;

namespace HttpResultMonad.Tests.State
{
    [Trait("HttpStateExtensions", "GetHashCodeForHeaders")]
    public class GetHashCodeForHeadersTests
    {
        [Fact]
        public void GetHashCodeForHeaders_is_equal_if_headers_are_structurally_equal()
        {
            var headers1 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "value1" }));
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key2", new List<string> { "value1", "value2" }));
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key3", new List<string> { "" }));

            var headers2 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "value1" }));
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key2", new List<string> { "value1", "value2" }));
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key3", new List<string> { "" }));

            headers1
                .GetHashCodeForHeaders()
                .ShouldBe(headers2.GetHashCodeForHeaders());
        }

        [Fact]
        public void GetHashCodeForHeaders_is_not_equal_if_keys_are_different()
        {
            var headers1 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "value1" }));
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key2", new List<string> { "value1", "value2" }));
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key3", new List<string> { "" }));

            var headers2 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "value1" }));
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key3", new List<string> { "value1", "value2" }));
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "" }));

            headers1
                .GetHashCodeForHeaders()
                .ShouldNotBe(headers2.GetHashCodeForHeaders());
        }

        [Fact]
        public void GetHashCodeForHeaders_is_not_equal_if_keys_count_are_different()
        {
            var headers1 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "value1" }));

            var headers2 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "value1" }));
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key2", new List<string> { "value1", "value2" }));

            headers1
                .GetHashCodeForHeaders()
                .ShouldNotBe(headers2.GetHashCodeForHeaders());
        }


        [Fact]
        public void GetHashCodeForHeaders_is_not_equal_if_the_values_for_the_keys_are_different()
        {
            var headers1 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "value1" }));
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key2", new List<string> { "value1", "value2" }));
            headers1.Add(new KeyValuePair<string, IEnumerable<string>>("key3", new List<string> { "" }));

            var headers2 = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key1", new List<string> { "valueA" }));
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key2", new List<string> { "valueB", "valueC" }));
            headers2.Add(new KeyValuePair<string, IEnumerable<string>>("key3", new List<string>()));

            headers1
                .GetHashCodeForHeaders()
                .ShouldNotBe(headers2.GetHashCodeForHeaders());
        }
    }
}
