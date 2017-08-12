using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithError
{
    public class HttpResultWithErrorEqualityTests
    {
        [Fact]
        public void Equality_operator_between_two_fail_httpResultWithErrors_is_true_if_the_http_state_and_the_error_are_the_same()
        {
            var error = "abc";
            var result1 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result2 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result3 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_httpResultWithErrors_is_false_if_the_http_state_and_the_error_are_the_same()
        {
            var error = "abc";
            var result1 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result2 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result3 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_httpResultWithErrors_with_different_error_is_false()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_httpResultWithErrors_with_different_http_state_is_false()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_httpResultWithErrors_with_different_error_is_true()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }


        [Fact]
        public void Inequality_operator_between_two_fail_httpResultWithErrors_with_different_http_state_is_true()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_httpResultWithErrors_is_true_if_http_state_are_the_same()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result3 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());

            var isEqual1 = result1 == result2;
            var isEqual2 = result3 == result4;

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_httpResultWithErrors_is_false_if_http_state_are_the_same()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result3 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_httpResultWithError_and_fail_httpResultWithError_is_false()
        {
            var okResult = HttpResultMonad.HttpResultWithError.Ok<string>();
            var errorResult = HttpResultMonad.HttpResultWithError.Fail("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_httpResultWithError_and_fail_httpResultWithError_is_true()
        {
            var okResult = HttpResultMonad.HttpResultWithError.Ok<string>();
            var errorResult = HttpResultMonad.HttpResultWithError.Fail("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_false_if_object_is_not_an_httpResultWithError()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            object someObject = "abc";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_true_if_object_is_fail_httpResultWithError_with_same_error_and_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            object someObject = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_false_if_object_is_fail_httpResultWithError_with_different_error()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            object someObject = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_httpResultWithError_and_object_is_false_if_object_is_fail_httpResultWithError_with_different_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            object someObject = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_ok_httpResultWithError_and_object_is_true_if_object_is_ok_result()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>();
            object someObject = HttpResultMonad.HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_httpResultWithError_and_object_is_false_if_object_is_not_ok_result()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultWithErrors_is_true_if_they_have_the_same_error_and_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("error", Test.CreateHttpStateA());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultWithErrors_is_false_if_they_do_not_have_the_same_error()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_httpResultWithErrors_is_false_if_they_do_not_have_the_same_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_two_ok_httpResultWithErrors_is_true_if_they_have_the_same_status_code_and_raw_body()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_httpResultWithErrors_is_false_if_they_have_different_http_state()
        {
            var result = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateB());
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_ok_httpResultWithError_and_fail_httpResultWithError_is_false()
        {
            var result = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
