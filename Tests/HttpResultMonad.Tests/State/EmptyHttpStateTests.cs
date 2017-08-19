using System.IO;
using System.Threading.Tasks;
using HttpResultMonad.State;
using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.State
{
    [Trait("HttpResultMonads", "EmptyHttpStateTests")]
    public class EmptyHttpStateTests
    {
        [Fact]
        public async Task GetRequestBodyAsync_returns_null_stream()
        {
            var emptyHttpState = HttpState.Empty;
            var requestBody = await emptyHttpState.GetRequestBodyAsync();
            requestBody.ShouldBe(Stream.Null);
        }

        [Fact]
        public async Task GetResponseBodyAsync_returns_null_stream()
        {
            var emptyHttpState = HttpState.Empty;
            var requestBody = await emptyHttpState.GetResponseBodyAsync();
            requestBody.ShouldBe(Stream.Null);
        }

        [Fact]
        public void Equals_between_two_EmptyHttpState_is_true()
        {
            var emptyHttpState1 = HttpState.Empty;
            var emptyHttpState2 = HttpState.Empty;

            var isEqual = emptyHttpState1.Equals(emptyHttpState2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_EmptyHttpState_and_object_is_true_if_object_is_EmptyHttpState()
        {
            var emptyHttpState1 = HttpState.Empty;
            object emptyHttpState2 = HttpState.Empty;

            var isEqual = emptyHttpState1.Equals(emptyHttpState2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_EmptyHttpState_and_object_is_false_if_object_is_NotEmptyHttpState()
        {
            var emptyHttpState1 = HttpState.Empty;
            object emptyHttpState2 = Test.CreateHttpStateA();

            var isEqual = emptyHttpState1.Equals(emptyHttpState2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_EmptyHttpState_and_null_is_false()
        {
            var isEqual = HttpState.Empty.Equals(null);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void GetHashCode_between_two_EmptyHttpState_is_true()
        {
            var emptyHttpState1 = HttpState.Empty;
            var emptyHttpState2 = HttpState.Empty;

            emptyHttpState1
                .GetHashCode()
                .ShouldBe(emptyHttpState2.GetHashCode());
        }
    }
}
