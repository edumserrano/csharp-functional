using System;

namespace MaybeMonad
{
    internal static class MaybeErrorMessages
    {
        public static string EmptyMaybe(Type type) => $"Maybe<{type}> value is empty.";
    }
}