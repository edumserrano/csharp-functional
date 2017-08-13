using System;
using System.Diagnostics;
using HttpResultMonad.State;
using MaybeMonad;

namespace HttpResultMonad
{
    /*
    The purpose of this struct is just for syntax usage of the HttpResultWithError struct.
    It allow the following syntax: HttpResultWithError.Ok<int> instead of HttpResultWithError<int>.Ok
    It thus allows keeping the same syntax between extension and non-extension methods.
    */
    public struct HttpResultWithError
    {
        [DebuggerStepThrough]
        public static HttpResultWithError<T> Ok<T>()
        {
            return Ok<T>(Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResultWithError<T> Ok<T>(Maybe<HttpState> httpState)
        {
            return new HttpResultWithError<T>(HttpResultStatus.Ok, Maybe<T>.Nothing, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResultWithError<T> Fail<T>(T error)
        {
            return Fail(error, Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResultWithError<T> Fail<T>(T error, Maybe<HttpState> httpState)
        {
            return new HttpResultWithError<T>(HttpResultStatus.Fail, error, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResultWithError<TError> From<TError>(Func<bool> predicate, TError error)
        {
            return predicate()
                ? Ok<TError>()
                : Fail(error);
        }
    }

    public struct HttpResultWithError<T> : IEquatable<HttpResultWithError<T>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<T> _error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HttpResultStatus _httpResultStatus;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<HttpState> _httpState;

        [DebuggerStepThrough]
        internal HttpResultWithError(HttpResultStatus httpResultStatus, Maybe<T> error, Maybe<HttpState> httpState)
        {
            if (httpResultStatus == HttpResultStatus.Fail && error.HasNoValue)
            {
                throw new ArgumentNullException(nameof(error), HttpResultWithErrorMessages.FailureResultMustHaveError);
            }

            _httpResultStatus = httpResultStatus;
            _httpState = httpState;
            _error = httpResultStatus == HttpResultStatus.Fail ? error : Maybe<T>.Nothing;
        }

        public bool IsFailure
        {
            [DebuggerStepThrough]
            get
            {
                return _httpResultStatus == HttpResultStatus.Fail;
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
                    throw new InvalidOperationException(HttpResultWithErrorMessages.NoErrorForSuccess);
                }

                return _error.Value;
            }
        }

        public Maybe<HttpState> HttpState
        {
            [DebuggerStepThrough]
            get
            {
                return _httpState;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(HttpResultWithError<T> first, HttpResultWithError<T> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(HttpResultWithError<T> first, HttpResultWithError<T> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (obj is HttpResultWithError<T> other)
            {
                return Equals(other);
            }

            return false;
        }

        [DebuggerStepThrough]
        public bool Equals(HttpResultWithError<T> other)
        {
            if (!HttpState.Equals(other.HttpState))
            {
                return false;
            }

            if (IsFailure && other.IsFailure)
            {
                return Error.Equals(other.Error);
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
            unchecked
            {
                var hashCode = _error.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)_httpResultStatus;
                hashCode = (hashCode * 397) ^ _httpState.GetHashCode();
                hashCode = (hashCode * 397) ^ typeof(T).GetHashCode();
                return hashCode;
            }
        }


        #endregion

        [DebuggerStepThrough]
        public override string ToString()
        {
            return IsSuccess
                ? HttpResultWithErrorMessages.GetSuccessResultWithErrorToStringMessage(typeof(T))
                : Error.ToString();
        }
    }
}
