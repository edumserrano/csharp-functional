using System;

namespace CSharp.Functional.MaybeMonad
{
    internal static class MaybeErrorMessages
    {
        public static string EmptyMaybe(Type type) => $"Maybe<{type}> value is empty.";
    }
}