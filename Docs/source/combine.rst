Combine methods
===============

The Combine methods are present for the Result and HttpResult monads. They allow you to evaluate a group of results and determine if they are all successful or not.

Combine methods for Result monads
---------------------------------

the available combine methods for the Result monads are::

	public static Result Combine(params Result[] results);

	public static Result Combine<T>(params Result<T>[] results);

	public static ResultWithError<TError> Combine<TError>(params ResultWithError<TError>[] resultsWithError);

	public static ResultWithError<TError> Combine<TValue, TError>(params Result<TValue, TError>[] results);

The first method combines an array of Result instances and returns an ok Result if all results are ok, otherwise it returns a failed Result;

The second method combines an array of Result<T> instances and returns an ok Result if all results are ok, otherwise it returns a failed Result. Note that it does not return a Result<T>. That's because even if in the case where at least one Result<T> is a fail result it could return the first failure, for the scenario where all are ok Result<T> instances it does not seem right to randomly chose one of the ok results.

The third method combines an array of ResultWithError<TError> instances and returns an ok ResultWithError<TError> if all results are ok, otherwise it returns the first failed ResultWithError<TError> in the array. In contrast with the previous method it does not "reduce" the ResultWithError<TError> to a Result because an ok ResultWithError does not have a value for Error. In other words all ok instances of ResultWithError<TError> with the same type of TError are equal.

The fourth method combines an array of Result<TValue, TError> instances and returns an ok ResultWithError<TError> if all results are ok, otherwise it returns a failed ResultWithError<TError> where the error is from the first fail Result<TValue, TError> in the array. Note that it does not return a Result<TValue, TError>. That's because even if in the case where at least one Result<T> is a fail result it could return the first failure, for the scenario where all are ok Result<TValue, TError>[] instances it does not seem right to randomly chose one of the ok results.

Combine methods for HttpResult monads
-------------------------------------

The available combine methods for the HttpResult monads are::

	public static HttpResult Combine(params HttpResult[] results);

	public static HttpResult Combine<T>(params HttpResult<T>[] results);

	public static HttpResultWithError<TError> Combine<TError>(params HttpResultWithError<TError>[] resultsWithError);

	public static HttpResultWithError<TError> Combine<TValue, TError>(params HttpResult<TValue, TError>[] results);

The first method combines an array of HttpResult instances and returns an ok HttpResult if all results are ok, otherwise it returns the first failed HttpResult.
In the case where all HttpResult instances are ok the returned ok HttpResult instance has an empty HttpState.

The second method combines an array of HttpResult<T> instances and returns an ok HttpResult if all results are ok, otherwise it returns a failed HttpResult. Note that it does not return a HttpResult<T>. That's because even if in the case where at least one HttpResult<T> is a fail result it could return the first failure, for the scenario where all are ok HttpResult<T> instances it does not seem right to randomly chose one of the ok results. 
In the case where all HttpResult instances are ok the returned ok HttpResult instance has an empty HttpState. Otherwise the failed HttpResult will contain the HttpState of the first fail HttpResult<T> in the array.

The third method combines an array of ResultWithError<TError> instances and returns an ok ResultWithError<TError> if all results are ok, otherwise it returns the first failed ResultWithError<TError> in the array. In contrast with the previous method it does not "reduce" the ResultWithError<TError> to a Result because an ok ResultWithError does not have a value for Error. In other words all ok instances of ResultWithError<TError> with the same type of TError are equal.
In the case where all HttpResult instances are ok the returned ok HttpResult instance has an empty HttpState. 

The fourth method combines an array of HttpResult<TValue, TError> instances and returns an ok HttpResultWithError<TError> if all results are ok, otherwise it returns a failed HttpResultWithError<TError> where the error is from the first fail HttpResult<TValue, TError> in the array. Note that it does not return a HttpResult<TValue, TError>. That's because even if in the case where at least one HttpResult<T> is a fail result it could return the first failure, for the scenario where all are ok HttpResult<TValue, TError>[] instances it does not seem right to randomly chose one of the ok results.
In the case where all HttpResult instances are ok the returned ok HttpResult instance has an empty HttpState.


Examples
--------

Here are a some examples of using the Combine methods on the Result monads. The usage of Combine methods for HttpResult monads is similar.

Combining a set of Result instances::

	var resultsLists = new List<Result>
	{
	    Result.Ok(),
	    Result.Fail(),
	    Result.Ok()
	};

	Result combinedResult = Result.Combine(resultsLists.ToArray()); //at least one Result is fail so the combinedResult is a fail Result

Combining a set of Result<TValue> instances::

	var resultsLists = new List<Result<string>>
	{
	    Result.Ok("value1"),
	    Result.Ok("value2"),
	    Result.Ok("value3")
	};

	Result combinedResult = Result.Combine(resultsLists.ToArray());
	/* 
	*  all Result<string> are ok so the combinedResult is an ok Result
	*  it is not a Result<string> because it doesn't seem right to randomly chose one of the values.
	*/

Combining a set of Result<TValue,TError> instances::

    var resultsLists = new List<Result<int, string>>
	{
	    Result.Ok<int,string>(1),
	    Result.Fail<int, string>("first error");
	    Result.Ok<int,string>(2),
	    Result.Fail<int,string>("second error")
	};

	ResultWithError<string> combinedResult = Result.Combine(resultsLists.ToArray()); 
	/* 
	*  at least one Result<int, string> is fail so the combinedResult is a fail ResultWithError<string>
	*  combinedResult.Error evaluates to "first error"
	*/
