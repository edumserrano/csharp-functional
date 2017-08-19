using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad.Equality
{
    [Trait("Monad", "HttpResultWithValue")]
    public class HttpResultWithValueEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_HttpResultWithValue_is_true_if_the_HttpState_are_equal()
        {
            var value = "abc";
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok(value);
            var result2 = HttpResult.Ok(value);
            var result3 = HttpResult.Ok(value, httpState);
            var result4 = HttpResult.Ok(value, httpState);

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_HttpResultWithValue_is_false_if_the_values_are_not_equal()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_ok_HttpResultWithValue_is_false_if_the_HttpState_are_not_equal()
        {
            var result1 = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_HttpResultWithValue_is_true_if_the_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string>();
            var result2 = HttpResult.Fail<string>();
            var result3 = HttpResult.Fail<string>(httpState);
            var result4 = HttpResult.Fail<string>(httpState);

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_HttpResultWithValue_is_false_if_the_HttpState_are_not_equal()
        {
            var result1 = HttpResult.Fail(Test.CreateHttpStateA());
            var result2 = HttpResult.Fail(Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_HttpResultWithValue_and_fail_HttpResultWithValue_is_false()
        {
            var okResult = HttpResult.Ok("abc");
            var errorResult = HttpResult.Fail<string>();
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }
    }
}
