using System;
using System.Diagnostics;
using System.Linq;
using HttpResultMonad.Extensions.HttpResultWithValue.Map;
using HttpResultMonad.Extensions.HttpResultWithValueAndError.Map;
using HttpResultMonad.State;
using MaybeMonad;

namespace HttpResultMonad
{
    public struct HttpResult : IEquatable<HttpResult>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HttpResultStatus _httpResultStatus;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HttpState _httpState;

        [DebuggerStepThrough]
        private HttpResult(HttpResultStatus httpResultStatus, HttpState httpState)
        {
            _httpResultStatus = httpResultStatus;
            _httpState = httpState;
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

        public HttpState HttpState
        {
            [DebuggerStepThrough]
            get
            {
                return _httpState;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(HttpResult first, HttpResult second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(HttpResult first, HttpResult second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (obj is HttpResult other)
            {
                return Equals(other);
            }

            return false;
        }

        [DebuggerStepThrough]
        public bool Equals(HttpResult other)
        {
            if (!HttpState.Equals(other.HttpState))
            {
                return false;
            }

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
            unchecked
            {
                var hashCode = (int)_httpResultStatus;
                hashCode = (hashCode * 397) ^ _httpState.GetHashCode();
                return hashCode;
            }
        }

        #endregion

        [DebuggerStepThrough]
        public static HttpResult Ok()
        {
            return new HttpResult(HttpResultStatus.Ok, HttpState.Empty);
        }

        [DebuggerStepThrough]
        public static HttpResult Ok(HttpState httpState)
        {
            return new HttpResult(HttpResultStatus.Ok, httpState);
        }
        
        [DebuggerStepThrough]
        public static HttpResult Fail()
        {
            return new HttpResult(HttpResultStatus.Fail, HttpState.Empty);
        }

        [DebuggerStepThrough]
        public static HttpResult Fail(HttpState httpState)
        {
            return new HttpResult(HttpResultStatus.Fail, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> Ok<TValue>(TValue value)
        {
            return new HttpResult<TValue>(HttpResultStatus.Ok, value, HttpState.Empty);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> Ok<TValue>(TValue value, HttpState httpState)
        {
            return new HttpResult<TValue>(HttpResultStatus.Ok, value, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> Fail<TValue>()
        {
            return new HttpResult<TValue>(HttpResultStatus.Fail, Maybe<TValue>.Nothing, HttpState.Empty);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> Fail<TValue>(HttpState httpState)
        {
            return new HttpResult<TValue>(HttpResultStatus.Fail, Maybe<TValue>.Nothing, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Ok<TValue, TError>(TValue value)
        {
            return new HttpResult<TValue, TError>(HttpResultStatus.Ok, value, Maybe<TError>.Nothing, HttpState.Empty);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Ok<TValue, TError>(TValue value, HttpState httpState)
        {
            return new HttpResult<TValue, TError>(HttpResultStatus.Ok, value, Maybe<TError>.Nothing, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Fail<TValue, TError>(TError error)
        {
            return new HttpResult<TValue, TError>(HttpResultStatus.Fail, Maybe<TValue>.Nothing, error, HttpState.Empty);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Fail<TValue, TError>(TError error, HttpState httpState)
        {
            return new HttpResult<TValue, TError>(HttpResultStatus.Fail, Maybe<TValue>.Nothing, error, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult Combine(params HttpResult[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? Ok(State.HttpState.Empty)
                : results.First(x => x.IsFailure);
        }

        [DebuggerStepThrough]
        public static HttpResult Combine<T>(params HttpResult<T>[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? Ok(State.HttpState.Empty)
                : results.First(x => x.IsFailure).ToHttpResult();
        }


        [DebuggerStepThrough]
        public static HttpResultWithError<TError> Combine<TError>(params HttpResultWithError<TError>[] resultsWithError)
        {
            var anyFailure = resultsWithError.Any(x => x.IsFailure);
            return !anyFailure
                ? HttpResultWithError.Ok<TError>(State.HttpState.Empty)
                : resultsWithError.First(x => x.IsFailure);
        }

        [DebuggerStepThrough]
        public static HttpResultWithError<TError> Combine<TValue, TError>(params HttpResult<TValue, TError>[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? HttpResultWithError.Ok<TError>(State.HttpState.Empty)
                : results.First(x => x.IsFailure).ToHttpResultWithError();
        }
    }
}
