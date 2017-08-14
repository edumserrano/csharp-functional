using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimple.Equality
{
    public class HttpResultSimpleInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_ok_results_is_false_if_http_state_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(httpState);
            var result4 = HttpResult.Ok(httpState);

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false_if_http_state_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail();
            var result2 = HttpResult.Fail();
            var result3 = HttpResult.Fail(httpState);
            var result4 = HttpResult.Fail(httpState);

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = HttpResult.Ok();
            var failResult = HttpResult.Fail();
            var isEqual = okResult != failResult;
            isEqual.ShouldBeTrue();
        }
    }
}
