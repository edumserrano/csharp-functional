using ResultMonad.Extensions.ResultWithValueAndErrorMonad.Ensure;
using Shouldly;
using Xunit;

namespace ResultMonad.Extensions.Tests.ResultWithValueAndErrorMonad.Ensure
{
    [Trait("Extensions", "ResultWithValueError")]
    public class EnsureToResultWithValueAndErrorTests_predicate_from_TValue_with_TError_if_fails
    {
        [Fact]
        public void Ensure_executes_predicate_if_result_is_ok()
        {
            var error = "error";
            var value = 1;
            var predicateExecuted = false;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => Predicate(i), error);

            predicateExecuted.ShouldBeTrue();

            bool Predicate(int i)
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
                .EnsureToResultWithValueAndError(i => Predicate(i), error2);

            predicateExecuted.ShouldBeFalse();

            bool Predicate(int i)
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
                .EnsureToResultWithValueAndError(i => true, error);

            result.Value.ShouldBe(value);
        }

        [Fact]
        public void Ensure_uses_predicate_error_if_predicate_evaluates_to_false()
        {
            var error = "error";
            var value = 1;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => false, error);

            result.Error.ShouldBe(error);
        }

        [Fact]
        public void Ensure_propagates_value_into_predicate_if_result_is_ok()
        {
            var error = "error";
            var value = 1;
            var propagatedValue = 0;

            var result = Result.Ok<int, string>(value)
                .EnsureToResultWithValueAndError(i => Predicate(i), error);

            propagatedValue.ShouldBe(value);

            bool Predicate(int i)
            {
                propagatedValue = i;
                return true;
            }
        }
    }
}
