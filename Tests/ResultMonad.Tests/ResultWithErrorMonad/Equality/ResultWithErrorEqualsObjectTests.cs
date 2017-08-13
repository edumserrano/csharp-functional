using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithErrorMonad.Equality
{
    [Trait("Monad", "ResultWithError")]
    public class ResultWithErrorEqualsObjectTests
    {
        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_true_if_object_equals_the_error_in_ResultWithError()
        {
            var error = "abc";
            var result = ResultWithError.Fail(error);
            object someObject = error;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_false_if_object_is_not_of_the_same_type_as_the_error_in_ResultWithError()
        {
            var result = ResultWithError.Fail("abc");
            object someObject = 1;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_false_if_object_does_not_equal_the_error_in_ResultWithError()
        {
            var result = ResultWithError.Fail("abc");
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_true_if_object_is_fail_ResultWithError_and_both_errors_are_equal()
        {
            var error = "abc";
            var result = ResultWithError.Fail(error);
            object someObject = ResultWithError.Fail(error);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_ResultWithError_and_object_is_false_if_object_is_fail_ResultWithError_and_both_errors_are_not_equal()
        {
            var result = ResultWithError.Fail("abc");
            object someObject = ResultWithError.Fail("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }


        [Fact]
        public void Equals_between_ok_ResultWithError_and_object_is_true_if_object_is_ok_result()
        {
            var result = ResultWithError.Ok<string>();
            object someObject = ResultWithError.Ok<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_ResultWithError_and_object_is_false_if_object_is_not_ok_result()
        {
            var result = ResultWithError.Ok<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
    }
}
