using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndError
{
    [Trait("Monad", "Result")]
    public class ResultWithValueAndErrorEqualityTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_results_with_same_value_is_true()
        {
            var value = "abc";
            var result1 = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_results_with_different_value_is_false()
        {
            var result1 = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_same_value_is_false()
        {
            var value = "abc";
            var result1 = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_value_is_true()
        {
            var result1 = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_with_same_error_is_true()
        {
            var error = "abc";
            var result1 = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_with_different_error_is_false()
        {
            var result1 = Result.Fail<string, string>("abc");
            var result2 = Result.Fail<string, string>("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_with_same_error_is_false()
        {
            var error = "abc";
            var result1 = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_with_different_error_is_true()
        {
            var result1 = Result.Fail<string, string>("abc");
            var result2 = Result.Fail<string, string>("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = Result.Ok<string, string>("abc");
            var errorResult = Result.Fail<string, string>("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = Result.Ok<string, string>("abc");
            var errorResult = Result.Fail<string, string>("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_true_if_object_is_ok_result_with_same_value()
        {
            var value = "abc";
            var result = Result.Ok<string, string>(value);
            object someObject = Result.Ok<string, string>(value);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_ok_result_with_different_value()
        {
            var result = Result.Ok<string, string>("abc");
            object someObject = Result.Ok<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_not_result()
        {
            var value = "abc";
            var result = Result.Ok<string, string>(value);
            object someObject = value;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_true_if_object_is_fail_result_with_same_error()
        {
            var error = "abc";
            var result = Result.Fail<string, string>(error);
            object someObject = Result.Fail<string, string>(error);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_fail_result_with_different_value()
        {
            var result = Result.Fail<string, string>("abc");
            object someObject = Result.Fail<string, string>("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_not_result()
        {
            var value = "abc";
            var result = Result.Fail<string, string>(value);
            object someObject = value;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_true_if_they_have_the_same_value()
        {
            var value = "abc";
            var result = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_false_if_they_do_not_have_the_same_value()
        {
            var result = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_true_if_they_have_the_same_error()
        {
            var error = "abc";
            var result = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_false_if_they_do_not_have_the_same_error()
        {
            var result = Result.Fail<string, string>("abc");
            var result2 = Result.Fail<string, string>("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result = Result.Ok<string, string>("abc");
            var result2 = Result.Fail<string, string>("abc");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
