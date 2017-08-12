using ResultMonad.Extensions.ResultWithError.Ensure;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithError.Ensure
{
    public class EnsureToResultWithErrorTests
    {
        [Fact]
        public void Ensure_executes_predicate_if_result_is_ok()
        {
            var error = "error1";
            var predicateExecuted = false;

            var result = ResultMonad.ResultWithError.Ok<string>()
                .EnsureToResultWithError(Predicate, error);

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
            var error = "error1";
            var predicateExecuted = false;

            var result = ResultMonad.ResultWithError.Fail(error)
                .EnsureToResultWithError(Predicate, error);

            predicateExecuted.ShouldBeFalse();

            bool Predicate()
            {
                predicateExecuted = true;
                return false;
            }
        }


        [Fact]
        public void Ensure_does_not_change_result_if_predicate_evaluates_to_true()
        {
            var error = "error";
            var predicateResult = true;

            var result = ResultMonad.ResultWithError.Ok<string>()
                .EnsureToResultWithError(() => predicateResult, error);

            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ensure_uses_predicate_error_if_predicate_evaluates_to_false()
        {
            var error = "error";
            var predicateResult = false;

            var result = ResultMonad.ResultWithError.Ok<string>()
                .EnsureToResultWithError(() => predicateResult, error);

            result.Error.ShouldBe(error);
        }
    }
}
