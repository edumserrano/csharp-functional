using MaybeMonad;
using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.MaybeMonad.Extensions.Map
{
    public class ToResultWithValueAndErrorExtensionsTests
    {
        [Fact]
        public void To_creates_ok_result_if_maybe_has_value()
        {
            var result = Maybe.From(1)
                    .ToResultWithValueAndError(() => "error");
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void To_creates_fail_result_if_maybe_does_not_have_value()
        {
            var result = Maybe<int>.Nothing
                    .ToResultWithValueAndError(() => "error");
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void To_propagates_value_if_maybe_has_value()
        {
            var value = 1;
            var result = Maybe.From(value)
                    .ToResultWithValueAndError(() => "error");
            result.Value.ShouldBe(value);
        }

        [Fact]
        public void To_new_result_contains_error_from_func_if_maybe_does_not_have_value()
        {
            var error = "error";
            var result = Maybe<int>.Nothing
                    .ToResultWithValueAndError(() => error);
            result.Error.ShouldBe(error);
        }
    }
}
