using System.Threading.Tasks;
using HttpResultMonad.Extensions.HttpResultWithValueMonad.OnError;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Extensions.Tests.HttpResultWithValue.OnError.WithAsyncLeftOperand
{
    public class OnErrorToHttpResultWithValueAndErrorTests
    {
        [Fact]
        public async Task OnError_executes_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResult.Fail<string>())
                .OnErrorToHttpResultWithValueAndError(() => OnErrorFunc());

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
            var result = await Task.FromResult(HttpResult.Ok("value"))
                .OnErrorToHttpResultWithValueAndError(() => OnErrorFunc());

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
            var result = await Task.FromResult(HttpResult.Fail<string>())
                .OnErrorToHttpResultWithValueAndError(() => newError);

            result.Error.ShouldBe(newError);
        }

        [Fact]
        public async Task OnError_propagates_value_if_result_is_ok()
        {
            var value = 1;
            var result = await Task.FromResult(HttpResult.Ok(value))
                .OnErrorToHttpResultWithValueAndError(() => "error");

            result.Value.ShouldBe(value);
        }

        [Fact]
        public async Task OnError_propagates_http_state_if_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResult.Fail<int>(httpState))
                .OnErrorToHttpResultWithValueAndError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }

        [Fact]
        public async Task OnError_propagates_http_state_if_result_is_ok()
        {
            var value = 1;
            var httpState = Test.CreateHttpStateA();
            var result = await Task.FromResult(HttpResult.Ok(value, httpState))
                .OnErrorToHttpResultWithValueAndError(() => "error");

            result.HttpState.ShouldBe(httpState);
        }
    }
}
