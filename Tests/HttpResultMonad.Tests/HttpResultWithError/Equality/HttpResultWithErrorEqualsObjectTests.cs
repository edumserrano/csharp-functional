using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithError.Equality
{
    public class HttpResultWithErrorEqualsObjectTests
    {
        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_false_if_object_is_not_an_httpResultWithError()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            object someObject = "abc";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_true_if_object_is_fail_httpResultWithError_with_same_error_and_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            object someObject = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_false_if_object_is_fail_httpResultWithError_with_different_error()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            object someObject = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_false_if_object_is_fail_httpResultWithError_with_different_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            object someObject = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_httpResultWithError_and_object_is_true_if_object_is_ok_result()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>();
            object someObject = HttpResultMonad.HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_httpResultWithError_and_object_is_false_if_object_is_not_ok_result()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
    }
}
