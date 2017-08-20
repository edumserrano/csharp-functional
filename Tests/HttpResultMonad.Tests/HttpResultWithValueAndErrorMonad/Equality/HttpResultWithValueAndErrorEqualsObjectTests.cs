using Shouldly;
using Tests.Shared;
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
        public void Equals_between_ok_HttpResultWithValueAndError_and_object_is_true_if_object_is_ok_HttpResultWithValueAndError_with_equal_value()
        {
            var result1 = HttpResult.Ok<string, string>("value");
            object someObject1 = HttpResult.Ok<string, string>("value");
            var result2 = HttpResult.Ok<string, string>("value", Test.CreateHttpStateA());
            object someObject2 = HttpResult.Ok<string, string>("value", Test.CreateHttpStateB());
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
        public void Equals_between_fail_HttpResultWithValueAndError_and_object_is_false_if_object_is_not__HttpResultWithValueAndError()
        {
            var result = HttpResult.Fail<string, string>("error");
            object someObject = "error";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithValueAndError_and_object_is_true_if_object_is_fail_HttpResultWithValueAndError_with_equal_error()
        {
            var error = "abc";
            var result1 = HttpResult.Fail<string, string>(error);
            object someObject1 = HttpResult.Fail<string, string>(error);
            var result2 = HttpResult.Fail<string, string>(error, Test.CreateHttpStateA());
            object someObject2 = HttpResult.Fail<string, string>(error, Test.CreateHttpStateB());
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
    }
}
