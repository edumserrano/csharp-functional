using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad.Equality
{
    public class HttpResultWithValueInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValue_is_false_if_the_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok("value", httpState);
            var result2 = HttpResult.Ok("value", httpState);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }


        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValue_with_different_value_is_true()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValue_with_different_HttpState_is_true()
        {
            var result1 = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }


        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithValue_is_false_if_the_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string>();
            var result2 = HttpResult.Fail<string>();
            var result3 = HttpResult.Fail<string>(httpState);
            var result4 = HttpResult.Fail<string>(httpState);

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithValue_is_true_if_the_HttpState_are_not_equal()
        {
            var result1 = HttpResult.Fail<string>(Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string>(Test.CreateHttpStateB());
            
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }


        [Fact]
        public void Inequality_operator_between_ok_HttpResultWithValue_and_fail_HttpResultWithValue_is_true()
        {
            var okResult = HttpResult.Ok("abc");
            var errorResult = HttpResult.Fail<string>();
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
