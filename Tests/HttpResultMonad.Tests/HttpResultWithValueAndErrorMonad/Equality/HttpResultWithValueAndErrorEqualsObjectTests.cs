using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "HttpResultWithValueAndError")]
    public class HttpResultWithValueAndErrorEqualsObjectTests
    {
        [Fact]
        public void Equals_between_ok_HttpResultWithValueAndError_and_object_is_false_if_object_is_not_HttpResultWithValueAndError()
        {
            var result = HttpResult.Ok<string, string>("abc");
            object someObject = "abc";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithValueAndError_and_object_is_true_if_object_is_ok_HttpResultWithValueAndError_with_equal_value_and_HttpState()
        {
            var state = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok<string, string>("value");
            object someObject1 = HttpResult.Ok<string, string>("value");
            var result2 = HttpResult.Ok<string, string>("value", state);
            object someObject2 = HttpResult.Ok<string, string>("value", state);
            var isEqual1 = result1.Equals(someObject1);
            var isEqual2 = result2.Equals(someObject2);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithValueAndError_and_object_is_false_if_object_is_ok_HttpResultWithValueAndError_with_different_value()
        {
            var result = HttpResult.Ok<string, string>("abc");
            object someObject = HttpResult.Ok<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithValueAndError_and_object_is_false_if_object_is_ok_HttpResultWithValueAndError_with_different_HttpState()
        {
            var result = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            object someObject = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithValueAndError_and_object_is_false_if_object_is_not__HttpResultWithValueAndError()
        {
            var result = HttpResult.Fail<string, string>("error");
            object someObject = "error";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithValueAndError_and_object_is_true_if_object_is_fail_HttpResultWithValueAndError_with_equal_error_and_HttpState()
        {
            var error = "abc";
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string, string>(error);
            object someObject1 = HttpResult.Fail<string, string>(error);
            var result2 = HttpResult.Fail<string, string>(error, httpState);
            object someObject2 = HttpResult.Fail<string, string>(error, httpState);
            var isEqual1 = result1.Equals(someObject1);
            var isEqual2 = result2.Equals(someObject2);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithValueAndError_and_object_is_false_if_object_is_fail_HttpResultWithValueAndError_with_different_value()
        {
            var result = HttpResult.Fail<string, string>("abc");
            object someObject = HttpResult.Fail<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithValueAndError_and_object_is_false_if_object_is_fail_HttpResultWithValueAndError_with_different_HttpState()
        {
            var result = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            object someObject = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
    }
}
