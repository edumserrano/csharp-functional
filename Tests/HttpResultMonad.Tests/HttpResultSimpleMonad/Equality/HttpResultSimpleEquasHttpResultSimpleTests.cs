using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "HttpResultSimple")]
    public class HttpResultSimpleEquasHttpResultSimpleTests
    {
        [Fact]
        public void Equals_between_two_ok_HttpResultSimple_is_true()
        {
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(Test.CreateHttpStateA());
            var result4 = HttpResult.Ok(Test.CreateHttpStateB());

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            var isEqual3 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
            isEqual3.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_HttpResultSimple_is_true()
        {
            var result1 = HttpResult.Fail();
            var result2 = HttpResult.Fail();
            var result3 = HttpResult.Fail(Test.CreateHttpStateA());
            var result4 = HttpResult.Fail(Test.CreateHttpStateB());

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            var isEqual3 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
            isEqual3.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_HttpResultSimple_and_fail_HttpResultSimple_is_false()
        {
            var result1 = HttpResult.Ok();
            var result2 = HttpResult.Fail();
            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            isEqual1.ShouldBeFalse();
            isEqual2.ShouldBeFalse();
        }
    }
}
