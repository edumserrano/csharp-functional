using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimple.Equality
{
    public class HttpResultSimpleEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_results_is_true_if_http_state_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(httpState);
            var result4 = HttpResult.Ok(httpState);

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }
        
        [Fact]
        public void Equality_operator_between_two_fail_results_is_true_if_http_state_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail();
            var result2 = HttpResult.Fail();
            var result3 = HttpResult.Fail(httpState);
            var result4 = HttpResult.Fail(httpState);

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }
        
        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = HttpResult.Ok();
            var failResult = HttpResult.Fail();
            var isEqual = okResult == failResult;
            isEqual.ShouldBeFalse();
        }
    }
}
