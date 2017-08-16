using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace MaybeMonad.Extensions.ResultMonad.Tests.Map
{
    [Trait("Extensions", "Maybe")]
    public class ToResultWithValueAndErrorTests
    {
        [Fact]
        public void ToResultWithValueAndError_creates_ok_ResultWithValueAndError_if_Maybe_has_value()
        {
            var resultWithValueAndError = Maybe.From(1)
                .ToResultWithValueAndError(() => "error");
            resultWithValueAndError.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void ToResultWithValueAndError_creates_fail_ResultWithValueAndError_if_Maybe_is_empty()
        {
            var resultWithValueAndError = Maybe<int>.Nothing
                .ToResultWithValueAndError(() => "error");
            resultWithValueAndError.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void ToResultWithValueAndError_propagates_value_to_ResultWithValueAndError_if_Maybe_has_value()
        {
            var value = 1;
            var resultWithValueAndError = Maybe.From(value)
                .ToResultWithValueAndError(() => "error");
            resultWithValueAndError.Value.ShouldBe(value);
        }

        [Fact]
        public void ToResultWithValueAndError_creates_fail_ResultWithValueAndError_that_contains_error_from_errorFunc_if_Maybe_is_empty()
        {
            var error = "error";
            var resultWithValueAndError = Maybe<int>.Nothing
                .ToResultWithValueAndError(() => error);
            resultWithValueAndError.Error.ShouldBe(error);
        }
    }
}
