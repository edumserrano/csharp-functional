using MaybeMonad.Extensions.ResultMonad.Map;
using Shouldly;
using Xunit;

namespace MaybeMonad.Extensions.ResultMonad.Tests.Map
{
    [Trait("Extensions", "Maybe")]
    public class ToResultExtensionsTests
    {
        [Fact]
        public void To_creates_ok_SimpleResult_if_Maybe_has_value()
        {
            var result = Maybe.From(1)
                    .ToResult();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void To_creates_fail_SimpleResult_if_Maybe_is_empty()
        {
            var result = Maybe<int>.Nothing
                    .ToResult();
            result.IsSuccess.ShouldBeFalse();
        }
    }
}
