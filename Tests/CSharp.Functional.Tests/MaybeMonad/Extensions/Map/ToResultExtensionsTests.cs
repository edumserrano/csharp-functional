using CSharp.Functional.MaybeMonad;
using CSharp.Functional.MaybeMonad.Extensions.Map;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.MaybeMonad.Extensions.Map
{
    public class ToResultExtensionsTests
    {
        [Fact]
        public void To_creates_ok_result_if_maybe_has_value()
        {
            var result = Maybe.From(1)
                    .ToResult();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void To_creates_fail_result_if_maybe_does_not_have_value()
        {
            var result = Maybe<int>.Nothing
                    .ToResult();
            result.IsSuccess.ShouldBeFalse();
        }
    }
}
