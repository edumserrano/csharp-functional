using ResultMonad.Extensions.ResultWithValueAndErrorMonad.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithValueAndErrorMonad.OnSuccess
{
    [Trait("Extensions", "ResultWithValueError")]
    public class OnSuccessToResultWithValueAndErrorTests_with_onSuccessAction_From_TValue
    {
        [Fact]
        public void OnSuccess_executes_action_if_result_is_ok()
        {
            var functionExecuted = false;
            var result = Result.Ok<int, string>(1)
                .OnSuccess(i => OnSuccesssAction(i));

            functionExecuted.ShouldBeTrue();

            void OnSuccesssAction(int value)
            {
                functionExecuted = true;
            }
        }

        [Fact]
        public void OnSuccess_does_not_execute_action_if_result_is_fail()
        {
            var functionExecuted = false;
            var result = Result.Fail<int, string>("error")
                .OnSuccess(i => OnSuccesssAction(i));

            functionExecuted.ShouldBeFalse();

            void OnSuccesssAction(int value)
            {
                functionExecuted = true;
            }
        }


        [Fact]
        public void OnSuccess_propagates_value_into_action_if_result_is_ok()
        {
            var propagatedValue = 0;
            var value = 1;
            var result = Result.Ok<int, string>(value)
                .OnSuccess(i => OnSuccesssAction(i));

            propagatedValue.ShouldBe(value);

            void OnSuccesssAction(int val)
            {
                propagatedValue = val;
            }
        }

        [Fact]
        public void OnSuccess_new_result_contains_value_from_original_result_if_result_is_ok()
        {
            var value = 1;
            var result = Result.Ok<int, string>(value)
                .OnSuccess(i =>
                {
                    //do something action
                });

            result.Value.ShouldBe(value);
        }

        [Fact]
        public void OnSuccess_propagates_error_if_result_is_fail()
        {
            var error = "error";
            var result = Result.Fail<int, string>(error)
                .OnSuccess(i =>
                {

                });

            result.Error.ShouldBe(error);
        }
    }
}
