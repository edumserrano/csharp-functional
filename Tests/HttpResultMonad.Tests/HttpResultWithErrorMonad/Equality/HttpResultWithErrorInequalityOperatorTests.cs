using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithErrorMonad.Equality
{
    [Trait("Monad", "HttpResultWithError")]
    public class HttpResultWithErrorInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithError_is_false_if_the_error_are_equal()
        {
            var error = "abc";
            var result1 = HttpResultWithError.Fail(error);
            var result2 = HttpResultWithError.Fail(error);
            var result3 = HttpResultWithError.Fail(error, Test.CreateHttpStateA());
            var result4 = HttpResultWithError.Fail(error, Test.CreateHttpStateB());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithError_with_different_error_is_true()
        {
            var result1 = HttpResultWithError.Fail("abc");
            var result2 = HttpResultWithError.Fail("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }
        
        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithError_is_false()
        {
            var result1 = HttpResultWithError.Ok<string>();
            var result2 = HttpResultWithError.Ok<string>();
            var result3 = HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultWithError.Ok<string>(Test.CreateHttpStateB());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_HttpResultWithError_and_fail_HttpResultWithError_is_true()
        {
            var okResult = HttpResultWithError.Ok<string>();
            var errorResult = HttpResultWithError.Fail("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
