using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "HttpResultSimple")]
    public class HttpResultSimpleGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_ok_HttpResultSimple_and_fail_HttpResultSimple_is_not_equal()
        {
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Fail();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }
    }
}
