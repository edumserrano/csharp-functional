using ResultMonad.Extensions.ResultWithValueAndError.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithValueAndError.OnSuccess
{
    public class OnSuccessToResultWithValueAndErrorTests_OnSuccessFunc_From_TValue_To_KValue
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = Result.Ok<int, string>(1)
                .OnSuccessToResultWithValueAndError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeTrue();

            string OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return "value";
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = Result.Fail<int, string>("error")
                .OnSuccessToResultWithValueAndError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeFalse();

            string OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return "value";
            }
        }


        [Fact]
        public void OnSuccess_propagates_value_into_function_if_result_is_ok()
        {
            var propagatedValue = 0;
            var value = 1;
            var result = Result.Ok<int, string>(value)
                .OnSuccessToResultWithValueAndError(i => OnSuccessFunc(i));

            propagatedValue.ShouldBe(value);

            string OnSuccessFunc(int val)
            {
                propagatedValue = val;
                return "value";
            }
        }

        [Fact]
        public void OnSuccess_new_result_contains_value_from_function_if_result_is_ok()
        {
            var newValue = "abc";
            var result = Result.Ok<int, string>(1)
                .OnSuccessToResultWithValueAndError(i => newValue);

            result.Value.ShouldBe(newValue);
        }

        [Fact]
        public void OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = Result.Fail<int, string>(error)
                .OnSuccessToResultWithValueAndError(i => 2);

            result.Error.ShouldBe(error);
        }
    }
}
