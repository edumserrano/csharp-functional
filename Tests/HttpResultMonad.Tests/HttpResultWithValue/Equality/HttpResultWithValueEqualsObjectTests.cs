using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValue.Equality
{
    public class HttpResultWithValueEqualsObjectTests
    {
        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_not_an_http_result()
        {
            var value = "abc";
            var result = HttpResult.Ok(value);
            object someObject = value;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_true_if_object_is_ok_result_with_same_value_and_http_state()
        {
            var value = "abc";
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Ok(value, httpState);
            object someObject = HttpResult.Ok(value, httpState);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_ok_result_with_different_value()
        {
            var result = HttpResult.Ok("abc");
            object someObject = HttpResult.Ok("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_ok_result_with_different_http_State()
        {
            var result = HttpResult.Ok("abc", Test.CreateHttpStateA());
            object someObject = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_not_an_http_result()
        {
            var result = HttpResult.Fail<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_true_if_object_is_fail_result_with_the_same_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<string>(httpState);
            object someObject = HttpResult.Fail<string>(httpState);

            var isEqual = result.Equals(someObject);

            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_fail_result_with_different_http_state()
        {
            var result = HttpResult.Fail<string>(Test.CreateHttpStateA());
            object someObject = HttpResult.Fail<string>(Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
    }
}
