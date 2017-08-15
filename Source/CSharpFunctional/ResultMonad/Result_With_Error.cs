using System;
using System.Diagnostics;
using MaybeMonad;

namespace ResultMonad
{

    /*
    The purpose of this struct is just for syntax usage of the ResultWithError struct.
    It allow the following syntax: ResultWithError.Ok<int> instead of ResultWithError<int>.Ok
    It thus allows keeping the same syntax between extension and non-extension methods.
    */
    public struct ResultWithError
    {
        [DebuggerStepThrough]
        public static ResultWithError<T> Ok<T>()
        {
            return new ResultWithError<T>(ResultStatus.Ok, Maybe<T>.Nothing);
        }

        [DebuggerStepThrough]
        public static ResultWithError<T> Fail<T>(T error)
        {
            return new ResultWithError<T>(ResultStatus.Fail, error);
        }

        [DebuggerStepThrough]
        public static ResultWithError<TError> From<TError>(Func<bool> predicate, TError error)
        {
            return predicate()
                ? Ok<TError>()
                : Fail(error);
        }
    }

    public struct ResultWithError<T> : IEquatable<ResultWithError<T>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<T> _error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultStatus _resultStatus;

        [DebuggerStepThrough]
        internal ResultWithError(ResultStatus status, Maybe<T> error)
        {
            if (status == ResultStatus.Fail && error.HasNoValue)
            {
                throw new ArgumentNullException(nameof(error), ResultMessages.FailureResultMustHaveError);
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
                    throw new InvalidOperationException(ResultMessages.NoErrorForSuccess);
                }

                return _error.Value;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(ResultWithError<T> resultWithError, T error)
        {
            if (resultWithError.IsSuccess)
            {
                return false;
            }

            return resultWithError.Error.Equals(error);
        }

        [DebuggerStepThrough]
        public static bool operator !=(ResultWithError<T> resultWithError, T error)
        {
            return !(resultWithError == error);
        }

        [DebuggerStepThrough]
        public static bool operator ==(ResultWithError<T> first, ResultWithError<T> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(ResultWithError<T> first, ResultWithError<T> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            ResultWithError<T> other;

            if (obj is T error)
            {
                other = new ResultWithError<T>(ResultStatus.Fail, error);
            }
            else if (obj is ResultWithError<T> result)
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
        public bool Equals(ResultWithError<T> other)
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
                var hashCode = _error.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)_resultStatus;
                hashCode = (hashCode * 397) ^ typeof(T).GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
