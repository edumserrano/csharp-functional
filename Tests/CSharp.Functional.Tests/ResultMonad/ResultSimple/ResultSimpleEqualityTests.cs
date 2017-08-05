using ResultMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.ResultSimple
{
    public class ResultSimpleEqualityTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_results_is_true()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_is_false()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_is_true()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = Result.Ok();
            var failResult = Result.Fail();
            var isEqual = okResult == failResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = Result.Ok();
            var failResult = Result.Fail();
            var isEqual = okResult != failResult;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_true()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_reults_is_trues()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result1 = Result.Ok();
            var result2 = Result.Fail();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

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
