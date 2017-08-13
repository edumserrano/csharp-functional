using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimpleMonad.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_ok_ResultSimple_is_equal()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_ResultSimple_is_equal()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }
        
        [Fact]
        public void GetHasCode_between_ok_ResultSimple_and_fail_ResultSimple_is_not_equal()
        {
            var result1 = Result.Ok();
            var result2 = Result.Fail();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }
    }
}
