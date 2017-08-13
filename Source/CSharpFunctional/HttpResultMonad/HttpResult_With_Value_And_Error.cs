﻿using System;
using System.Diagnostics;
using HttpResultMonad.State;
using MaybeMonad;

namespace HttpResultMonad
{
    public struct HttpResult<TValue, TError> : IEquatable<HttpResult<TValue, TError>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<TError> _error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<TValue> _value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HttpResultStatus _httpResultStatus;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<HttpState> _httpState;

        [DebuggerStepThrough]
        internal HttpResult(
            HttpResultStatus status,
            Maybe<TValue> value,
            Maybe<TError> error,
            Maybe<HttpState> httpState)
        {
            if (status == HttpResultStatus.Fail && error.HasNoValue)
            {
                throw new ArgumentNullException(nameof(error), HttpResultWithErrorMessages.FailureResultMustHaveError);
            }

            if (status == HttpResultStatus.Ok && value.HasNoValue)
            {
                throw new ArgumentNullException(nameof(value), HttpResultWithErrorMessages.SuccessResultMustHaveValue);
            }

            _value = value;
            _error = error;
            _httpState = httpState;
            _httpResultStatus = status;
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

        public TValue Value
        {
            [DebuggerStepThrough]
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException(HttpResultWithErrorMessages.NoValueForFailure);
                }

                return _value.Value;
            }
        }

        public TError Error
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
        public static bool operator ==(HttpResult<TValue, TError> first, HttpResult<TValue, TError> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(HttpResult<TValue, TError> first, HttpResult<TValue, TError> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (obj is HttpResult<TValue, TError> other)
            {
                return Equals(other);
            }

            return false;
        }

        [DebuggerStepThrough]
        public bool Equals(HttpResult<TValue, TError> other)
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
                return Value.Equals(other.Value);
            }

            return false;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _error.GetHashCode();
                hashCode = (hashCode * 397) ^ _value.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)_httpResultStatus;
                hashCode = (hashCode * 397) ^ _httpState.GetHashCode();
                hashCode = (hashCode * 397) ^ typeof(TValue).GetHashCode();
                hashCode = (hashCode * 397) ^ typeof(TError).GetHashCode();
                return hashCode;
            }
        }

        #endregion

        [DebuggerStepThrough]
        public override string ToString()
        {
            return IsFailure ? HttpResultWithErrorMessages.GetFailureResultToStringMessage(typeof(TValue), typeof(TError)) : Value.ToString();
        }
    }
}
