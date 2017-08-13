using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace MaybeMonad.Extensions.ResultMonad.Tests.Map
{
    [Trait("Extensions", "Maybe")]
    public class ToResultWithValueExtensionsTests
    {
        [Fact]
        public void To_creates_ok_ResultWithValue_if_Maybe_has_value()
        {
            var result = Maybe.From(1)
                    .ToResultWithValue();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void To_creates_fail_ResultWithValue_if_Maybe_is_empty()
        {
            var result = Maybe<int>.Nothing
                    .ToResultWithValue();
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void To_propagates_value_to_ResultWithValue_if_Maybe_has_value()
        {
            var value = 1;
            var result = Maybe.From(value)
                    .ToResultWithValue();
            result.Value.ShouldBe(value);
        }
    }
}
