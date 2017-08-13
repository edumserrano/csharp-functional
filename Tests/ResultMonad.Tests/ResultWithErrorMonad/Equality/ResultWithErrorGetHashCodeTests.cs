using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithErrorMonad.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class ResultWithErrorGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_ok_ResultWithError_of_same_type_is_equal()
        {
            var result1 = ResultWithError.Ok<string>();
            var result2 = ResultWithError.Ok<string>();
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_ok_ResultWithError_of_different_types_is_not_equal()
        {
            var result1 = ResultWithError.Ok<int>();
            var result2 = ResultWithError.Ok<string>();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_ResultWithError_is_equal_if_both_errors_are_equal()
        {
            var error = "abc";
            var result1 = ResultWithError.Fail(error);
            var result2 = ResultWithError.Fail(error);
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_ResultWithError_is_not_equal_if_both_errors_are_not_equal()
        {
            var result1 = ResultWithError.Fail("abc");
            var result2 = ResultWithError.Fail("zzz");
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_ok_ResultWithError_and_fail_result_is_not_equal()
        {
            var result1 = ResultWithError.Ok<string>();
            var result2 = ResultWithError.Fail("abc");
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }
    }
}
