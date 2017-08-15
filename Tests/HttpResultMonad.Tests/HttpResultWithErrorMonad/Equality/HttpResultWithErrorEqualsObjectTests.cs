using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithErrorMonad.Equality
{
    public class HttpResultWithErrorEqualsObjectTests
    {
        [Fact]
        public void Equals_between_fail_HttpResultWithError_and_object_is_false_if_object_is_not_an_HttpResultWithError()
        {
            var result = HttpResultWithError.Fail("abc");
            object someObject = "abc";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithError_and_object_is_true_if_object_is_fail_HttpResultWithError_with_equal_error_and_HttpState()
        {
            var result = HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            object someObject = HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithError_and_object_is_false_if_object_is_fail_HttpResultWithError_with_different_error()
        {
            var result = HttpResultWithError.Fail("abc");
            object someObject = HttpResultWithError.Fail("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithError_and_object_is_false_if_object_is_fail_HttpResultWithError_with_different_HttpState()
        {
            var result = HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            object someObject = HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithError_and_object_is_true_if_object_is_ok_HttpResultWithError()
        {
            var result = HttpResultWithError.Ok<string>();
            object someObject = HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithError_and_object_is_false_if_object_is_not_ok_HttpResultWithError()
        {
            var result = HttpResultWithError.Ok<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
    }
}
