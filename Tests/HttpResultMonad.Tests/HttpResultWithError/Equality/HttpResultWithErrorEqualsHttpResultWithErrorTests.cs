using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithError.Equality
{
    public class HttpResultWithErrorEqualsHttpResultWithErrorTests
    {
        [Fact]
        public void Equals_between_two_fail_httpResultWithErrors_is_true_if_they_have_the_same_error_and_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultWithErrors_is_false_if_they_do_not_have_the_same_error()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultWithErrors_is_false_if_they_do_not_have_the_same_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_httpResultWithErrors_is_true_if_they_have_the_same_status_code_and_raw_body()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_httpResultWithErrors_is_false_if_they_have_different_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_httpResultWithError_and_fail_httpResultWithError_is_false()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
