using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "ResultWithValueAndError")]
    public class ResultWithValueAndErrorEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_ResultWithValueAndError_with_same_value_is_true()
        {
            var value = "abc";
            var result1 = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_ResultWithValueAndError_with_different_value_is_false()
        {
            var result1 = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equality_operator_between_two_fail_ResultWithValueAndError_with_same_error_is_true()
        {
            var error = "abc";
            var result1 = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultWithValueAndError_with_different_error_is_false()
        {
            var result1 = Result.Fail<string, string>("abc");
            var result2 = Result.Fail<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equality_operator_between_ok_ResultWithValueAndError_and_fail_ResultWithValueAndError_is_false()
        {
            var okResult = Result.Ok<string, string>("abc");
            var errorResult = Result.Fail<string, string>("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_ResultWithValueAndError_with_same_value_is_false()
        {
            var value = "abc";
            var result1 = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_ResultWithValueAndError_with_different_value_is_true()
        {
            var result1 = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_ResultWithValueAndError_with_same_error_is_false()
        {
            var error = "abc";
            var result1 = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_ResultWithValueAndError_with_different_error_is_true()
        {
            var result1 = Result.Fail<string, string>("abc");
            var result2 = Result.Fail<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_ok_ResultWithValueAndError_and_fail_ResultWithValueAndError_is_true()
        {
            var okResult = Result.Ok<string, string>("abc");
            var errorResult = Result.Fail<string, string>("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
