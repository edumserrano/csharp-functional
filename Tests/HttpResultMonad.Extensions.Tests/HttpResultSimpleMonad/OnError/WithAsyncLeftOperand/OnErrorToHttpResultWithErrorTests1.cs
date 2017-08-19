using System.Threading.Tasks;
using HttpResultMonad.Extensions.HttpResultSimpleMonad.OnError;
using MaybeMonad;
using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Extensions.Tests.HttpResultSimpleMonad.OnError.WithAsyncLeftOperand
{
    [Trait("Extensions", "HttpResult")]
    public class OnErrorToHttpResultWithErrorTests1
    {
        [Fact]
        public async Task OnError_executes_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResult.Fail())
                .OnErrorToHttpResultWithError(() => OnErrorFunc());

            functionExecuted.ShouldBeTrue();

            int OnErrorFunc()
            {
                functionExecuted = true;
                return 1;
            }
        }

        [Fact]
        public async Task OnError_does_not_execute_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResult.Ok())
                .OnErrorToHttpResultWithError(() => OnErrorFunc());

            functionExecuted.ShouldBeFalse();

            int OnErrorFunc()
            {
                functionExecuted = true;
                return 1;
            }
        }

        [Fact]
        public async Task OnError_new_result_contains_error_from_function_if_result_is_fail()
        {
            var newError = "abc";
            var result = await Task.FromResult(HttpResult.Fail())
                .OnErrorToHttpResultWithError(() => newError);

            result.Error.ShouldBe(newError);
        }

        [Fact]
        public async Task OnError_propagates_http_state_f_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResult.Fail(httpState))
                .OnErrorToHttpResultWithError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }


        [Fact]
        public async Task OnError_propagates_http_state_if_result_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResult.Ok(httpState))
                .OnErrorToHttpResultWithError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }
    }
}
