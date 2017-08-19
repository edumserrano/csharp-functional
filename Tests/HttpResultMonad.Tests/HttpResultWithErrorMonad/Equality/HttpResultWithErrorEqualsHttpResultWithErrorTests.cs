using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithErrorMonad.Equality
{
    [Trait("Monad", "HttpResultWithError")]
    public class HttpResultWithErrorEqualsHttpResultWithErrorTests
    {
        [Fact]
        public void Equals_between_two_fail_HttpResultWithError_is_true_if_the_error_and_HttpState_are_equal()
        {
            var result1 = HttpResultWithError.Fail("error");
            var result2 = HttpResultWithError.Fail("error");
            var result3 = HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var result4 = HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_HttpResultWithError_is_false_if_the_errors_are_not_equal()
        {
            var result = HttpResultWithError.Fail("abc");
            var result2 = HttpResultWithError.Fail("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_HttpResultWithError_is_false_if_the_HttpState_are_not_equal()
        {
            var result = HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_HttpResultWithError_is_true_if_the_error_and_HttpState_are_equal()
        {
            var result1 = HttpResultWithError.Ok<string>();
            var result2 = HttpResultWithError.Ok<string>();
            var result3 = HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_HttpResultWithError_is_false_if_the_HttpState_are_not_equal()
        {
            var result1 = HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result2 = HttpResultWithError.Ok<string>(Test.CreateHttpStateB());
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithError_and_fail_HttpResultWithError_is_false()
        {
            var result = HttpResultWithError.Fail("abc");
            var result2 = HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
