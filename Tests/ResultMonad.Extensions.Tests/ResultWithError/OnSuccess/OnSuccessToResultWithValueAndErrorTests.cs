using System;
using ResultMonad.Extensions.ResultWithError.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithError.OnSuccess
{
    public class OnSuccessToResultWithValueAndErrorTests
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = ResultError.Ok<string>()
                .OnSuccessToResultWithValueAndError(OnSuccessFunc());

            functionExecuted.ShouldBeTrue();

            Func<int> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    return 1;
                };
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = ResultError.Fail("error")
                .OnSuccessToResultWithValueAndError(OnSuccessFunc());

            functionExecuted.ShouldBeFalse();

            Func<int> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    return 1;
                };
            }
        }

        [Fact]
        public void OnSuccess_new_result_contains_value_from_function_if_result_is_ok()
        {
            var newValue = 1;
            var result = ResultError.Ok<string>()
                .OnSuccessToResultWithValueAndError(() => newValue);

            result.Value.ShouldBe(newValue);
        }

        [Fact]
        public void OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = ResultError.Fail(error)
                .OnSuccessToResultWithValueAndError(() => 1);

            result.Error.ShouldBe(error);
        }
    }
}
