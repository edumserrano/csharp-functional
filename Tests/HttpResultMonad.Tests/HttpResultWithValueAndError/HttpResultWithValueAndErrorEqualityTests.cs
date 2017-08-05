using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndError
{
    public class HttpResultWithValueAndErrorEqualityTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_results_is_true_if_they_have_the_same_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok<string, string>("abc", httpState);
            var result2 = HttpResult.Ok<string, string>("abc", httpState);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_results_with_different_value_is_false()
        {
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_ok_results_with_different_http_state_is_false()
        {
            var result1 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_is_false_if_they_have_same_value_and_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok<string, string>("abc", httpState);
            var result2 = HttpResult.Ok<string, string>("abc", httpState);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_value_is_true()
        {
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_http_state_is_true()
        {
            var result1 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_is_true_if_they_have_the_same_error_and_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string, string>("abc", httpState);
            var result2 = HttpResult.Fail<string, string>("abc", httpState);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_with_different_error_is_false()
        {
            var result1 = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_with_different_http_state_is_false()
        {
            var result1 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false_if_they_have_the_same_error_and_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string, string>("abc", httpState);
            var result2 = HttpResult.Fail<string, string>("abc", httpState);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_with_different_error_is_true()
        {
            var result1 = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_with_different_http_state_is_true()
        {
            var result1 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = HttpResult.Ok<string, string>("abc");
            var errorResult = HttpResult.Fail<string, string>("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = HttpResult.Ok<string, string>("abc");
            var errorResult = HttpResult.Fail<string, string>("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

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

        [Fact]
        public void Equals_between_two_ok_results_is_true_if_they_have_the_same_value_and_http_state()
        {
            var state = Test.CreateHttpStateA();
            var result = HttpResult.Ok<string, string>("value", state);
            var result2 = HttpResult.Ok<string, string>("value", state);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_false_if_they_do_not_have_the_same_value()
        {
            var result = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_false_if_they_do_not_have_the_same_http_state()
        {
            var result = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_true_if_they_have_the_same_error_and_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<string, string>("error", httpState);
            var result2 = HttpResult.Fail<string, string>("error", httpState);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_false_if_they_do_not_have_the_same_error()
        {
            var result = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_false_if_they_do_not_have_the_same_http_state()
        {
            var result = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string, string>("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("abc");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
