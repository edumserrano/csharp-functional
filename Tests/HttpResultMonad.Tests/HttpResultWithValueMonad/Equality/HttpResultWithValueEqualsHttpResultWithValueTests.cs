using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad.Equality
{
    [Trait("Monad", "HttpResultWithValue")]
    public class HttpResultWithValueEqualsHttpResultWithValueTests
    {
        [Fact]
        public void Equals_between_two_ok_HttpResultWithValue_is_true_if_the_values_are_equal()
        {
            var result1 = HttpResult.Ok("abc", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("abc", Test.CreateHttpStateB());
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_HttpResultWithValue_is_false_if_the_value_are_not_equal()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_two_fail_HttpResultWithValue_is_true()
        {
            var result1 = HttpResult.Fail<string>(Test.CreateHttpStateA());
            var result2 = HttpResult.Fail<string>(Test.CreateHttpStateB());
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }
        
        [Fact]
        public void Equals_between_ok_result_and_fail_result_is_false()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Fail<string>();
            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            isEqual1.ShouldBeFalse();
            isEqual2.ShouldBeFalse();
        }
    }
}
