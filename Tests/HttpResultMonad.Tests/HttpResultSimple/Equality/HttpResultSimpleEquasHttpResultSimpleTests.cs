using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimple.Equality
{
    public class HttpResultSimpleEquasHttpResultSimpleTests
    {
        [Fact]
        public void Equals_between_two_ok_results_is_true_if_http_state_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(httpState);
            var result4 = HttpResult.Ok(httpState);

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_trues_if_http_state_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail();
            var result2 = HttpResult.Fail();
            var result3 = HttpResult.Fail(httpState);
            var result4 = HttpResult.Fail(httpState);

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Fail();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
