using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad.Equality
{
    public class HttpResultWithValueEqualsHttpResultWithValueTests
    {
        [Fact]
        public void Equals_between_two_ok_HttpResultWithValue_is_true_if_the_values_and_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok("abc", httpState);
            var result2 = HttpResult.Ok("abc", httpState);
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_HttpResultWithValue_is_false_if_they_do_not_have_the_same_value()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_HttpResultWithValue_is_false_if_they_do_not_have_the_same_HttpState()
        {
            var result1 = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_HttpResultWithValue_is_true_if_they_have_the_same_HttpState()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string>(httpState);
            var result2 = HttpResult.Fail<string>(httpState);
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_HttpResultWithValue_is_false_if_they_do_not_have_the_same_HttpState()
        {
            var result1 = HttpResult.Fail<string>(Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string>(Test.CreateHttpStateB());
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Fail<string>();
            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            isEqual1.ShouldBeFalse();
            isEqual2.ShouldBeFalse();
        }
    }
}
