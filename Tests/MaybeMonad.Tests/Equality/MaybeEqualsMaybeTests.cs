using Shouldly;
using Xunit;

namespace MaybeMonad.Tests.Equality
{
    [Trait("Monad", "Maybe")]
    public class MaybeEqualsMaybeTests
    {
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
        public void Equals_between_maybes_with_different_value_is_false()
        {
            var maybe1 = Maybe.From("abc");
            var maybe2 = Maybe.From("zzz");
            var isEqual = maybe1.Equals(maybe2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_empty_maybes_is_true()
        {
            var maybe1 = Maybe<string>.Nothing;
            var maybe2 = Maybe<string>.Nothing;
            var isEqual = maybe1.Equals(maybe2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_maybe_with_value_and_empty_maybe_is_false()
        {
            var maybe1 = Maybe.From("abc");
            var maybe2 = Maybe<string>.Nothing;
            var isEqual = maybe1.Equals(maybe2);
            var isEqual2 = maybe2.Equals(maybe1);
            isEqual.ShouldBeFalse();
            isEqual2.ShouldBeFalse();
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
        public void Equals_between_value_and_maybe_with_different_value_is_false()
        {
            var maybe = Maybe.From("abc");
            var isEqual = maybe.Equals("zzz");
            isEqual.ShouldBeFalse();
        }
    }
}
