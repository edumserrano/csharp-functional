using ResultMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.ResultWithValue
{
    public class ResultWithValueEqualityTests
    {
        [Fact]
        public void Equality_operator_between_value_and_ok_result_with_same_value_is_true()
        {
            var value = "abc";
            var result = Result.Ok(value);
            var isEqual = result == value;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_value_and_ok_result_with_same_value_is_false()
        {
            var value = "abc";
            var result = Result.Ok(value);
            var isDifferent = result != value;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_value_and_fail_result_with_same_value_is_false()
        {
            var value = "abc";
            var result = Result.Fail<string>();
            var isEqual = result == value;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_value_and_fail_result_with_same_value_is_true()
        {
            var value = "abc";
            var result = Result.Fail<string>();
            var isDifferent = result != value;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_results_with_same_value_is_true()
        {
            var value = "abc";
            var result1 = Result.Ok(value);
            var result2 = Result.Ok(value);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_ok_results_with_different_value_is_false()
        {
            var result1 = Result.Ok("abc");
            var result2 = Result.Ok("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_same_value_is_false()
        {
            var value = "abc";
            var result1 = Result.Ok(value);
            var result2 = Result.Ok(value);
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_with_different_value_is_true()
        {
            var result1 = Result.Ok("abc");
            var result2 = Result.Ok("zzz");
            var isEqual = result1 != result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_is_true()
        {
            var result1 = Result.Fail<string>();
            var result2 = Result.Fail<string>();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false()
        {
            var result1 = Result.Fail<string>();
            var result2 = Result.Fail<string>();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = Result.Ok("abc");
            var errorResult = Result.Fail<string>();
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = Result.Ok("abc");
            var errorResult = Result.Fail<string>();
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_true_if_object_is_the_same_as_the_value_in_result()
        {
            var value = "abc";
            var result = Result.Ok(value);
            object someObject = value;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }
        
        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_not_the_same_as_the_value_in_result()
        {
            var result = Result.Ok("abc");
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_true_if_object_is_ok_result_with_same_value()
        {
            var value = "abc";
            var result = Result.Ok(value);
            object someObject = Result.Ok(value);
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_object_is_false_if_object_is_ok_result_with_different_value()
        {
            var result = Result.Ok("abc");
            object someObject = Result.Ok("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_true_if_object_is_fail_result()
        {
            var result = Result.Fail<string>();
            object someObject = Result.Fail<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_result_and_object_is_false_if_object_is_not_fail_result()
        {
            var result = Result.Fail<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_true_if_they_have_the_same_value()
        {
            var value = "abc";
            var result = Result.Ok(value);
            var result2 = Result.Ok(value);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_results_is_false_if_they_do_not_have_the_same_value()
        {
            var result = Result.Ok("abc");
            var result2 = Result.Ok("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_results_is_true()
        {
            var result = Result.Fail<string>();
            var result2 = Result.Fail<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result = Result.Ok("abc");
            var result2 = Result.Fail<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
