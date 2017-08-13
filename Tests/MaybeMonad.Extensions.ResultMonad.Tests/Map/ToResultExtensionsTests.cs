using System.Diagnostics.CodeAnalysis;
using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace MaybeMonad.Extensions.ResultMonad.Tests.Map
{
    [Trait("Extensions", "Maybe")]
    [ExcludeFromCodeCoverage]
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
