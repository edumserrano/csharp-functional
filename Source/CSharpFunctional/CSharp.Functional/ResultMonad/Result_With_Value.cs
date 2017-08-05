using System;
using System.Diagnostics;
using CSharp.Functional.MaybeMonad;

namespace CSharp.Functional.ResultMonad
{
    public struct Result<T> : IEquatable<Result<T>>
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Maybe<T> _value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ResultStatus _resultStatus;

        [DebuggerStepThrough]
        internal Result(ResultStatus status, Maybe<T> value)
        {
            if (status == ResultStatus.Ok && value.HasNoValue)
            {
                throw new ArgumentNullException(nameof(value), ResultErrorMessages.SuccessResultMustHaveValue);
            }

            _resultStatus = status;
            _value = status == ResultStatus.Ok ? value : Maybe<T>.Nothing;
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

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException(ResultErrorMessages.NoValueForFailure);
                }

                return _value.Value;
            }
        }

        #region [IEquatable]

        [DebuggerStepThrough]
        public static bool operator ==(Result<T> result, T value)
        {
            if (result.IsFailure)
            {
                return false;
            }

            return result.Value.Equals(value);
        }

        [DebuggerStepThrough]
        public static bool operator !=(Result<T> result, T value)
        {
            return !(result == value);
        }

        [DebuggerStepThrough]
        public static bool operator ==(Result<T> first, Result<T> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(Result<T> first, Result<T> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            Result<T> other;

            if (obj is T value)
            {
                other = new Result<T>(ResultStatus.Ok, value);
            }
            else if (obj is Result<T> result)
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
        public bool Equals(Result<T> other)
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
                return (_value.GetHashCode() * 397) ^ (int)_resultStatus;
            }
        }

        #endregion

        [DebuggerStepThrough]
        public override string ToString()
        {
            return IsFailure
                ? ResultErrorMessages.GetFailureResultToStringMessage(typeof(T))
                : Value.ToString();
        }
    }
}
