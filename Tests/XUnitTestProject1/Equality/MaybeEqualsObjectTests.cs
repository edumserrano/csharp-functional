using Shouldly;
using Xunit;

namespace MaybeMonad.Tests.Equality
{
    [Trait("Monad", "Maybe")]
    public class MaybeEqualsObjectTests
    {
        [Fact]
        public void Equals_between_maybe_of_T_and_object_of_type_T_is_true_if_values_are_equal()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            object someObject = value;
            var isEqual = maybe.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_maybe_of_T_and_object_of_type_K_which_is_not_maybe_is_false()
        {
            var maybe = Maybe.From("abc");
            object someObject = 1;
            var isEqual = maybe.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_maybe_of_T_and_object_of_type_K_which_is_maybe_of_T_is_true_if_both_values_are_equal()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            object someObject = Maybe.From(value);
            var isEqual = maybe.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_maybe_of_T_and_object_of_type_K__which_is_maybe_of_Z_is_false()
        {
            var maybe = Maybe.From("abc");
            object someObject = Maybe.From(1);
            var isEqual = maybe.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
    }
}
