using System.Threading.Tasks;
using HttpResultMonad;
using ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndError.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.HttpResultMonad.Tests.ResultWithValueAndError.OnSuccess.WithAsyncRightOperand
{
    public class OnSuccessToHttpResultWithErrorTests_OnSuccessFunc_from_TValue_to_HttpResultWithError_of_TError
    {
        [Fact]
        public async Task OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = await Result.Ok<int, string>(1)
                .OnSuccessToHttpResultWithError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeTrue();

            Task<HttpResultWithError<string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResultWithError.Ok<string>());
            }
        }

        [Fact]
        public async Task OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Result.Fail<int, string>("error")
                .OnSuccessToHttpResultWithError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeFalse();

            Task<HttpResultWithError<string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResultWithError.Ok<string>());
            }
        }

        [Fact]
        public async Task OnSuccess_propagates_value_into_function_if_result_is_ok()
        {
            var propagatedValue = 0;
            var value = 1;
            var result = await Result.Ok<int, string>(value)
                .OnSuccessToHttpResultWithError(i => OnSuccessFunc(i));

            propagatedValue.ShouldBe(value);

            Task<HttpResultWithError<string>> OnSuccessFunc(int val)
            {
                propagatedValue = val;
                return Task.FromResult(HttpResultWithError.Ok<string>());
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_contains_error_from_function_if_result_is_ok()
        {
            var error = "abc";
            var result = await Result.Ok<int, string>(1)
                .OnSuccessToHttpResultWithError(i => Task.FromResult(HttpResultWithError.Fail(error)));

            result.Error.ShouldBe(error);
        }

        [Fact]
        public async Task OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = await Result.Fail<int, string>(error)
                .OnSuccessToHttpResultWithError(i => Task.FromResult(HttpResultWithError.Fail("new error")));

            result.Error.ShouldBe(error);
        }
    }
}
