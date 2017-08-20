using System.Threading.Tasks;
using HttpResultMonad;
using ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndErrorMonad.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.HttpResultMonad.Tests.ResultWithValueAndErrorMonad.OnSuccess.WithAsyncRightOperand
{
    [Trait("Extensions", "ResultWithValueAndError")]
    public class OnSuccessToHttpResultWithErrorTests2
    {
        [Fact]
        public async Task OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = await Result.Ok<int, string>(1)
                .OnSuccessToHttpResultWithValueAndError(x => OnSuccessFunc(x));

            functionExecuted.ShouldBeTrue();

            Task<HttpResult<string, string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResult.Ok<string, string>("abc"));
            }
        }

        [Fact]
        public async Task OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Result.Fail<int, string>("error")
                .OnSuccessToHttpResultWithValueAndError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeFalse();

            Task<HttpResult<string, string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResult.Ok<string, string>("abc"));
            }
        }

        [Fact]
        public async Task OnSuccess_propagates_value_into_function_if_result_is_ok()
        {
            var propagatedValue = 0;
            var value = 1;
            var result = await Result.Ok<int, string>(value)
                .OnSuccessToHttpResultWithValueAndError(i => OnSuccessFunc(i));

            propagatedValue.ShouldBe(value);

            Task<HttpResult<string, string>> OnSuccessFunc(int val)
            {
                propagatedValue = val;
                return Task.FromResult(HttpResult.Ok<string, string>("abc"));
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_contains_error_from_function_if_result_is_ok()
        {
            var error = "abc";
            var result = await Result.Ok<int, string>(1)
                .OnSuccessToHttpResultWithValueAndError(i => Task.FromResult(HttpResult.Fail<string, string>(error)));

            result.Error.ShouldBe(error);
        }

        [Fact]
        public async Task OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = await Result.Fail<int, string>(error)
                .OnSuccessToHttpResultWithValueAndError(i => Task.FromResult(HttpResult.Fail<string, string>("new error")));

            result.Error.ShouldBe(error);
        }
    }
}
