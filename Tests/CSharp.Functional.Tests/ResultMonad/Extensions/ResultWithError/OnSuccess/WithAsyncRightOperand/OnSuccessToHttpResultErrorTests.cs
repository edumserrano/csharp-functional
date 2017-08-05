using System;
using System.Threading.Tasks;
using CSharp.Functional.HttpResultMonad;
using CSharp.Functional.ResultMonad;
using CSharp.Functional.ResultMonad.Extensions.ResultWithError.OnSuccess;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.Extensions.ResultWithError.OnSuccess.WithAsyncRightOperand
{
    public class OnSuccessToHttpResultErrorTests
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = ResultError.Ok<string>()
                .OnSuccessToHttpResultError(OnSuccessFunc());

            functionExecuted.ShouldBeTrue();

            Func<Task<HttpResultError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    var httpResult = HttpResultError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = ResultError.Fail("error")
                .OnSuccessToHttpResultError(OnSuccessFunc());

            functionExecuted.ShouldBeFalse();

            Func<Task<HttpResultError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    var httpResult = HttpResultError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = await ResultError.Fail(error)
                .OnSuccessToHttpResultError(OnSuccessFunc());

            result.Error.ShouldBe(error);

            Func<Task<HttpResultError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResultError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_does_not_contain_http_state_because_previous_ok_result_does_not_have_them()
        {
            var result = await ResultError.Ok<string>()
                .OnSuccessToHttpResultError(OnSuccessFunc());

            result.HttpState.HasNoValue.ShouldBeTrue();

            Func<Task<HttpResultError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResultError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_does_not_contain_http_state_because_previous_fail_result_does_not_have_them()
        {
            var result = await ResultError.Fail("error")
                .OnSuccessToHttpResultError(OnSuccessFunc());

            result.HttpState.HasNoValue.ShouldBeTrue();

            Func<Task<HttpResultError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResultError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }
    }
}
