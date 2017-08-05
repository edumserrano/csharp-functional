using System;

namespace CSharp.Functional.ResultMonad
{
    internal static class ResultErrorMessages
    {
        public static string SuccessResult => "The operation was successfull.";
        public static string FailureResult => "The operation failed.";

        public static string NoValueForFailure => "There is no value for failure.";
        public static string NoErrorForSuccess => "There is no error for success.";
        public static string SuccessResultMustHaveValue => "Success result must have a value.";
        public static string FailureResultMustHaveError => "Error result must have an error.";
        public static string GetFailureResultToStringMessage(Type type) => $"Result<{type}> is a failure. {NoValueForFailure}";
        public static string GetSuccessResultErrorToStringMessage(Type type) => $"ResultError<{type}> is a success. {NoErrorForSuccess}";
    }
}