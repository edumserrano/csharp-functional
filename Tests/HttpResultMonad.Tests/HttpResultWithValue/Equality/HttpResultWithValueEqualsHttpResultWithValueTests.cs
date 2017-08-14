using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValue.Equality
{
    public class HttpResultWithValueEqualsHttpResultWithValueTests
    {
        [Fact]
        public void Equals_between_two_ok_results_is_true_if_they_have_the_same_value_and_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Ok("abc", httpState);
            var result2 = HttpResult.Ok("abc", httpState);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_false_if_they_do_not_have_the_same_value()
        {
            var result = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_false_if_they_do_not_have_the_same_http_state()
        {
            var result = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_true_if_they_have_the_same_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<string>(httpState);
            var result2 = HttpResult.Fail<string>(httpState);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_false_if_they_do_not_have_the_same_http_state()
        {
            var result = HttpResult.Fail<string>(Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string>(Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result = HttpResult.Ok("abc");
            var result2 = HttpResult.Fail<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
