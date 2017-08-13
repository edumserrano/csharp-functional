using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimpleMonad
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleTests
    {
        [Fact]
        public void Ok_ResultSimple_IsSuccess_is_true()
        {
            var result = Result.Ok();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_ResultSimple_IsFailure_is_false()
        {
            var result = Result.Ok();
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_ResultSimple_IsSuccess_is_false()
        {
            var result = Result.Fail();
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_ResultSimple_IsFailure_is_true()
        {
            var result = Result.Fail();
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Combine_if_all_ResultSimple_are_ok_returns_ok_ResultSimple()
        {
            var resultsLists = new List<Result>
            {
                Result.Ok(),
                Result.Ok(),
                Result.Ok()
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Combine_returns_fail_ResultSimple_if_at_least_one_ResultSimple_is_a_fail()
        {
            var resultsLists = new List<Result>
            {
                Result.Ok(),
                Result.Fail(),
                Result.Ok()
            };

            var combinedResult = Result.Combine(resultsLists.ToArray());
            combinedResult.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void ToString_returns_ResultSimple_success_message_when_ResultSimple_is_ok()
        {
            var result = Result.Ok();
            result.ToString().ShouldBe(ResultMessages.SuccessResult);
        }

        [Fact]
        public void ToString_returns_ResultSimple_fail_message_when_ResultSimple_is_fail()
        {
            var result = Result.Fail();
            result.ToString().ShouldBe(ResultMessages.FailureResult);
        }
    }
}
