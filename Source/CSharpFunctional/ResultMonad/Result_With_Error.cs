using System;
using System.Diagnostics;
using MaybeMonad;

namespace ResultMonad
{

    /*
    The purpose of this struct is just for syntax usage of the ResultError struct.
    It allow the following syntax: ResultError.Ok<int> instead of ResultError<int>.Ok
    */
    public struct ResultError
    {
        [DebuggerStepThrough]
        public static ResultError<T> Ok<T>()
        {
            return new ResultError<T>(ResultStatus.Ok, Maybe<T>.Nothing);
        }

        [DebuggerStepThrough]
        public static ResultError<T> Fail<T>(T error)
        {
            return new ResultError<T>(ResultStatus.Fail, error);
        }

        [DebuggerStepThrough]
        public static ResultError<TError> From<TError>(Func<bool> predicate, TError error)
        {
            return predicate()
                ? Ok<TError>()
                : Fail(error);
        }
    }

    public struct ResultError<T> : IEquatable<ResultError<T>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<T> _error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultStatus _resultStatus;

        [DebuggerStepThrough]
        internal ResultError(ResultStatus status, Maybe<T> error)
        {
            if (status == ResultStatus.Fail && error.HasNoValue)
            {
                throw new ArgumentNullException(nameof(error), ResultErrorMessages.FailureResultMustHaveError);
            }

            _resultStatus = status;
            _error = status == ResultStatus.Fail ? error : Maybe<T>.Nothing;
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

        public T Error
        {
            [DebuggerStepThrough]
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException(ResultErrorMessages.NoErrorForSuccess);
                }

                return _error.Value;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(ResultError<T> resultError, T error)
        {
            if (resultError.IsSuccess)
            {
                return false;
            }

            return resultError.Error.Equals(error);
        }

        [DebuggerStepThrough]
        public static bool operator !=(ResultError<T> resultError, T error)
        {
            return !(resultError == error);
        }

        [DebuggerStepThrough]
        public static bool operator ==(ResultError<T> first, ResultError<T> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(ResultError<T> first, ResultError<T> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            ResultError<T> other;

            if (obj is T error)
            {
                other = new ResultError<T>(ResultStatus.Fail, error);
            }
            else if (obj is ResultError<T> result)
            {
                other = result;
            }
            else
            {
                return false;
            }

            return Equals(other);
        }

        [DebuggerStepThrough]
        public bool Equals(ResultError<T> other)
        {
            if (IsSuccess && other.IsSuccess)
            {
                return true;
            }

            if (IsFailure && other.IsFailure)
            {
                return Error.Equals(other.Error);
            }

            return false;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            unchecked
            {
                return (_error.GetHashCode() * 397) ^ (int)_resultStatus;
            }
        }

        #endregion

        [DebuggerStepThrough]
        public override string ToString()
        {
            return IsSuccess
                ? ResultErrorMessages.GetSuccessResultErrorToStringMessage(typeof(T))
                : Error.ToString();
        }
    }
}
