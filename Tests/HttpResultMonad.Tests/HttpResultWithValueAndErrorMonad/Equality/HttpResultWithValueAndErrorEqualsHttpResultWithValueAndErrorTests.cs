using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "HttpResultWithValueAndError")]
    public class HttpResultWithValueAndErrorEqualsHttpResultWithValueAndErrorTests
    {
        [Fact]
        public void Equals_between_two_ok_HttpResultWithValueAndError_is_true_if_they_have_the_value_are_equal()
        {
            var result1 = HttpResult.Ok<string, string>("value");
            var result2 = HttpResult.Ok<string, string>("value");
            var result3 = HttpResult.Ok<string, string>("value", Test.CreateHttpStateA());
            var result4 = HttpResult.Ok<string, string>("value", Test.CreateHttpStateB());
            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_HttpResultWithValueAndError_is_false_if_the_values_are_not_equal()
        {
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Ok<string, string>("zzz");
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_two_fail_HttpResultWithValueAndError_is_true_if_the_error_are_equal()
        {
            var result1 = HttpResult.Fail<string, string>("error", Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string, string>("error", Test.CreateHttpStateB());
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_HttpResultWithValueAndError_is_false_if_the_errors_are_not_equal()
        {
            var result1 = HttpResult.Fail<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("zzz");
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_ok_HttpResultWithValueAndError_and_fail_HttpResultWithValueAndError_is_false()
        {
            var result1 = HttpResult.Ok<string, string>("abc");
            var result2 = HttpResult.Fail<string, string>("abc");
            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            isEqual1.ShouldBeFalse();
            isEqual2.ShouldBeFalse();
        }
    }
}
