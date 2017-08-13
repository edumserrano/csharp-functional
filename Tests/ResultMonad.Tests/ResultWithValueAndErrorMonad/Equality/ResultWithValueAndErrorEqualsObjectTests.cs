using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "ResultWithValueAndError")]
    public class ResultWithValueAndErrorEqualsObjectTests
    {
        [Fact]
        public void Equals_between_ok_ResultWithValueAndError_and_object_is_true_if_object_is_ok_ResultWithValueAndError_and_both_values_are_equal()
        {
            var value = "abc";
            var result = Result.Ok<string, string>(value);
            object someObject = Result.Ok<string, string>(value);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_ResultWithValueAndError_and_object_is_false_if_object_is_ok_ResultWithValueAndError_and_both_values_are_not_equal()
        {
            var result = Result.Ok<string, string>("abc");
            object someObject = Result.Ok<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_ResultWithValueAndError_and_object_is_false_if_object_is_not_ResultWithValueAndError()
        {
            var value = "abc";
            var result = Result.Ok<string, string>(value);
            object someObject = value;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_ResultWithValueAndError_and_object_is_true_if_object_is_fail_ResultWithValueAndError_and_both_errors_are_equal()
        {
            var error = "abc";
            var result = Result.Fail<string, string>(error);
            object someObject = Result.Fail<string, string>(error);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_ResultWithValueAndError_and_object_is_false_if_object_is_fail_ResultWithValueAndError_and_both_errors_are_not_equal()
        {
            var result = Result.Fail<string, string>("abc");
            object someObject = Result.Fail<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_ResultWithValueAndError_and_object_is_false_if_object_is_not_ResultWithValueAndError()
        {
            var value = "abc";
            var result = Result.Fail<string, string>(value);
            object someObject = value;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_fail_ResultWithValueAndError_and_object_is_false_if_object_is_not_ResultWithValueAndError_of_the_same_value_and_error_type()
        {
            var error = "abc";
            var result = Result.Fail<string, string>(error);
            object someObject = Result.Fail<int, string>(error);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();

            var value = 1;
            var result2 = Result.Ok<int, bool>(value);
            object someObject2 = Result.Ok<int, string>(value);
            var isEqual2 = result2.Equals(someObject2);
            isEqual2.ShouldBeFalse();
        }
    }
}
