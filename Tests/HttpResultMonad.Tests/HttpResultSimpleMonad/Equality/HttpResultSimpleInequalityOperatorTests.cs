using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "HttpResultSimple")]
    public class HttpResultSimpleInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_ok_HttpResultSimple_and_fail_HttpResultSimple_is_true()
        {
            var okResult = HttpResult.Ok();
            var failResult = HttpResult.Fail();
            var isEqual = okResult != failResult;
            isEqual.ShouldBeTrue();
        }
    }
}
