using System;
using System.Diagnostics;
using MaybeMonad;

namespace ResultMonad
{
    public struct Result<TValue, TError> : IEquatable<Result<TValue, TError>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<TError> _error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<TValue> _value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultStatus _resultStatus;

        [DebuggerStepThrough]
        internal Result(ResultStatus status, Maybe<TValue> value, Maybe<TError> error)
        {
            if (status == ResultStatus.Fail && error.HasNoValue)
            {
                throw new ArgumentNullException(nameof(error), ResultMessages.FailureResultMustHaveError);
            }

            if (status == ResultStatus.Ok && value.HasNoValue)
            {
                throw new ArgumentNullException(nameof(value), ResultMessages.SuccessResultMustHaveValue);
            }

            _value = value;
            _error = error;
            _resultStatus = status;
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

        public TValue Value
        {
            [DebuggerStepThrough]
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException(ResultMessages.NoValueForFailure);
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
                    throw new InvalidOperationException(ResultMessages.NoErrorForSuccess);
                }

                return _error.Value;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(Result<TValue, TError> first, Result<TValue, TError> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(Result<TValue, TError> first, Result<TValue, TError> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (obj is Result<TValue, TError> other)
            {
                return Equals(other);
            }

            return false;
        }

        [DebuggerStepThrough]
        public bool Equals(Result<TValue, TError> other)
        {
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
                hashCode = (hashCode * 397) ^ (int)_resultStatus;
                hashCode = (hashCode * 397) ^ typeof(TValue).GetHashCode();
                hashCode = (hashCode * 397) ^ typeof(TError).GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
