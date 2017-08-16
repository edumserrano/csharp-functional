using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad.Equality
{
    [Trait("Monad", "HttpResultWithValue")]
    public class HttpResultWithValueGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_ok_HttpResultWithValue_is_equal_if_both_values_are_equal()
        {
            var value = "abc";
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok(value);
            var result2 = HttpResult.Ok(value);
            var result3 = HttpResult.Ok(value, httpState);
            var result4 = HttpResult.Ok(value, httpState);
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
            result3.GetHashCode().ShouldBe(result4.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_ok_HttpResultWithValue_is_not_equal_if_HttpState_are_not_equal()
        {
            var value = 1;
            var result1 = HttpResult.Ok(value, Test.CreateHttpStateA());
            var result2 = HttpResult.Ok(value, Test.CreateHttpStateB());
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_ok_HttpResultWithValue_is_not_equal_if_values_are_not_equal()
        {
            var result1 = HttpResult.Ok(1);
            var result2 = HttpResult.Ok(2);
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_HttpResultWithValue_is_equal_if_the_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string>(httpState);
            var result2 = HttpResult.Fail<string>(httpState);
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_HttpResultWithValue_is_not_equal_if_the_HttpState_are_not_equal()
        {
            var result1 = HttpResult.Fail<string>(Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string>(Test.CreateHttpStateB());
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_HttpResultWithValue_is_not_equal_if_values_are_not_of_the_same_type()
        {
            var result1 = HttpResult.Fail<int>();
            var result2 = HttpResult.Fail<string>();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_ok_HttpResultWithValue_and_fail_HttpResultWithValue_is_not_equal()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Fail<string>();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }
    }
}
