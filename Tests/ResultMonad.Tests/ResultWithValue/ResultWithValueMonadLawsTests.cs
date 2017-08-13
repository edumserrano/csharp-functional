using System;
using ResultMonad.Extensions.ResultWithValue.OnSuccess;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValue
{
    [Trait("Monad", "Result")]
    public class ResultWithValueMonadLawsTests
    {
        private readonly Func<int, Result<int>> _plusOneFunc = i => Result.Ok(i + 1);
        private readonly Func<int, Result<int>> _plusTwoFunc = i => Result.Ok(i + 2);
        private readonly Func<int, Result<int>> _unitFunc = i => Result.Ok(i);
        private readonly Result<int> _unit = Result.Ok(5);
        private readonly Result<int> _aMonad = Result.Ok(10);

        [Fact]
        public void LeftIdentity()
        {
            // unit(a) bind f MustBe f(a)
            // ie.
            // Applying a function to the result of the construction function on a value, 
            // and applying that function to the value directly, 
            // produces two logically identical instances of the monad.

            var first = _unit.OnSuccessToResultWithValue(_plusOneFunc);
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

            var first = _aMonad.OnSuccessToResultWithValue(_unitFunc);
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
                .OnSuccessToResultWithValue(_plusOneFunc)
                .OnSuccessToResultWithValue(_plusTwoFunc);

            var right = _aMonad
                .OnSuccessToResultWithValue(x => _plusOneFunc(x).OnSuccessToResultWithValue(_plusTwoFunc));

            left.ShouldBe(right);
        }

    }
}
