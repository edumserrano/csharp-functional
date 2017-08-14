using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValue.Equality
{
    public class HttpResultWithValueInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_ok_results_is_false_if_they_have_the_same_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok("value", httpState);
            var result2 = HttpResult.Ok("value", httpState);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }


        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_value_is_true()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_http_state_is_true()
        {
            var result1 = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }


        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false_if_they_have_the_same_http_state()
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
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = HttpResult.Ok("abc");
            var errorResult = HttpResult.Fail<string>();
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
