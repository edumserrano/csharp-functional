using CSharp.Functional.ResultMonad;
using CSharp.Functional.ResultMonad.Extensions.ResultWithValueAndError.OnSuccess;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.Extensions.ResultWithValueAndError.OnSuccess
{
    public class OnSuccessToResultWithValueAndErrorTests_OnSuccessFunc_From_TValue_To_ResultWithValueAndError_Of_KValue_TError
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = Result.Ok<int, string>(1)
                .OnSuccessToResultWithValueAndError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeTrue();

            Result<string, string> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Result.Ok<string, string>("value");
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = Result.Fail<int, string>("error")
                .OnSuccessToResultWithValueAndError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeFalse();

            Result<string, string> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Result.Ok<string, string>("value");
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

            Result<string, string> OnSuccessFunc(int val)
            {
                propagatedValue = val;
                return Result.Ok<string, string>("value");
            }
        }

        [Fact]
        public void OnSuccess_new_result_contains_value_from_function_if_result_is_ok()
        {
            var newValue = "abc";
            var result = Result.Ok<int, string>(1)
                .OnSuccessToResultWithValueAndError(i => Result.Ok<string, string>(newValue));

            result.Value.ShouldBe(newValue);
        }

        [Fact]
        public void OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = Result.Fail<int, string>(error)
                .OnSuccessToResultWithValueAndError(i => Result.Ok<int, string>(2));

            result.Error.ShouldBe(error);
        }
    }
}
