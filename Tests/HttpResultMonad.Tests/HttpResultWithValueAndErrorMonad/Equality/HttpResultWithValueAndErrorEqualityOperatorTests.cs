using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "HttpResultWithValueAndError")]
    public class HttpResultWithValueAndErrorEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_HttpResultWithValueAndError_is_true_if_the_value_and_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("abc");
            var result3 = HttpResult.Ok<string, string>("abc", httpState);
            var result4 = HttpResult.Ok<string, string>("abc", httpState);
            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_HttpResultWithValueAndError_with_different_value_is_false()
        {
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_ok_HttpResultWithValueAndError_with_different_HttpState_is_false()
        {
            var result1 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_HttpResultWithValueAndError_is_true_if_the_error_and_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string, string>("abc", httpState);
            var result2 = HttpResult.Fail<string, string>("abc", httpState);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_HttpResultWithValueAndError_with_different_error_is_false()
        {
            var result1 = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_HttpResultWithValueAndError_with_different_HttpState_is_false()
        {
            var result1 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = HttpResult.Ok<string, string>("abc");
            var errorResult = HttpResult.Fail<string, string>("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }
    }
}
