using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithErrorMonad.Equality
{
    [Trait("Monad", "HttpResultWithError")]
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
            var error = "error";
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResultWithError.Fail(error);
            object someObject1 = HttpResultWithError.Fail(error);
            var result2 = HttpResultWithError.Fail(error, httpState);
            object someObject2 = HttpResultWithError.Fail(error, httpState);
            var isEqual1 = result1.Equals(someObject1);
            var isEqual2 = result2.Equals(someObject2);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
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
        public void Equals_between_ok_HttpResultWithError_and_object_is_true_if_object_is_ok_HttpResultWithError_with_equal_HttpState()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResultWithError.Ok<string>();
            object someObject1 = HttpResultWithError.Ok<string>();
            var result2 = HttpResultWithError.Ok<string>(httpState);
            object someObject2 = HttpResultWithError.Ok<string>(httpState);
            var isEqual1 = result1.Equals(someObject1);
            var isEqual2 = result2.Equals(someObject2);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
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
