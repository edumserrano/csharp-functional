using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimple.Equality
{
    public class HttpResultSimpleEqualsObjectTests
    {
        [Fact]
        public void Equals_betwwen_ok_result_and_object_is_true_if_object_is_ok_result_and__raw_body_and_status_code_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok();
            object result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(httpState);
            object result4 = HttpResult.Ok(httpState);

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_true_if_object_is_fail_result_and_http_state_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail();
            object result2 = HttpResult.Fail();
            var result3 = HttpResult.Fail(httpState);
            object result4 = HttpResult.Fail(httpState);

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_not_ok_result()
        {
            var result1 = HttpResult.Ok();
            object result2 = "abc";
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_not_fail_result()
        {
            var result1 = HttpResult.Fail();
            object result2 = "abc";
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
