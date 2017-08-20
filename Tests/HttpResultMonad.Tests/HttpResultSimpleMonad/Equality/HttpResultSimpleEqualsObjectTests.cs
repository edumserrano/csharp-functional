using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad.Equality
{
    [Trait("Monad", "HttpResultSimple")]
    public class HttpResultSimpleEqualsObjectTests
    {
        [Fact]
        public void Equals_between_ok_HttpResultSimple_and_object_is_true_if_object_is_ok_HttpResultSimple()
        {
            var result1 = HttpResult.Ok();
            object result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(Test.CreateHttpStateA());
            object result4 = HttpResult.Ok(Test.CreateHttpStateB());

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }


        [Fact]
        public void Equals_between_fail_HttpResultSimple_and_object_is_true_if_object_is_fail_HttpResultSimple()
        {
            var result1 = HttpResult.Ok();
            object result2 = HttpResult.Ok();
            var result3 = HttpResult.Ok(Test.CreateHttpStateA());
            object result4 = HttpResult.Ok(Test.CreateHttpStateB());

            var isEqual1 = result1.Equals(result2);
            var isEqual2 = result3.Equals(result4);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_HttpResultSimple_and_object_is_false_if_object_is_not_ok_HttpResultSimple()
        {
            var result1 = HttpResult.Ok();
            object result2 = "abc";
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultSimple_and_object_is_false_if_object_is_not_fail_HttpResultSimple()
        {
            var result1 = HttpResult.Fail();
            object result2 = "abc";
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}
