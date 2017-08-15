using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad.Equality
{
    public class HttpResultWithValueAndErrorEqualsObjectTests
    {
        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_not_http_result()
        {
            var result = HttpResult.Ok<string, string>("abc");
            object someObject = "abc";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_true_if_object_is_ok_result_with_same_value_and_http_state()
        {
            var state = Test.CreateHttpStateA();
            var result = HttpResult.Ok<string, string>("value", state);
            object someObject = HttpResult.Ok<string, string>("value", state);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_ok_result_with_different_value()
        {
            var result = HttpResult.Ok<string, string>("abc");
            object someObject = HttpResult.Ok<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_ok_result_with_different_http_state()
        {
            var result = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            object someObject = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_not_http_result()
        {
            var result = HttpResult.Fail<string, string>("error");
            object someObject = "error";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_true_if_object_is_fail_result_with_same_error_and_http_state()
        {
            var error = "abc";
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<string, string>(error, httpState);
            object someObject = HttpResult.Fail<string, string>(error, httpState);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_fail_result_with_different_value()
        {
            var result = HttpResult.Fail<string, string>("abc");
            object someObject = HttpResult.Fail<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_fail_result_with_different_http_state()
        {
            var result = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            object someObject = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
    }
}
