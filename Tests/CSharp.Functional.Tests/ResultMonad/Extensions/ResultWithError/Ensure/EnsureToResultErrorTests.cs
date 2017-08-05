using ResultMonad;
using ResultMonad.Extensions.ResultWithError.Ensure;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.Extensions.ResultWithError.Ensure
{
    public class EnsureToResultErrorTests
    {
        [Fact]
        public void Ensure_executes_predicate_if_result_is_ok()
        {
            var error = "error1";
            var predicateExecuted = false;

            var result = ResultError.Ok<string>()
                .EnsureToResultError(Predicate, error);

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

            var result = ResultError.Fail(error)
                .EnsureToResultError(Predicate, error);

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

            var result = ResultError.Ok<string>()
                .EnsureToResultError(() => predicateResult, error);

            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ensure_uses_predicate_error_if_predicate_evaluates_to_false()
        {
            var error = "error";
            var predicateResult = false;

            var result = ResultError.Ok<string>()
                .EnsureToResultError(() => predicateResult, error);

            result.Error.ShouldBe(error);
        }
    }
}
