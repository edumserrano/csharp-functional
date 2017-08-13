using ResultMonad.Extensions.ResultWithValueAndErrorMonad.Ensure;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithValueAndErrorMonad.Ensure
{
    [Trait("Extensions", "ResultWithValueError")]
    public class EnsureToResultWithValueAndErrorTests_Predicate_From_TValue_With_Func_From_TValue_To_TError_If_Fails
    {
        [Fact]
        public void Ensure_executes_predicate_if_result_is_ok()
        {
            var error = "error";
            var value = 1;
            var predicateExecuted = false;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => Predicate(i), i => ErrorFunc(i));

            predicateExecuted.ShouldBeTrue();

            bool Predicate(int i)
            {
                predicateExecuted = true;
                return true;
            }

            string ErrorFunc(int i)
            {
                return error;
            }
        }

        [Fact]
        public void Ensure_does_not_execute_predicate_if_result_is_fail()
        {
            var error1 = "error1";
            var error2 = "error2";
            var predicateExecuted = false;

            var result = Result.Fail<int, string>(error1)
                .EnsureToResultWithValueAndError(i => Predicate(i), i => ErrorFunc(i));

            predicateExecuted.ShouldBeFalse();

            bool Predicate(int i)
            {
                predicateExecuted = true;
                return true;
            }

            string ErrorFunc(int i)
            {
                return error2;
            }
        }

        [Fact]
        public void Ensure_does_not_execute_error_function_if_predicate_evaluates_to_true()
        {
            var error = "error1";
            var value = 1;
            var errorFunctionExecuted = false;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => true, i => ErrorFunc(i));

            errorFunctionExecuted.ShouldBeFalse();

            string ErrorFunc(int i)
            {
                errorFunctionExecuted = true;
                return error;
            }
        }

        [Fact]
        public void Ensure_executes_error_function_if_predicate_evaluates_to_false()
        {
            var error = "error1";
            var value = 1;
            var errorFunctionExecuted = false;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => false, i => ErrorFunc(i));

            errorFunctionExecuted.ShouldBeTrue();

            string ErrorFunc(int i)
            {
                errorFunctionExecuted = true;
                return error;
            }
        }

        [Fact]
        public void Ensure_does_not_change_result_if_predicate_evaluates_to_true()
        {
            var error = "error";
            var value = 1;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => true, i => ErrorFunc(i));

            result.Value.ShouldBe(value);

            string ErrorFunc(int i)
            {
                return error;
            }
        }

        [Fact]
        public void Ensure_uses_predicate_error_if_predicate_evaluates_to_false()
        {
            var error = "error";
            var value = 1;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => false, i => ErrorFunc(i));

            result.Error.ShouldBe(error);

            string ErrorFunc(int i)
            {
                return error;
            }
        }

        [Fact]
        public void Ensure_propagates_value_into_predicate_if_result_is_ok()
        {
            var error = "error";
            var value = 1;
            var propagatedValue = 0;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => Predicate(i), i => ErrorFunc(i));

            propagatedValue.ShouldBe(value);

            bool Predicate(int i)
            {
                propagatedValue = i;
                return true;
            }

            string ErrorFunc(int i)
            {
                return error;
            }
        }
    }
}
