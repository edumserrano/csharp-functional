using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimple
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleTests
    {
        [Fact]
        public void Ok_result_IsSuccess_is_true()
        {
            var result = Result.Ok();
            result.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void Ok_result_IsFailure_is_false()
        {
            var result = Result.Ok();
            result.IsFailure.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsSuccess_is_false()
        {
            var result = Result.Fail();
            result.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Fail_result_IsFailure_is_true()
        {
            var result = Result.Fail();
            result.IsFailure.ShouldBeTrue();
        }

        [Fact]
        public void Combine_if_all_results_are_ok_returns_ok_result()
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
        public void Combine_returns_fail_result_if_at_least_one_result_is_a_fail()
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
        public void ToString_returns_result_success_message_when_result_is_ok()
        {
            var result = Result.Ok();
            result.ToString().ShouldBe(ResultMessages.SuccessResult);
        }

        [Fact]
        public void ToString_returns_result_fail_message_when_result_is_fail()
        {
            var result = Result.Fail();
            result.ToString().ShouldBe(ResultMessages.FailureResult);
        }
    }
}
