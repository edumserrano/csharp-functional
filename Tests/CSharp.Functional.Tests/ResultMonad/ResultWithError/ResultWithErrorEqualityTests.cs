using ResultMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.ResultWithError
{
    public class ResultWithErrorEqualityTests
    {
        [Fact]
        public void Equality_operator_between_error_and_fail_resultError_with_same_error_is_true()
        {
            var error = "abc";
            var result = ResultError.Fail(error);
            var isEqual = result == error;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_error_and_fail_resultError_with_same_error_is_false()
        {
            var error = "abc";
            var result = ResultError.Fail(error);
            var isDifferent = result != error;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_error_and_ok_resultError_is_false()
        {
            var error = "abc";
            var result = ResultError.Ok<string>();
            var isEqual = result == error;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_error_and_ok_resultError_is_true()
        {
            var error = "abc";
            var result = ResultError.Ok<string>();
            var isDifferent = result != error;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_resultErrors_with_same_error_is_true()
        {
            var error = "abc";
            var result1 = ResultError.Fail(error);
            var result2 = ResultError.Fail(error);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_resultErrors_with_different_error_is_false()
        {
            var result1 = ResultError.Fail("abc");
            var result2 = ResultError.Fail("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_resultErrors_with_same_error_is_false()
        {
            var error = "abc";
            var result1 = ResultError.Fail(error);
            var result2 = ResultError.Fail(error);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_resultErrors_with_different_error_is_true()
        {
            var result1 = ResultError.Fail("abc");
            var result2 = ResultError.Fail("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_resultErrors_is_true()
        {
            var result1 = ResultError.Ok<string>();
            var result2 = ResultError.Ok<string>();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_resultErrors_is_false()
        {
            var result1 = ResultError.Ok<string>();
            var result2 = ResultError.Ok<string>();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_resultError_and_fail_resultError_is_false()
        {
            var okResult = ResultError.Ok<string>();
            var errorResult = ResultError.Fail("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_resultError_and_fail_resultError_is_true()
        {
            var okResult = ResultError.Ok<string>();
            var errorResult = ResultError.Fail("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_resultError_and_object_is_true_if_object_is_the_same_as_the_error_in_resultError()
        {
            var error = "abc";
            var result = ResultError.Fail(error);
            object someObject = error;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_resultError_and_object_is_false_if_object_is_not_the_same_as_the_error_in_resultError()
        {
            var result = ResultError.Fail("abc");
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_resultError_and_object_is_true_if_object_is_fail_resultError_with_same_error()
        {
            var error = "abc";
            var result = ResultError.Fail(error);
            object someObject = ResultError.Fail(error);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_resultError_and_object_is_false_if_object_is_fail_resultError_with_different_error()
        {
            var result = ResultError.Fail("abc");
            object someObject = ResultError.Fail("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_ok_resultError_and_object_is_true_if_object_is_ok_result()
        {
            var result = ResultError.Ok<string>();
            object someObject = ResultError.Ok<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_resultError_and_object_is_false_if_object_is_not_ok_result()
        {
            var result = ResultError.Ok<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_resultErrors_is_true_if_they_have_the_same_error()
        {
            var error = "abc";
            var result = ResultError.Fail(error);
            var result2 = ResultError.Fail(error);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_resultErrors_is_false_if_they_do_not_have_the_same_error()
        {
            var result = ResultError.Fail("abc");
            var result2 = ResultError.Fail("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_resultErrors_is_true()
        {
            var result = ResultError.Ok<string>();
            var result2 = ResultError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_resultError_and_fail_resultError_is_false()
        {
            var result = ResultError.Fail("abc");
            var result2 = ResultError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
