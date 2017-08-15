using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class HttpResultSimpleGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_ok_HttpResultSimple_is_equal()
        {
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Ok();
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_HttpResultSimple_is_equal()
        {
            var result1 = HttpResult.Fail();
            var result2 = HttpResult.Fail();
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_ok_HttpResultSimple_and_fail_HttpResultSimple_is_not_equal()
        {
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Fail();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }
    }
}
