using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace MaybeMonad.Extensions.ResultMonad.Tests.Map
{
    [Trait("Extensions", "Maybe")]
    public class ToResultWithValueAndErrorExtensionsTests
    {
        [Fact]
        public void To_creates_ok_ResultWithValueAndError_if_Maybe_has_value()
        {
            var result = Maybe.From(1)
                    .ToResultWithValueAndError(() => "error");
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void To_creates_fail_ResultWithValueAndError_if_Maybe_is_empty()
        {
            var result = Maybe<int>.Nothing
                    .ToResultWithValueAndError(() => "error");
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void To_propagates_value_to_ResultWithValueAndError_if_Maybe_has_value()
        {
            var value = 1;
            var result = Maybe.From(value)
                    .ToResultWithValueAndError(() => "error");
            result.Value.ShouldBe(value);
        }

        [Fact]
        public void To_creates_fail_ResultWithValueAndError_that_contains_error_from_errorFunc_if_Maybe_is_empty()
        {
            var error = "error";
            var result = Maybe<int>.Nothing
                    .ToResultWithValueAndError(() => error);
            result.Error.ShouldBe(error);
        }
    }
}
