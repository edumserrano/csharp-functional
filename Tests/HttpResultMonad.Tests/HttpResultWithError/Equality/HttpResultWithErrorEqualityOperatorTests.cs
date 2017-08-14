using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithError.Equality
{
    public class HttpResultWithErrorEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_fail_httpResultWithErrors_is_true_if_the_http_state_and_the_error_are_the_same()
        {
            var error = "abc";
            var result1 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result2 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result3 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }
        
        [Fact]
        public void Equality_operator_between_two_fail_httpResultWithErrors_with_different_error_is_false()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_httpResultWithErrors_with_different_http_state_is_false()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }
       
        [Fact]
        public void Equality_operator_between_ok_httpResultWithErrors_is_true_if_http_state_are_the_same()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result3 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_httpResultWithError_and_fail_httpResultWithError_is_false()
        {
            var okResult = HttpResultMonad.HttpResultWithError.Ok<string>();
            var errorResult = HttpResultMonad.HttpResultWithError.Fail("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }
    }
}
