using ResultMonad.Extensions.ResultWithValue.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithValue.OnSuccess
{
    public class OnSuccessToResultWithValueTests
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = Result.Ok(1)
                .OnSuccessToResultWithValue(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeTrue();

            Result<string> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Result.Ok("abc");
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = Result.Fail<int>()
                .OnSuccessToResultWithValue(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeFalse();

            Result<string> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Result.Ok("abc");
            }
        }


        [Fact]
        public void OnSuccess_propagates_value_into_function_if_result_is_ok()
        {
            var propagatedValue = 0;
            var value = 1;
            var result = Result.Ok(value)
                .OnSuccessToResultWithValue(i => OnSuccessFunc(i));

            propagatedValue.ShouldBe(value);

            Result<string> OnSuccessFunc(int i)
            {
                propagatedValue = value;
                return Result.Ok("abc");
            }
        }

        [Fact]
        public void OnSuccess_new_result_contains_value_from_function_if_result_is_ok()
        {
            var newValue = "abc";
            var result = Result.Ok(1)
                .OnSuccessToResultWithValue(i => Result.Ok(newValue));

            result.Value.ShouldBe(newValue);
        }

        [Fact]
        public void OnSuccess_propagates_fail_result_if_result_is_fail()
        {
            var result = Result.Fail<int>()
                .OnSuccessToResultWithValue(i => Result.Ok("abc"));

            result.IsFailure.ShouldBeTrue();
        }
    }
}
