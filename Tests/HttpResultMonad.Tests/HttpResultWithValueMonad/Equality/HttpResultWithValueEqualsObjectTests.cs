using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithValueMonad.Equality
{
    [Trait("Monad", "HttpResultWithValue")]
    public class HttpResultWithValueEqualsObjectTests
    {
        [Fact]
        public void Equals_between_ok_HttpResultWithValue_and_object_is_false_if_object_is_not_an_HttpResultWithValue()
        {
            var value = "abc";
            var result = HttpResult.Ok(value);
            object someObject = value;
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithValue_and_object_is_true_if_object_is_ok_HttpResultWithValue_with_equal_value()
        {
            var value = "abc";
            var result1 = HttpResult.Ok(value);
            object someObject1 = HttpResult.Ok(value);
            var result2 = HttpResult.Ok(value, Test.CreateHttpStateA());
            object someObject2 = HttpResult.Ok(value, Test.CreateHttpStateB());
            var isEqual1 = result1.Equals(someObject1);
            var isEqual2 = result2.Equals(someObject2);
            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_HttpResultWithValue_and_object_is_false_if_object_is_ok_HttpResultWithValue_with_different_value()
        {
            var result = HttpResult.Ok("abc");
            object someObject = HttpResult.Ok("zzz");
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }
        
        [Fact]
        public void Equals_between_fail_HttpResultWithValue_and_object_is_false_if_object_is_not_an_HttpResultWithValue()
        {
            var result = HttpResult.Fail<string>();
            object someObject = "zzz";
            var isEqual = result.Equals(someObject);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_fail_HttpResultWithValue_and_object_is_true_if_object_is_fail_HttpResultWithValue()
        {
            var result1 = HttpResult.Fail<string>();
            object someObject1 = HttpResult.Fail<string>();
            var result2 = HttpResult.Fail<string>(Test.CreateHttpStateA());
            object someObject2 = HttpResult.Fail<string>(Test.CreateHttpStateB());

            var isEqual1 = result1.Equals(someObject1);
            var isEqual2 = result2.Equals(someObject2);

            isEqual1.ShouldBeTrue();
            isEqual2.ShouldBeTrue();
        }
    }
}
