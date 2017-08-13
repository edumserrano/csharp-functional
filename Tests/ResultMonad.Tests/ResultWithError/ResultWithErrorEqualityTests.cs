using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithError
{
    [Trait("Monad", "Result")]
    public class ResultWithErrorEqualityTests
    {
        [Fact]
        public void Equality_operator_between_error_and_fail_ResultWithError_with_same_error_is_true()
        {
            var error = "abc";
            var result = ResultMonad.ResultWithError.Fail(error);
            var isEqual = result == error;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_error_and_fail_ResultWithError_with_same_error_is_false()
        {
            var error = "abc";
            var result = ResultMonad.ResultWithError.Fail(error);
            var isDifferent = result != error;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_error_and_ok_ResultWithError_is_false()
        {
            var error = "abc";
            var result = ResultMonad.ResultWithError.Ok<string>();
            var isEqual = result == error;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_error_and_ok_ResultWithError_is_true()
        {
            var error = "abc";
            var result = ResultMonad.ResultWithError.Ok<string>();
            var isDifferent = result != error;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultWithErrors_with_same_error_is_true()
        {
            var error = "abc";
            var result1 = ResultMonad.ResultWithError.Fail(error);
            var result2 = ResultMonad.ResultWithError.Fail(error);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultWithErrors_with_different_error_is_false()
        {
            var result1 = ResultMonad.ResultWithError.Fail("abc");
            var result2 = ResultMonad.ResultWithError.Fail("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_ResultWithErrors_with_same_error_is_false()
        {
            var error = "abc";
            var result1 = ResultMonad.ResultWithError.Fail(error);
            var result2 = ResultMonad.ResultWithError.Fail(error);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_ResultWithErrors_with_different_error_is_true()
        {
            var result1 = ResultMonad.ResultWithError.Fail("abc");
            var result2 = ResultMonad.ResultWithError.Fail("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_ResultWithErrors_is_true()
        {
            var result1 = ResultMonad.ResultWithError.Ok<string>();
            var result2 = ResultMonad.ResultWithError.Ok<string>();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_ResultWithErrors_is_false()
        {
            var result1 = ResultMonad.ResultWithError.Ok<string>();
            var result2 = ResultMonad.ResultWithError.Ok<string>();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_ResultWithError_and_fail_ResultWithError_is_false()
        {
            var okResult = ResultMonad.ResultWithError.Ok<string>();
            var errorResult = ResultMonad.ResultWithError.Fail("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_ResultWithError_and_fail_ResultWithError_is_true()
        {
            var okResult = ResultMonad.ResultWithError.Ok<string>();
            var errorResult = ResultMonad.ResultWithError.Fail("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_true_if_object_is_the_same_as_the_error_in_ResultWithError()
        {
            var error = "abc";
            var result = ResultMonad.ResultWithError.Fail(error);
            object someObject = error;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_false_if_object_is_not_the_same_as_the_error_in_ResultWithError()
        {
            var result = ResultMonad.ResultWithError.Fail("abc");
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_true_if_object_is_fail_ResultWithError_with_same_error()
        {
            var error = "abc";
            var result = ResultMonad.ResultWithError.Fail(error);
            object someObject = ResultMonad.ResultWithError.Fail(error);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_false_if_object_is_fail_ResultWithError_with_different_error()
        {
            var result = ResultMonad.ResultWithError.Fail("abc");
            object someObject = ResultMonad.ResultWithError.Fail("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_ok_ResultWithError_and_object_is_true_if_object_is_ok_result()
        {
            var result = ResultMonad.ResultWithError.Ok<string>();
            object someObject = ResultMonad.ResultWithError.Ok<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_ResultWithError_and_object_is_false_if_object_is_not_ok_result()
        {
            var result = ResultMonad.ResultWithError.Ok<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_ResultWithErrors_is_true_if_they_have_the_same_error()
        {
            var error = "abc";
            var result = ResultMonad.ResultWithError.Fail(error);
            var result2 = ResultMonad.ResultWithError.Fail(error);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_ResultWithErrors_is_false_if_they_do_not_have_the_same_error()
        {
            var result = ResultMonad.ResultWithError.Fail("abc");
            var result2 = ResultMonad.ResultWithError.Fail("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_ResultWithErrors_is_true()
        {
            var result = ResultMonad.ResultWithError.Ok<string>();
            var result2 = ResultMonad.ResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_ResultWithError_and_fail_ResultWithError_is_false()
        {
            var result = ResultMonad.ResultWithError.Fail("abc");
            var result2 = ResultMonad.ResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
