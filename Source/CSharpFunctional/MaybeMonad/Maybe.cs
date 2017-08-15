using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MaybeMonad
{
    /*
    The purpose of this struct is just for syntax usage of the Maybe struct.
    It allow the following syntax: Maybe.From<int> instead of Maybe<int>.From
    It thus allows keeping the same syntax between extension and non-extension methods.
    */
    public struct Maybe
    {
        [DebuggerStepThrough]
        public static Maybe<T> From<T>(T obj)
        {
            return obj == null
                ? new Maybe<T>(MaybeStatus.Empty, default(T))
                : new Maybe<T>(MaybeStatus.HasValue, obj);
        }
    }

    public struct Maybe<T> : IEquatable<Maybe<T>>
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly MaybeStatus _status;

        [DebuggerStepThrough]
        internal Maybe(MaybeStatus status, T value)
        {
            _value = value;
            _status = status;
        }

        public static Maybe<T> Nothing => new Maybe<T>(MaybeStatus.Empty, default(T));

        public T Value
        {
            [DebuggerStepThrough]
            get
            {
                if (HasNoValue)
                {
                    throw new InvalidOperationException(MaybeMessages.EmptyMaybe(typeof(T)));
                }

                return _value;
            }
        }

        public bool HasValue
        {
            [DebuggerStepThrough]
            get
            {
                return _status == MaybeStatus.HasValue;
            }
        }

        public bool HasNoValue
        {
            [DebuggerStepThrough]
            get
            {
                return !HasValue;
            }
        }

        [DebuggerStepThrough]
        public static implicit operator Maybe<T>(T value)
        {
            return value == null 
                ? Nothing 
                : new Maybe<T>(MaybeStatus.HasValue, value);
        }

        [DebuggerStepThrough]
        public static bool operator ==(Maybe<T> maybe, T value)
        {
            if (maybe.HasNoValue)
            {
                return false;
            }

            return maybe.Value.Equals(value);
        }

        [DebuggerStepThrough]
        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        [DebuggerStepThrough]
        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        [DebuggerStepThrough]
        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            Maybe<T> other;

            if (obj is T value)
            {
                other = new Maybe<T>(MaybeStatus.HasValue, value);
            }
            else if (obj is Maybe<T> maybe)
            {
                other = maybe;
            }
            else
            {
                return false;
            }

            return Equals(other);
        }

        [DebuggerStepThrough]
        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasValue && other.HasValue)
            {
                return _value.Equals(other._value);
            }

            return false; 
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ (int)_status;
            }
        }
    }
}
