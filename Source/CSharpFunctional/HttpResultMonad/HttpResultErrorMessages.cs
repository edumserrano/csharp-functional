using System;

namespace HttpResultMonad
{
    internal static class HttpResultWithErrorMessages
    {
        public static string NoValueForFailure => "There is no value for failure.";
        public static string NoErrorForSuccess => "There is no error for success.";
        public static string SuccessResultMustHaveValue => "Success result must have a value.";
        public static string FailureResultMustHaveError => "Failure result must have an error.";
        public static string GetFailureResultToStringMessage(Type type) => $"HttpResult<{type}> is a failure. {NoValueForFailure}";
        public static string GetFailureResultToStringMessage(Type type, Type error) => $"HttpResult<{type},{error}> is a failure. {NoValueForFailure}";
        public static string GetSuccessResultWithErrorToStringMessage(Type type) => $"HttpResultWithError<{type}> is a success. {NoErrorForSuccess}";

    }
}