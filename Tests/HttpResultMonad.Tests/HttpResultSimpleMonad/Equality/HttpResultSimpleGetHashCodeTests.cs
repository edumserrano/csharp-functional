using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "HttpResultWithSimple")]
    public class HttpResultSimpleGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_ok_HttpResultSimple_is_equal_if_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(httpState);
            var result4 = HttpResult.Ok(httpState);
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
            result3.GetHashCode().ShouldBe(result4.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_ok_HttpResultSimple_is_not_equal_if_HttpState_are_not_equal()
        {
            var result1 = HttpResult.Ok(Test.CreateHttpStateA());
            var result2 = HttpResult.Ok(Test.CreateHttpStateB());
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_HttpResultSimple_is_equal_if_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail();
            var result2 = HttpResult.Fail();
            var result3 = HttpResult.Fail(httpState);
            var result4 = HttpResult.Fail(httpState);
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
            result3.GetHashCode().ShouldBe(result4.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_HttpResultSimple_is_not_equal_if_HttpState_are_not_equal()
        {
            var result1 = HttpResult.Fail(Test.CreateHttpStateA());
            var result2 = HttpResult.Fail(Test.CreateHttpStateB());
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
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
