using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "HttpResultSimple")]
    public class HttpResultSimpleEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_ok_HttpResultSimple_and_fail_HttpResultSimple_is_false()
        {
            var okResult = HttpResult.Ok();
            var failResult = HttpResult.Fail();
            var isEqual = okResult == failResult;
            isEqual.ShouldBeFalse();
        }
    }
}
