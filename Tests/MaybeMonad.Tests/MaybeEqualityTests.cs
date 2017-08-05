using Shouldly;
using Xunit;

namespace MaybeMonad.Tests
{
    public class MaybeEqualityTests
    {
        [Fact]
        public void Equality_operator_between_value_and_maybe_with_same_value_is_true()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            var isEqual = maybe == value;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_value_and_maybe_with_different_value_is_false()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            var isEqual = maybe == "zzz";
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_value_and_maybe_with_same_value_is_false()
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
        public void Equality_operator_between_maybes_with_same_value_is_true()
        {
            var value = "abc";
            var maybe1 = Maybe.From(value);
            var maybe2 = Maybe.From(value);
            var isEqual = maybe1 == maybe2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_maybes_with_different_value_is_false()
        {
            var maybe1 = Maybe.From("abc");
            var maybe2 = Maybe.From("zzz");
            var isEqual = maybe1 == maybe2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_maybes_with_same_value_is_false()
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

        [Fact]
        public void Equals_between_maybes_with_same_value_is_true()
        {
            var value = "abc";
            var maybe1 = Maybe.From(value);
            var maybe2 = Maybe.From(value);
            var isEqual = maybe1.Equals(maybe2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_value_and_maybe_with_same_value_is_true()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            var isEqual = maybe.Equals(value);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_maybes_with_different_value_is_false()
        {
            var maybe1 = Maybe.From("abc");
            var maybe2 = Maybe.From("zzz");
            var isEqual = maybe1.Equals(maybe2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_value_and_maybe_with_different_value_is_false()
        {
            var maybe = Maybe.From("abc");
            var isEqual = maybe.Equals("zzz");
            isEqual.ShouldBeFalse();
        }
    }
}
