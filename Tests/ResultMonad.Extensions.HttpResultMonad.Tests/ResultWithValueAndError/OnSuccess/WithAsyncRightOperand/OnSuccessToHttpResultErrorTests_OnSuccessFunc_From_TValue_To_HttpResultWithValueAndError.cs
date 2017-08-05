using System.Threading.Tasks;
using HttpResultMonad;
using ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndError.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.HttpResultMonad.Tests.ResultWithValueAndError.OnSuccess.WithAsyncRightOperand
{
#warning this is one example of the naming problem that i have on these kind of tests. I have reached the max number of chars for the file path
    //I should actually name this class with an extra appended info (_Of_K_Value_TError) but can't because of os system limitation on filepath length
    public class OnSuccessToHttpResultErrorTests_OnSuccessFunc_From_TValue_To_HttpResultWithValueAndError
    {
        [Fact]
        public async Task OnSuccess_executes_function_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = await Result.Ok<int, string>(1)
                .OnSuccessToHttpResultError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeTrue();

            Task<HttpResultError<string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResultError.Ok<string>());
            }
        }

        [Fact]
        public async Task OnSuccess_does_not_execute_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Result.Fail<int, string>("error")
                .OnSuccessToHttpResultError(i => OnSuccessFunc(i));

            functionExecuted.ShouldBeFalse();

            Task<HttpResultError<string>> OnSuccessFunc(int value)
            {
                functionExecuted = true;
                return Task.FromResult(HttpResultError.Ok<string>());
            }
        }

        [Fact]
        public async Task OnSuccess_propagates_value_into_function_if_result_is_ok()
        {
            var propagatedValue = 0;
            var value = 1;
            var result = await Result.Ok<int, string>(value)
                .OnSuccessToHttpResultError(i => OnSuccessFunc(i));

            propagatedValue.ShouldBe(value);

            Task<HttpResultError<string>> OnSuccessFunc(int val)
            {
                propagatedValue = val;
                return Task.FromResult(HttpResultError.Ok<string>());
            }
        }

        [Fact]
        public async Task OnSuccess_new_result_contains_error_from_function_if_result_is_ok()
        {
            var error = "abc";
            var result = await Result.Ok<int, string>(1)
                .OnSuccessToHttpResultError(i => Task.FromResult(HttpResultError.Fail(error)));

            result.Error.ShouldBe(error);
        }

        [Fact]
        public async Task OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = await Result.Fail<int, string>(error)
                .OnSuccessToHttpResultError(i => Task.FromResult(HttpResultError.Fail("new error")));

            result.Error.ShouldBe(error);
        }
    }
}
