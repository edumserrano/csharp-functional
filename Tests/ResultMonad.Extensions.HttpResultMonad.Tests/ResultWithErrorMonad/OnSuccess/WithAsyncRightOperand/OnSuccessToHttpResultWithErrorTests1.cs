using System;
using System.Threading.Tasks;
using HttpResultMonad;
using ResultMonad.Extensions.HttpResultMonad.ResultWithErrorMonad.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.HttpResultMonad.Tests.ResultWithErrorMonad.OnSuccess.WithAsyncRightOperand
{
    [Trait("Extensions", "ResultWithError")]
    public class OnSuccessToHttpResultWithErrorTests1
    {
        [Fact]
        public void OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = ResultWithError.Ok<string>()
                .OnSuccessToHttpResultWithError(OnSuccessFunc());

            functionExecuted.ShouldBeTrue();

            Func<Task<HttpResultWithError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    var httpResult = HttpResultWithError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = ResultWithError.Fail("error")
                .OnSuccessToHttpResultWithError(OnSuccessFunc());

            functionExecuted.ShouldBeFalse();

            Func<Task<HttpResultWithError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    functionExecuted = true;
                    var httpResult = HttpResultWithError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = await ResultWithError.Fail(error)
                .OnSuccessToHttpResultWithError(OnSuccessFunc());

            result.Error.ShouldBe(error);

            Func<Task<HttpResultWithError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResultWithError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_does_not_contain_http_state_because_previous_ok_result_does_not_have_them()
        {
            var result = await ResultWithError.Ok<string>()
                .OnSuccessToHttpResultWithError(OnSuccessFunc());

            result.HttpState.HasNoValue.ShouldBeTrue();

            Func<Task<HttpResultWithError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResultWithError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_does_not_contain_http_state_because_previous_fail_result_does_not_have_them()
        {
            var result = await ResultWithError.Fail("error")
                .OnSuccessToHttpResultWithError(OnSuccessFunc());

            result.HttpState.HasNoValue.ShouldBeTrue();

            Func<Task<HttpResultWithError<string>>> OnSuccessFunc()
            {
                return () =>
                {
                    var httpResult = HttpResultWithError.Ok<string>();
                    return Task.FromResult(httpResult);
                };
            }
        }
    }
}
