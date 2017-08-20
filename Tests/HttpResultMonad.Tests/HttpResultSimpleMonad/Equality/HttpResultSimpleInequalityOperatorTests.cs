using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "HttpResultSimple")]
    public class HttpResultSimpleInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultSimple_is_false_if_HttpState_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(httpState);
            var result4 = HttpResult.Ok(httpState);

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultSimple_is_true_if_HttpState_are_not_equal()
        {
            var httpState1 = Test.CreateHttpStateA();
            var httpState2 = Test.CreateHttpStateB();
            var result1 = HttpResult.Ok(httpState1);
            var result2 = HttpResult.Ok(httpState2);
            
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_HttpResultSimple_is_true_if_HttpState_is_not_equal()
        {
            var httpState1 = Test.CreateHttpStateA();
            var httpState2 = Test.CreateHttpStateB();
            var result1 = HttpResult.Ok(httpState1);
            var result2 = HttpResult.Ok(httpState2);

            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_HttpResultSimple_is_false_if_HttpState_are_equal()
        {
            var httpState = Test.CreateHttpStateA();
            var result1 = HttpResult.Fail();
            var result2 = HttpResult.Fail();
            var result3 = HttpResult.Fail(httpState);
            var result4 = HttpResult.Fail(httpState);

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_HttpResultSimple_and_fail_HttpResultSimple_is_true()
        {
            var okResult = HttpResult.Ok();
            var failResult = HttpResult.Fail();
            var isEqual = okResult != failResult;
            isEqual.ShouldBeTrue();
        }
    }
}
