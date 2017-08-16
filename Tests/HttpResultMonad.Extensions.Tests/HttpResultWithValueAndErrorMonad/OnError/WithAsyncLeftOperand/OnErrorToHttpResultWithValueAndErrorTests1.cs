using System.Threading.Tasks;
using HttpResultMonad.Extensions.HttpResultWithValueAndErrorMonad.OnError;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Extensions.Tests.HttpResultWithValueAndErrorMonad.OnError.WithAsyncLeftOperand
{
    [Trait("Extensions", "HttpResultWithValueAndError")]
    public class OnErrorToHttpResultWithValueAndErrorTests1
    {
        [Fact]
        public async Task OnError_executes_function_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = await Task.FromResult(HttpResult.Fail<int, string>("error"))
                .OnErrorToHttpResultWithValueAndError(i => OnErrorFunc(i));

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
            var result = await Task.FromResult(HttpResult.Ok<int, string>(1))
                .OnErrorToHttpResultWithValueAndError(i => OnErrorFunc(i));

            functionExecuted.ShouldBeFalse();

            int OnErrorFunc(string error)
            {
                functionExecuted = true;
                return 1;
            }
        }


        [Fact]
        public async Task OnError_propagates_error_into_function_if_result_is_fail()
        {
            var propagatedValue = "";
            var error = "error";
            var result = await Task.FromResult(HttpResult.Fail<int, string>(error))
                .OnErrorToHttpResultWithValueAndError(i => OnErrorFunc(i));

            propagatedValue.ShouldBe(error);

            int OnErrorFunc(string err)
            {
                propagatedValue = err;
                return 1;
            }
        }

        [Fact]
        public async Task OnError_new_result_contains_error_from_function_if_result_is_fail()
        {
            var newError = "abc";
            var result = await Task.FromResult(HttpResult.Fail<int, string>("error"))
                .OnErrorToHttpResultWithValueAndError(i => newError);

            result.Error.ShouldBe(newError);
        }

        [Fact]
        public async Task OnError_propagates_value_if_result_is_ok()
        {
            var value = 1;
            var result = await Task.FromResult(HttpResult.Ok<int, string>(value))
                .OnErrorToHttpResultWithValueAndError(i => "error");

            result.Value.ShouldBe(value);
        }
    }
}
