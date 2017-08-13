using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimpleMonad.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleEqualsObjectTests
    {
        [Fact]
        public void Equals_betwwen_ok_result_and_object_is_true_if_object_is_ok_result()
        {
            var result1 = Result.Ok();
            object result2 = Result.Ok();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_true_if_object_is_fail_result()
        {
            var result1 = Result.Fail();
            object result2 = Result.Fail();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_not_ok_result()
        {
            var result1 = Result.Ok();
            object result2 = "abc";
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_not_fail_result()
        {
            var result1 = Result.Fail();
            object result2 = "abc";
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
