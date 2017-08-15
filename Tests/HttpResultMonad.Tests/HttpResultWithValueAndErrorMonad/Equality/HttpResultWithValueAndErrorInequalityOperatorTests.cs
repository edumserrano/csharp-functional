using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad.Equality
{
    public class HttpResultWithValueAndErrorInequalityOperatorTests
    {
       [Fact]
        public void Inequality_operator_between_two_ok_results_is_false_if_they_have_same_value_and_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok<string, string>("abc", httpState);
            var result2 = HttpResult.Ok<string, string>("abc", httpState);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_value_is_true()
        {
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_http_state_is_true()
        {
            var result1 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }
        
        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false_if_they_have_the_same_error_and_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string, string>("abc", httpState);
            var result2 = HttpResult.Fail<string, string>("abc", httpState);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_with_different_error_is_true()
        {
            var result1 = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_with_different_http_state_is_true()
        {
            var result1 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = HttpResult.Ok<string, string>("abc");
            var errorResult = HttpResult.Fail<string, string>("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
