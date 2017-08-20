using Shouldly;
using Xunit;

namespace MaybeMonad.Tests.Equality
{
    [Trait("Monad", "Maybe")]
    public class MaybeInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_maybe_and_null_is_true()
        {
            var maybe = Maybe.From("abc");
            var isEqual = maybe != null;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_value_and_maybe_with_equal_value_is_false()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            var isDifferent = maybe != value;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_value_and_maybe_with_different_value_is_true()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            var isDifferent = maybe != "zzz";
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_maybes_with_equal_value_is_false()
        {
            var value = "abc";
            var maybe1 = Maybe.From(value);
            var maybe2 = Maybe.From(value);
            var isDifferent = maybe1 != maybe2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_maybes_with_different_value_is_true()
        {
            var maybe1 = Maybe.From("abc");
            var maybe2 = Maybe.From("zzz");
            var isDifferent = maybe1 != maybe2;
            isDifferent.ShouldBeTrue();
        }
    }
}
