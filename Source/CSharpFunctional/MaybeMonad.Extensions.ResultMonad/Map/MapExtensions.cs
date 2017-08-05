using System;
using ResultMonad;

namespace MaybeMonad.Extensions.ResultMonad.Map
{
    public static class MapExtensions
    {
        public static Result ToResult<T>(this Maybe<T> maybe)
        {
            return maybe.HasNoValue
                ? Result.Fail()
                : Result.Ok();
        }

        public static Result<T> ToResultWithValue<T>(this Maybe<T> maybe)
        {
            return maybe.HasNoValue
                ? Result.Fail<T>()
                : Result.Ok(maybe.Value);
        }

        public static Result<TValue, TError> ToResultWithValueAndError<TValue, TError>(
            this Maybe<TValue> maybe,
            Func<TError> errorFunc)
        {
            if (maybe.HasValue)
            {
                return Result.Ok<TValue, TError>(maybe.Value);
            }

            var error = errorFunc();
            return Result.Fail<TValue, TError>(error);
        }
    }
}