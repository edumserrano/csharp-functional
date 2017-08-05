using CSharp.Functional.MaybeMonad;
using CSharp.Functional.MaybeMonad.Extensions.Map;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.MaybeMonad.Extensions.Map
{
    public class ToResultWithValueExtensionsTests
    {
        [Fact]
        public void To_creates_ok_result_if_maybe_has_value()
        {
            var result = Maybe.From(1)
                    .ToResultWithValue();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void To_creates_fail_result_if_maybe_does_not_have_value()
        {
            var result = Maybe<int>.Nothing
                    .ToResultWithValue();
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void To_propagates_value_if_maybe_has_value()
        {
            var value = 1;
            var result = Maybe.From(value)
                    .ToResultWithValue();
            result.Value.ShouldBe(value);
        }
    }
}
