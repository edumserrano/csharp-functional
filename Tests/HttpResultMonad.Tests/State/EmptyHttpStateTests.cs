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
        public void EmptyState_is_empty()
        {
            var emptyHttpState = HttpState.Empty;
            emptyHttpState.Url.ShouldBeNull();
            emptyHttpState.HttpMethod.ShouldBeNull();
            emptyHttpState.HttpStatusCode.ShouldBe(0);
            emptyHttpState.RequestContentLength.ShouldBeNull();
            emptyHttpState.ResponseContentLength.ShouldBeNull();
            emptyHttpState.RequestHeaders.ShouldBeNull();
            emptyHttpState.ResponseHeaders.ShouldBeNull();
        }

        [Fact]
        public void Dispose_can_be_called_multiple_times()
        {
            var emptyHttpState = HttpState.Empty;
            emptyHttpState.Dispose();
            emptyHttpState.Dispose();
            emptyHttpState.Dispose();
        }

        [Fact]
        public async Task ReadRequestBodyAsStreamAsync_returns_null_stream()
        {
            var emptyHttpState = HttpState.Empty;
            var requestBody = await emptyHttpState.ReadRequestBodyAsStreamAsync();
            requestBody.ShouldBe(Stream.Null);
        }

        [Fact]
        public async Task ReadResponseBodyAsStreamAsync_returns_null_stream()
        {
            var emptyHttpState = HttpState.Empty;
            var responseBody = await emptyHttpState.ReadResponseBodyAsStreamAsync();
            responseBody.ShouldBe(Stream.Null);
        }

        [Fact]
        public async Task ReadRequestBodyAsStringAsync_returns_empty_string()
        {
            var emptyHttpState = HttpState.Empty;
            var requestBody = await emptyHttpState.ReadRequestBodyAsStringAsync();
            requestBody.ShouldBeEmpty();
        }

        [Fact]
        public async Task ReadResponseBodyAsStringAsync_returns_empty_string()
        {
            var emptyHttpState = HttpState.Empty;
            var responseBody = await emptyHttpState.ReadResponseBodyAsStringAsync();
            responseBody.ShouldBeEmpty();
        }

        [Fact]
        public async Task ReadRequestBodyAsByteArrayAsync_returns_null_stream()
        {
            var emptyHttpState = HttpState.Empty;
            var requestBody = await emptyHttpState.ReadRequestBodyAsByteArrayAsync();
            requestBody.ShouldBeEmpty();
        }

        [Fact]
        public async Task ReadResponseBodyAsByteArrayAsync_returns_null_stream()
        {
            var emptyHttpState = HttpState.Empty;
            var responseBody = await emptyHttpState.ReadResponseBodyAsByteArrayAsync();
            responseBody.ShouldBeEmpty();
        }


        [Fact]
        public void Equals_between_two_EmptyHttpState_that_are_the_same_reference_is_true()
        {
            var emptyHttpState1 = HttpState.Empty;
            var emptyHttpState2 = emptyHttpState1;

            var isEqual = emptyHttpState1.Equals(emptyHttpState2);
            isEqual.ShouldBeTrue();
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
