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
        private readonly Maybe<HttpState> _httpState;

        [DebuggerStepThrough]
        private HttpResult(HttpResultStatus httpResultStatus, Maybe<HttpState> httpState)
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
                return ((int)_httpResultStatus * 397) ^ _httpState.GetHashCode();
            }
        }

        #endregion

        [DebuggerStepThrough]
        public static HttpResult Ok()
        {
            return Ok(Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResult Ok(Maybe<HttpState> httpState)
        {
            return new HttpResult(HttpResultStatus.Ok, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult Fail()
        {
            return Fail(Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResult Fail(Maybe<HttpState> httpState)
        {
            return new HttpResult(HttpResultStatus.Fail, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> Ok<TValue>(TValue value)
        {
            return Ok(value, Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> Ok<TValue>(TValue value, Maybe<HttpState> httpState)
        {
            return new HttpResult<TValue>(HttpResultStatus.Ok, value, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> Fail<TValue>()
        {
            return Fail<TValue>(Maybe<HttpState>.Nothing);
        }


        [DebuggerStepThrough]
        public static HttpResult<TValue> Fail<TValue>(Maybe<HttpState> httpState)
        {
            return new HttpResult<TValue>(HttpResultStatus.Fail, Maybe<TValue>.Nothing, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Ok<TValue, TError>(TValue value)
        {
            return Ok<TValue, TError>(value, Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Ok<TValue, TError>(TValue value, Maybe<HttpState> httpState)
        {
            return new HttpResult<TValue, TError>(HttpResultStatus.Ok, value, Maybe<TError>.Nothing, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Fail<TValue, TError>(TError error)
        {
            return Fail<TValue, TError>(error, Maybe<HttpState>.Nothing);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> Fail<TValue, TError>(TError error, Maybe<HttpState> httpState)
        {
            return new HttpResult<TValue, TError>(HttpResultStatus.Fail, Maybe<TValue>.Nothing, error, httpState);
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue> From<TValue>(Func<bool> predicate, TValue value)
        {
            return predicate()
                ? Ok(value)
                : Fail<TValue>();
        }

        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> From<TValue, TError>(Func<bool> predicate, TValue value, TError error)
        {
            return predicate()
                ? Ok<TValue, TError>(value)
                : Fail<TValue, TError>(error);
        }

        [DebuggerStepThrough]
        public static HttpResult Combine(params HttpResult[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? Ok()
                : results.First(x => x.IsFailure);
        }

        [DebuggerStepThrough]
        public static HttpResult Combine<T>(params HttpResult<T>[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? Ok()
                : results.First(x => x.IsFailure).ToHttpResult();
        }


        [DebuggerStepThrough]
        public static HttpResultError<TError> Combine<TError>(params HttpResultError<TError>[] resultsError)
        {
            var anyFailure = resultsError.Any(x => x.IsFailure);
            return !anyFailure
                ? HttpResultError.Ok<TError>()
                : resultsError.First(x => x.IsFailure);
        }

        [DebuggerStepThrough]
        public static HttpResultError<TError> Combine<TValue, TError>(params HttpResult<TValue, TError>[] results)
        {
            var anyFailure = results.Any(x => x.IsFailure);
            return !anyFailure
                ? HttpResultError.Ok<TError>()
                : results.First(x => x.IsFailure).ToHttpResultError();
        }
    }
}
