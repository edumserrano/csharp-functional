using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad.Equality
{
    public class HttpResultWithValueAndErrorEqualsHttpResultWithValueAndErrorTests
    {
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
