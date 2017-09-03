using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace MaybeMonad.Extensions.ResultMonad.Tests.Map
{
    [Trait("Extensions", "Maybe")]
    public class ToResultWithValueTests
    {
        [Fact]
        public void ToResultWithValue_creates_ok_ResultWithValue_if_Maybe_has_value()
        {
            var resultWithValue = Maybe.From(1)
                .ToResultWithValue();
            resultWithValue.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void ToResultWithValue_creates_fail_ResultWithValue_if_Maybe_is_empty()
        {
            var resultWithValue = Maybe<int>.Nothing
                .ToResultWithValue();
            resultWithValue.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void ToResultWithValue_propagates_value_to_ResultWithValue_if_Maybe_has_value()
        {
            var value = 1;
            var resultWithValue = Maybe.From(value)
                .ToResultWithValue();
            resultWithValue.Value.ShouldBe(value);
        }
    }
}
