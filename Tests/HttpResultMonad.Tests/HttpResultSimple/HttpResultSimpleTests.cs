using System.Collections.Generic;
using HttpResultMonad.State;
using MaybeMonad;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultSimple
{
    public class HttpResultSimpleTests
    {
        [Fact]
        public void Ok_HttpResult_IsSuccess_is_true()
        {
            var httpResult = HttpResult.Ok();
            httpResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_HttpResult_IsFailure_is_false()
        {
            var httpResult = HttpResult.Ok();
            httpResult.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResult_IsSuccess_is_false()
        {
            var httpResult = HttpResult.Fail();
            httpResult.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_HttpResult_IsFailure_is_true()
        {
            var httpResult = HttpResult.Fail();
            httpResult.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void OK_HttpResult_withouth_passing_in_http_state_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Ok();
            httpResult.HttpState.ShouldBe(Maybe<HttpState>.Nothing);
        }


        [Fact]
        public void Fail_HttpResult_withouth_passing_in_http_state_has_maybe_nothing_for_that_field()
        {
            var httpResult = HttpResult.Fail();
            httpResult.HttpState.ShouldBe(Maybe<HttpState>.Nothing);
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
