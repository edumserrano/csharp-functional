using System.Threading.Tasks;
using HttpResultMonad;
using HttpResultMonad.Extensions.HttpResultSimple.OnError;
using MaybeMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.HttpResultMonad.Extensions.HttpResultSimple.OnError.WithAsyncLeftOperand
{
    public class OnErrorToHttpResultErrorTests
    {
        [Fact]
        public async Task OnError_executes_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResult.Fail())
                .OnErrorToHttpResultError(() => OnErrorFunc());

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
                .OnErrorToHttpResultError(() => OnErrorFunc());

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
                .OnErrorToHttpResultError(() => newError);

            result.Error.ShouldBe(newError);
        }

        [Fact]
        public async Task OnError_propagates_http_state_f_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResult.Fail(httpState))
                .OnErrorToHttpResultError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }


        [Fact]
        public async Task OnError_propagates_http_state_if_result_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResult.Ok(Maybe.From(httpState)))
                .OnErrorToHttpResultError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }
    }
}
