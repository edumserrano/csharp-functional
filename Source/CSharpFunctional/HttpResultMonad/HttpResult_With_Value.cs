using System;
using System.Diagnostics;
using HttpResultMonad.State;
using MaybeMonad;

namespace HttpResultMonad
{
    public struct HttpResult<T> : IEquatable<HttpResult<T>>, IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<T> _value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HttpResultStatus _httpResultStatus;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IHttpState _httpState;

        [DebuggerStepThrough]
        internal HttpResult(HttpResultStatus status, Maybe<T> value, IHttpState httpState)
        {
            if (status == HttpResultStatus.Ok && value.HasNoValue)
            {
                throw new ArgumentNullException(nameof(value), HttpResultMessages.SuccessResultMustHaveValue);
            }

            _httpResultStatus = status;
            _httpState = httpState ?? throw new ArgumentNullException(nameof(httpState));
            _value = status == HttpResultStatus.Ok ? value : Maybe<T>.Nothing;
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

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException(HttpResultMessages.NoValueForFailure);
                }

                return _value.Value;
            }
        }

        public IHttpState HttpState
        {
            [DebuggerStepThrough]
            get
            {
                return _httpState;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(HttpResult<T> first, HttpResult<T> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(HttpResult<T> first, HttpResult<T> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (obj is HttpResult<T> other)
            {
                return Equals(other);
            }

            return false;
        }

        [DebuggerStepThrough]
        public bool Equals(HttpResult<T> other)
        {
            if (IsFailure && other.IsFailure)
            {
                return true;
            }

            if (IsSuccess && other.IsSuccess)
            {
                return Value.Equals(other.Value);
            }

            return false;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _value.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)_httpResultStatus;
                hashCode = (hashCode * 397) ^ typeof(T).GetHashCode();
                return hashCode;
            }
        }

        #endregion

        public void Dispose()
        {
            _httpState.Dispose();
        }
    }
}
