using Shouldly;
using Xunit;

namespace MaybeMonad.Tests.Equality
{
    [Trait("Monad", "Maybe")]
    public class MaybeGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_empty_maybes_is_equal()
        {
            var maybe1 = Maybe<string>.Nothing;
            var maybe2 = Maybe<string>.Nothing;
            maybe1.GetHashCode().ShouldBe(maybe2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_maybes_with_same_value_is_equal()
        {
            var maybe1 = Maybe.From("abc");
            var maybe2 = Maybe.From("abc");
            maybe1.GetHashCode().ShouldBe(maybe2.GetHashCode());
        }
        
        [Fact]
        public void GetHasCode_between_two_maybes_with_different_value_is_not_equal()
        {
            var maybe1 = Maybe.From("abc");
            var maybe2 = Maybe.From("zzz");
            maybe1.GetHashCode().ShouldNotBe(maybe2.GetHashCode());
        }
    }
}
