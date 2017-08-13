using System;

namespace MaybeMonad
{
    internal static class MaybeMessages
    {
        public static string EmptyMaybe(Type type) => $"Maybe<{type}> value is empty.";
    }
}