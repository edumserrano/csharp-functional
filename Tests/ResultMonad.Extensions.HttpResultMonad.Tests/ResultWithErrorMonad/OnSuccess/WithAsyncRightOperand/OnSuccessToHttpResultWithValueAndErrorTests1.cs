﻿using System;
using System.Threading.Tasks;
using HttpResultMonad;
using HttpResultMonad.State;
using ResultMonad.Extensions.HttpResultMonad.ResultWithErrorMonad.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.HttpResultMonad.Tests.ResultWithErrorMonad.OnSuccess.WithAsyncRightOperand
{
    [Trait("Extensions", "ResultWithError")]
    public class OnSuccessToHttpResultWithValueAndErrorTests1
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = ResultWithError.Ok<string>()
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

            functionExecuted.ShouldBeTrue();

            Func<Task<HttpResult<int, string>>> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    var httpResult = HttpResult.Ok<int, string>(1);
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = ResultWithError.Fail("error")
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

            functionExecuted.ShouldBeFalse();

            Func<Task<HttpResult<int, string>>> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    var httpResult = HttpResult.Ok<int, string>(1);
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_contains_value_from_function_if_result_is_ok()
        {
            var newValue = 1;
            var result = await ResultWithError.Ok<string>()
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

            result.Value.ShouldBe(newValue);

            Func<Task<HttpResult<int, string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResult.Ok<int, string>(newValue);
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = await ResultWithError.Fail(error)
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

            result.Error.ShouldBe(error);

            Func<Task<HttpResult<int, string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResult.Ok<int, string>(1);
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_does_not_contain_http_state_because_previous_ok_result_does_not_have_them()
        {
            var result = await ResultWithError.Ok<string>()
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

            result.HttpState.ShouldBe(HttpState.Empty);

            Func<Task<HttpResult<int, string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResult.Ok<int, string>(1);
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_does_not_contain_http_state_because_previous_fail_result_does_not_have_them()
        {
            var result = await ResultWithError.Fail("error")
                .OnSuccessToHttpResultWithValueAndError(OnSuccessFunc());

            result.HttpState.ShouldBe(HttpState.Empty);

            Func<Task<HttpResult<int, string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResult.Ok<int, string>(1);
                    return Task.FromResult(httpResult);
                };
            }
        }
    }
}
