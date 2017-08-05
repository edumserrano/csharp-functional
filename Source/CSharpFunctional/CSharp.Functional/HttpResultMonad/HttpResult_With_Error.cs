using System;
using System.Diagnostics;
using CSharp.Functional.HttpResultMonad.State;
using CSharp.Functional.MaybeMonad;

namespace CSharp.Functional.HttpResultMonad
{
    /*
    The purpose of this struct is just for syntax usage of the HttpResultError struct.
    It allow the following syntax: HttpResultError.Ok<int> instead of HttpResultError<int>.Ok
    */
    public struct HttpResultError
    {
        [DebuggerStepThrough]
        public static HttpResultError<T> Ok<T>()
        {
            return Ok<T>(Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResultError<T> Ok<T>(Maybe<HttpState> httpState)
        {
            return new HttpResultError<T>(HttpResultStatus.Ok, Maybe<T>.Nothing, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResultError<T> Fail<T>(T error)
        {
            return Fail(error, Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResultError<T> Fail<T>(T error, Maybe<HttpState> httpState)
        {
            return new HttpResultError<T>(HttpResultStatus.Fail, error, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResultError<TError> From<TError>(Func<bool> predicate, TError error)
        {
            return predicate()
                ? Ok<TError>()
                : Fail(error);
        }
    }

    public struct HttpResultError<T> : IEquatable<HttpResultError<T>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<T> _error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HttpResultStatus _httpResultStatus;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<HttpState> _httpState;

        [DebuggerStepThrough]
        internal HttpResultError(HttpResultStatus httpResultStatus, Maybe<T> error, Maybe<HttpState> httpState)
        {
            if (httpResultStatus == HttpResultStatus.Fail && error.HasNoValue)
            {
                throw new ArgumentNullException(nameof(error), HttpResultErrorMessages.FailureResultMustHaveError);
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
                    throw new InvalidOperationException(HttpResultErrorMessages.NoErrorForSuccess);
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
        public static bool operator ==(HttpResultError<T> first, HttpResultError<T> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(HttpResultError<T> first, HttpResultError<T> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (obj is HttpResultError<T> other)
            {
                return Equals(other);
            }

            return false;
        }

        [DebuggerStepThrough]
        public bool Equals(HttpResultError<T> other)
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
                return hashCode;
            }
        }


        #endregion

        [DebuggerStepThrough]
        public override string ToString()
        {
            return IsSuccess
                ? HttpResultErrorMessages.GetSuccessResultErrorToStringMessage(typeof(T))
                : Error.ToString();
        }
    }
}
