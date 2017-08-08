using ResultMonad.Extensions.ResultWithValueAndError.Ensure;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithValueAndError.Ensure
{
    public class EnsureToResultWithValueAndErrorTests_Predicate_With_TError_If_Fails
    {
        [Fact]
        public void Ensure_executes_predicate_if_result_is_ok()
        {
            var error = "error";
            var value = 1;
            var predicateExecuted = false;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(Predicate, error);

            predicateExecuted.ShouldBeTrue();

            bool Predicate()
            {
                predicateExecuted = true;
                return true;
            }
        }

        [Fact]
        public void Ensure_does_not_execute_predicate_if_result_is_fail()
        {
            var error1 = "error1";
            var error2 = "error2";
            var predicateExecuted = false;

            var result = Result.Fail<int, string>(error1)
                .EnsureToResultWithValueAndError(Predicate, error2);

            predicateExecuted.ShouldBeFalse();

            bool Predicate()
            {
                predicateExecuted = true;
                return true;
            }
        }

        [Fact]
        public void Ensure_does_not_change_result_if_predicate_evaluates_to_true()
        {
            var error = "error";
            var value = 1;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(() => true, error);

            result.Value.ShouldBe(value);
        }

        [Fact]
        public void Ensure_uses_predicate_error_if_predicate_evaluates_to_false()
        {
            var error = "error";
            var value = 1;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(() => false, error);

            result.Error.ShouldBe(error);
        }
    }
}
