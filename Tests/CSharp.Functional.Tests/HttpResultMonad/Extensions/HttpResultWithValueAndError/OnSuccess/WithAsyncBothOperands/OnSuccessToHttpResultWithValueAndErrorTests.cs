using System.Threading.Tasks;
using CSharp.Functional.HttpResultMonad;
using CSharp.Functional.HttpResultMonad.Extensions.HttpResultWithValueAndError.OnSuccess;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.HttpResultMonad.Extensions.HttpResultWithValueAndError.OnSuccess.WithAsyncBothOperands
{
    public class OnSuccessToHttpResultWithValueAndErrorTests_OnSuccessFunc_From_TValue_To_Task_Of_HttpResultWithValueAndError_Of_KValue_TError
    {
        [Fact]
        public async Task OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResult.Ok<int, string>(1))
                .OnSuccessToHttpResultWithValueAndError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeTrue();

            Task<HttpResult<string, string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResult.Ok<string, string>("value"));
            }
        }

        [Fact]
        public async Task OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResult.Fail<int, string>("error"))
                .OnSuccessToHttpResultWithValueAndError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeFalse();

            Task<HttpResult<string, string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResult.Ok<string, string>("value"));
            }
        }

        [Fact]
        public async Task OnSuccess_propagates_value_into_function_if_result_is_ok()
        {
            var propagatedValue = 0;
            var value = 1;
            var result = await Task.FromResult(HttpResult.Ok<int, string>(value))
                .OnSuccessToHttpResultWithValueAndError(i => OnSuccessFunc(i));

            propagatedValue.ShouldBe(value);

            Task<HttpResult<string, string>> OnSuccessFunc(int val)
            {
                propagatedValue = val;
                return Task.FromResult(HttpResult.Ok<string, string>("value"));
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_contains_value_from_function_if_result_is_ok()
        {
            var newValue = "abc";
            var result = await Task.FromResult(HttpResult.Ok<int, string>(1))
                .OnSuccessToHttpResultWithValueAndError(i => Task.FromResult(HttpResult.Ok<string, string>(newValue)));

            result.Value.ShouldBe(newValue);
        }

        [Fact]
        public async Task OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = await Task.FromResult(HttpResult.Fail<int, string>(error))
                .OnSuccessToHttpResultWithValueAndError(i => Task.FromResult(HttpResult.Ok<int, string>(2)));

            result.Error.ShouldBe(error);
        }

        [Fact]
        public async Task OnSuccess_uses_http_state_from_new_result_if_result_is_ok()
        {
            var originalHttpState = Test.CreateHttpStateA();
            var newHttpState = Test.CreateHttpStateB();
            var result = await Task.FromResult(HttpResult.Ok<int, string>(1, originalHttpState))
                .OnSuccessToHttpResultWithValueAndError(i => Task.FromResult(HttpResult.Ok<int, string>(2, newHttpState)));

            result.HttpState.ShouldBe(newHttpState);
        }

        [Fact]
        public async Task OnSuccess_propagates_http_state_if_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var onSuccessHttpState = Test.CreateHttpStateB();
            var result = await Task.FromResult(HttpResult.Fail<int, string>("error", httpState))
                .OnSuccessToHttpResultWithValueAndError(i => Task.FromResult(HttpResult.Ok<int, string>(2, onSuccessHttpState)));

            result.HttpState.ShouldBe(httpState);
        }
    }
}
