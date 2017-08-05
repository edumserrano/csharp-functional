using CSharp.Functional.HttpResultMonad;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.HttpResultMonad.HttpResultWithError
{
    public class HttpResultWithErrorEqualityTests
    {
        [Fact]
        public void Equality_operator_between_two_fail_httpResultErrors_is_true_if_the_http_state_and_the_error_are_the_same()
        {
            var error = "abc";
            var result1 = HttpResultError.Fail(error);
            var result2 = HttpResultError.Fail(error);
            var result3 = HttpResultError.Fail(error, Test.CreateHttpStateA());
            var result4 = HttpResultError.Fail(error, Test.CreateHttpStateA());

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_httpResultErrors_is_false_if_the_http_state_and_the_error_are_the_same()
        {
            var error = "abc";
            var result1 = HttpResultError.Fail(error);
            var result2 = HttpResultError.Fail(error);
            var result3 = HttpResultError.Fail(error, Test.CreateHttpStateA());
            var result4 = HttpResultError.Fail(error, Test.CreateHttpStateA());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_httpResultErrors_with_different_error_is_false()
        {
            var result1 = HttpResultError.Fail("abc");
            var result2 = HttpResultError.Fail("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_httpResultErrors_with_different_http_state_is_false()
        {
            var result1 = HttpResultError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_httpResultErrors_with_different_error_is_true()
        {
            var result1 = HttpResultError.Fail("abc");
            var result2 = HttpResultError.Fail("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }


        [Fact]
        public void Inequality_operator_between_two_fail_httpResultErrors_with_different_http_state_is_true()
        {
            var result1 = HttpResultError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultError.Fail("abc", Test.CreateHttpStateB());
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_httpResultErrors_is_true_if_http_state_are_the_same()
        {
            var result1 = HttpResultError.Ok<string>();
            var result2 = HttpResultError.Ok<string>();
            var result3 = HttpResultError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultError.Ok<string>(Test.CreateHttpStateA());

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_httpResultErrors_is_false_if_http_state_are_the_same()
        {
            var result1 = HttpResultError.Ok<string>();
            var result2 = HttpResultError.Ok<string>();
            var result3 = HttpResultError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultError.Ok<string>(Test.CreateHttpStateA());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_httpResultError_and_fail_httpResultError_is_false()
        {
            var okResult = HttpResultError.Ok<string>();
            var errorResult = HttpResultError.Fail("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_httpResultError_and_fail_httpResultError_is_true()
        {
            var okResult = HttpResultError.Ok<string>();
            var errorResult = HttpResultError.Fail("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_httpResultError_and_object_is_false_if_object_is_not_an_httpResultError()
        {
            var result = HttpResultError.Fail("abc");
            object someObject = "abc";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_httpResultError_and_object_is_true_if_object_is_fail_httpResultError_with_same_error_and_http_state()
        {
            var result = HttpResultError.Fail("error", Test.CreateHttpStateA());
            object someObject = HttpResultError.Fail("error", Test.CreateHttpStateA());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_httpResultError_and_object_is_false_if_object_is_fail_httpResultError_with_different_error()
        {
            var result = HttpResultError.Fail("abc");
            object someObject = HttpResultError.Fail("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_httpResultError_and_object_is_false_if_object_is_fail_httpResultError_with_different_http_state()
        {
            var result = HttpResultError.Fail("abc", Test.CreateHttpStateA());
            object someObject = HttpResultError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_ok_httpResultError_and_object_is_true_if_object_is_ok_result()
        {
            var result = HttpResultError.Ok<string>();
            object someObject = HttpResultError.Ok<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_httpResultError_and_object_is_false_if_object_is_not_ok_result()
        {
            var result = HttpResultError.Ok<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultErrors_is_true_if_they_have_the_same_error_and_http_state()
        {
            var result = HttpResultError.Fail("error", Test.CreateHttpStateA());
            var result2 = HttpResultError.Fail("error", Test.CreateHttpStateA());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultErrors_is_false_if_they_do_not_have_the_same_error()
        {
            var result = HttpResultError.Fail("abc");
            var result2 = HttpResultError.Fail("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultErrors_is_false_if_they_do_not_have_the_same_http_state()
        {
            var result = HttpResultError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_two_ok_httpResultErrors_is_true_if_they_have_the_same_status_code_and_raw_body()
        {
            var result = HttpResultError.Ok<string>();
            var result2 = HttpResultError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_httpResultErrors_is_false_if_they_have_different_http_state()
        {
            var result = HttpResultError.Ok<string>(Test.CreateHttpStateA());
            var result2 = HttpResultError.Ok<string>(Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_ok_httpResultError_and_fail_httpResultError_is_false()
        {
            var result = HttpResultError.Fail("abc");
            var result2 = HttpResultError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
