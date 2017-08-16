using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "HttpResultWithValueAndError")]
    public class HttpResultWithValueAndErrorInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValueAndError_is_false_if_the_value_and_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("abc");
            var result3 = HttpResult.Ok<string, string>("abc", httpState);
            var result4 = HttpResult.Ok<string, string>("abc", httpState);
            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;
            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValueAndError_with_different_value_is_true()
        {
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValueAndError_with_different_HttpState_is_true()
        {
            var result1 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithValueAndError_is_false_if_the_error_and_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("abc");
            var result3 = HttpResult.Fail<string, string>("abc", httpState);
            var result4 = HttpResult.Fail<string, string>("abc", httpState);
            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;
            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithValueAndError_with_different_error_is_true()
        {
            var result1 = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithValueAndError_with_different_HttpState_is_true()
        {
            var result1 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_ok_HttpResultWithValueAndError_and_fail_HttpResultWithValueAndError_is_true()
        {
            var okResult = HttpResult.Ok<string, string>("abc");
            var errorResult = HttpResult.Fail<string, string>("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
