using System;
using HttpResultMonad;
using HttpResultMonad.Extensions.HttpResultWithError.OnSuccess;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.HttpResultMonad.Extensions.HttpResultWithError.OnSuccess
{
    public class OnSuccessToHttpResultWithValueAndErrorTests
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = HttpResultError.Ok<string>()
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

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
            var result = HttpResultError.Fail("error")
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

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
            var result = HttpResultError.Ok<string>()
                .OnSuccessToHttpResultWithValueAndError(() => newValue);

            result.Value.ShouldBe(newValue);
        }

        [Fact]
        public void OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = HttpResultError.Fail(error)
                .OnSuccessToHttpResultWithValueAndError(() => 1);

            result.Error.ShouldBe(error);
        }

        [Fact]
        public void OnSuccess_propagates_http_state_if_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResultError.Fail("error", httpState)
                .OnSuccessToHttpResultWithValueAndError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }

        [Fact]
        public void OnSuccess_propagates_http_state_if_result_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResultError.Ok<string>(httpState)
                .OnSuccessToHttpResultWithValueAndError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }
    }
}
