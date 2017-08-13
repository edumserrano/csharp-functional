using System;
using ResultMonad.Extensions.ResultWithValueAndError.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndError
{
    [Trait("Monad", "ResultSimple")]
    public class ResultWithValueAndErrorMonadLawsTests
    {
        private readonly Func<int, Result<int, string>> _plusOneFunc = i => Result.Ok<int, string>(i + 1);
        private readonly Func<int, Result<int, string>> _plusTwoFunc = i => Result.Ok<int, string>(i + 2);
        private readonly Func<int, Result<int, string>> _unitFunc = i => Result.Ok<int, string>(i);
        private readonly Result<int, string> _unit = Result.Ok<int, string>(5);
        private readonly Result<int, string> _aMonad = Result.Ok<int, string>(10);

        [Fact]
        public void LeftIdentity()
        {
            // unit(a) bind f MustBe f(a)
            // ie.
            // Applying a function to the result of the construction function on a value, 
            // and applying that function to the value directly, 
            // produces two logically identical instances of the monad.

            var first = _unit.OnSuccessToResultWithValueAndError(_plusOneFunc);
            var second = _plusOneFunc(_unit.Value);
            first.ShouldBe(second);
        }

        [Fact]
        public void RightIdentity()
        {
            // m bind unit MustBe m
            // ie.
            // Applying the construction function to a given instance of the monad 
            // produces a logically identical instance of the monad.

            var first = _aMonad.OnSuccessToResultWithValueAndError(_unitFunc);
            var second = _aMonad;
            first.ShouldBe(second);
        }

        [Fact]
        public void Associativity()
        {
            // (m bind f) bind g MustBe m bind (f(x) bind g)
            // ie.
            // Applying to a value a first function followed by applying to the result a second function, 
            // and applying to the original value a third function that is the composition of the first and second functions, 
            // produces two logically identical instances of the monad.

            var left = _aMonad
                .OnSuccessToResultWithValueAndError(_plusOneFunc)
                .OnSuccessToResultWithValueAndError(_plusTwoFunc);

            var right = _aMonad
                .OnSuccessToResultWithValueAndError(x => _plusOneFunc(x).OnSuccessToResultWithValueAndError(_plusTwoFunc));

            left.ShouldBe(right);
        }

    }
}
