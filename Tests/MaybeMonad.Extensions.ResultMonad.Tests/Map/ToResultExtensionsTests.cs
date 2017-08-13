using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace MaybeMonad.Extensions.ResultMonad.Tests.Map
{
    [Trait("Extensions", "Maybe")]
    public class ToResultExtensionsTests
    {
        [Fact]
        public void ToResult_creates_ok_ResultSimple_if_Maybe_has_value()
        {
            var result = Maybe.From(1)
                .ToResult();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void ToResult_creates_fail_ResultSimple_if_Maybe_is_empty()
        {
            var result = Maybe<int>.Nothing
                .ToResult();
            result.IsSuccess.ShouldBeFalse();
        }
    }
}
