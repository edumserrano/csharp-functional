using HttpResultMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.HttpResultMonad.HttpResultWithValue
{
    public class HttpResultWithValueEqualityTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_results_is_true_if_the_http_state_are_equal()
        {
            var value = "abc";
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok(value);
            var result2 = HttpResult.Ok(value);
            var result3 = HttpResult.Ok(value, httpState);
            var result4 = HttpResult.Ok(value, httpState);

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_results_with_different_value_is_false()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_ok_results_with_different_http_state_is_false()
        {
            var result1 = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_is_false_if_they_have_the_same_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok("value", httpState);
            var result2 = HttpResult.Ok("value", httpState);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }


        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_value_is_true()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_http_state_is_true()
        {
            var result1 = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_is_true_if_they_have_the_same_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string>();
            var result2 = HttpResult.Fail<string>();
            var result3 = HttpResult.Fail<string>(httpState);
            var result4 = HttpResult.Fail<string>(httpState);

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false_if_they_have_the_same_http_state()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail<string>();
            var result2 = HttpResult.Fail<string>();
            var result3 = HttpResult.Fail<string>(httpState);
            var result4 = HttpResult.Fail<string>(httpState);

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = HttpResult.Ok("abc");
            var errorResult = HttpResult.Fail<string>();
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = HttpResult.Ok("abc");
            var errorResult = HttpResult.Fail<string>();
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

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
