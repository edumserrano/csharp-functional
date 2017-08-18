using System;
using System.Diagnostics;
using System.Linq;
using MaybeMonad;
using ResultMonad.Extensions.ResultWithValueAndErrorMonad.Map;

namespace ResultMonad
{
    public struct Result : IEquatable<Result>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultStatus _resultStatus;

        [DebuggerStepThrough]
        private Result(ResultStatus resultStatus)
        {
            _resultStatus = resultStatus;
        }

        public bool IsFailure
        {
            [DebuggerStepThrough]
            get
            {
                return _resultStatus == ResultStatus.Fail;
            }
        }

        public bool IsSuccess
        {
            [DebuggerStepThrough]
            get
            {
                return !IsFailure;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(Result first, Result second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(Result first, Result second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (obj is Result other)
            {
                return Equals(other);
            }

            return false;
        }

        [DebuggerStepThrough]
        public bool Equals(Result other)
        {
            if (IsFailure && other.IsFailure)
            {
                return true;
            }

            if (IsSuccess && other.IsSuccess)
            {
                return true;
            }

            return false;
        }
        
        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            return (int)_resultStatus;
        }

        #endregion
        
        [DebuggerStepThrough]
        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        [DebuggerStepThrough]
        public static Result Fail()
        {
            return new Result(ResultStatus.Fail);
        }

        [DebuggerStepThrough]
        public static Result<TValue> Ok<TValue>(TValue value)
        {
            return new Result<TValue>(ResultStatus.Ok, value);
        }

        [DebuggerStepThrough]
        public static Result<TValue> Fail<TValue>()
        {
            return new Result<TValue>(ResultStatus.Fail, Maybe<TValue>.Nothing);
        }

        [DebuggerStepThrough]
        public static Result<TValue, TError> Ok<TValue, TError>(TValue value)
        {
            return new Result<TValue, TError>(ResultStatus.Ok, value, Maybe<TError>.Nothing);
        }

        [DebuggerStepThrough]
        public static Result<TValue, TError> Fail<TValue, TError>(TError error)
        {
            return new Result<TValue, TError>(ResultStatus.Fail, Maybe<TValue>.Nothing, error);
        }
        
        [DebuggerStepThrough]
        public static Result<TValue> From<TValue>(Func<bool> predicate, TValue value)
        {
            return predicate()
                ? Ok(value)
                : Fail<TValue>();
        }

        [DebuggerStepThrough]
        public static Result<TValue, TError> From<TValue, TError>(Func<bool> predicate, TValue value, TError error)
        {
            return predicate()
                ? Ok<TValue, TError>(value)
                : Fail<TValue, TError>(error);
        }

        [DebuggerStepThrough]
        public static Result Combine(params Result[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? Result.Ok()
                : Result.Fail();
        }

        [DebuggerStepThrough]
        public static Result Combine<T>(params Result<T>[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? Result.Ok()
                : Result.Fail();
        }


        [DebuggerStepThrough]
        public static ResultWithError<TError> Combine<TError>(params ResultWithError<TError>[] resultsWithError)
        {
            var anyFailure = resultsWithError.Any(x => x.IsFailure);
            return !anyFailure
                ? ResultWithError.Ok<TError>()
                : resultsWithError.First(x => x.IsFailure);
        }

        [DebuggerStepThrough]
        public static ResultWithError<TError> Combine<TValue, TError>(params Result<TValue, TError>[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? ResultWithError.Ok<TError>()
                : results.First(x => x.IsFailure).ToResultWithError();
        }
    }
}
