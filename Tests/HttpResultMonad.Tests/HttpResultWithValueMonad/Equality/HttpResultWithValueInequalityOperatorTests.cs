using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad.Equality
{
    [Trait("Monad", "HttpResultWithValue")]
    public class HttpResultWithValueInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValue_is_false_if_value_are_equal()
        {
            var result1 = HttpResult.Ok("value", Test.CreateHttpStateA());
            var result2 = HttpResult.Ok("value", Test.CreateHttpStateB());
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }


        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultWithValue_with_different_value_is_true()
        {
            var result1 = HttpResult.Ok("abc");
            var result2 = HttpResult.Ok("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }
        
        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultWithValue_is_false()
        {
            var result1 = HttpResult.Fail<string>();
            var result2 = HttpResult.Fail<string>();
            var result3 = HttpResult.Fail<string>(Test.CreateHttpStateA());
            var result4 = HttpResult.Fail<string>(Test.CreateHttpStateB());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }
        
        [Fact]
        public void Inequality_operator_between_ok_HttpResultWithValue_and_fail_HttpResultWithValue_is_true()
        {
            var okResult = HttpResult.Ok("abc");
            var errorResult = HttpResult.Fail<string>();
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
