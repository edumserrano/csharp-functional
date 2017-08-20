using System.Collections.Generic;
using HttpResultMonad.State;
using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimpleMonad
{
    [Trait("Monad", "HttpResultSimple")]
    public class HttpResultSimpleTests
    {
        [Fact]
        public void Ok_HttpResultSimple_IsSuccess_is_true()
        {
            var httpResult = HttpResult.Ok();
            httpResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_HttpResultSimple_IsFailure_is_false()
        {
            var httpResult = HttpResult.Ok();
            httpResult.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResultSimple_IsSuccess_is_false()
        {
            var httpResult = HttpResult.Fail();
            httpResult.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResultSimple_IsFailure_is_true()
        {
            var httpResult = HttpResult.Fail();
            httpResult.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void OK_HttpResultSimple_withouth_passing_in_HttpState_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Ok();
            httpResult.HttpState.ShouldBe(HttpState.Empty);
        }


        [Fact]
        public void Fail_HttpResultSimple_withouth_passing_in_HttpState_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Fail();
            httpResult.HttpState.ShouldBe(HttpState.Empty);
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
        {
            var resultsLists = new List<HttpResult>
            {
                HttpResult.Ok(),
                HttpResult.Ok(),
                HttpResult.Ok()
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_first_fail_result_if_at_least_one_result_is_a_fail()
        {
            var firstHttpState = Test.CreateHttpStateA();
            var secondHttpState = Test.CreateHttpStateB();
            var firstFailure = HttpResult.Fail(firstHttpState);
            var resultsLists = new List<HttpResult>
            {
                HttpResult.Ok(),
                firstFailure,
                HttpResult.Ok(),
                HttpResult.Fail(secondHttpState)
            };

            var combinedResult = HttpResult.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
            combinedResult.HttpState.ShouldBe(firstFailure.HttpState);
        }
    }
}
