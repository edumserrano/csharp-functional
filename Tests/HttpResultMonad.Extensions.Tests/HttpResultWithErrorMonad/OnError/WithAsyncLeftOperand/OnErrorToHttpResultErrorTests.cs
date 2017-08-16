using System.Threading.Tasks;
using HttpResultMonad.Extensions.HttpResultWithErrorMonad.OnError;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Extensions.Tests.HttpResultWithErrorMonad.OnError.WithAsyncLeftOperand
{
    public class OnErrorToHttpResultWithErrorTests1
    {
        [Fact]
        public async Task OnError_executes_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResultMonad.HttpResultWithError.Fail("error"))
                .OnErrorToHttpResultWithError(error => OnErrorFunc(error));

            functionExecuted.ShouldBeTrue();

            int OnErrorFunc(string error)
            {
                functionExecuted = true;
                return 1;
            }
        }

        [Fact]
        public async Task OnError_does_not_execute_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResultMonad.HttpResultWithError.Ok<string>())
                .OnErrorToHttpResultWithError(error => OnErrorFunc(error));

            functionExecuted.ShouldBeFalse();

            int OnErrorFunc(string error)
            {
                functionExecuted = true;
                return 1;
            }
        }

        [Fact]
        public async Task OnError_new_result_contains_error_from_function_if_result_is_fail()
        {
            var newError = -1;
            var result = await Task.FromResult(HttpResultMonad.HttpResultWithError.Fail("error"))
                .OnErrorToHttpResultWithError(error => newError);

            result.Error.ShouldBe(newError);
        }

        [Fact]
        public async Task OnError_propagates_http_state_if_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResultMonad.HttpResultWithError.Fail("error", httpState))
                .OnErrorToHttpResultWithError(error => -1);

            result.HttpState.ShouldBe(httpState);
        }

        [Fact]
        public async Task OnError_propagates_http_state_if_result_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResultMonad.HttpResultWithError.Ok<string>(httpState))
                .OnErrorToHttpResultWithError(error => -1);

            result.HttpState.ShouldBe(httpState);
        }
    }
}
