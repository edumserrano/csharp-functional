.. include:: links.rst

.. _result-monad:

Result monad
============

The essence of the Result monad is to augment the outcome of an operation with a success status. 
There are four variations of the Result monad:

	* Result : simply augments the outcome of an operation with a success status.
	* ResultWithError<TError> : besides the success status it includes an Error property which must have a value if the success status is false.
	* Result<TValue> : besides the success status it includes a Value property which must have a value if the success status is true.
	* Result<TValue,TError> : besides the success status it includes a Value property which must have a value if the success status is true and an Error property which must have a value if the success status is false.

All variations of the Result monad contain an IsSuccess and it's inverse IsFailure property that indicate if the operation was successful or not.

Installing
----------

The Result monad NuGet package can be found at |ResultNuget|_.
Installing is performed via NuGet::

	PM> Install-Package ResultMonad

.. _how-to-result-monad:

Result: How To 
--------------

The Result type is meant to be used when the method does not return any value and if it fails you do not care about the details of the failure.

To create a Result instance do::

	var okResult = Result.Ok();
	var failResult = Result.Fail();

ResultWithError: How To 
-----------------------

The ResultWithError<TError> type is meant to be used when the method does not return any value but if it fails you want information regarding the failure.

To create a ResultWithError instance do::

	var okResult = ResultWithError.Ok<string>();
	var failResult = ResultWithError.Fail("some error info");

To access the error do::

	var failResult = ResultWithError.Fail("some error info");
	var error = failResult.Error; // evaluates to "some error info"

Accessing the Error property of an ok ResultWithError throws InvalidOperationException exception::

	var okResult = ResultWithError.Ok<string>();
	var error = failResult.Error; //throws exception

You can also use the From method to create a ResultWithError. It accepts a predicate function and an error. If the predicate evaluates to true the method returns an ok ResultWithError, if the predicate evaluates to false it returns a fail ResultWithError containing the error::

	var okResult = ResultWithError.From(()=>true, "some error info"); 	// creates an ok ResultWithError<string>
	var failResult = ResultWithError.From(()=>false, "some error info");	// creates a fail ResultWithError<string> with "some error info" as the error

Result<TValue>: How To 
----------------------

The Result<TValue> type is meant to be used when the method does returns a value but if it fails you do not want information regarding the failure.

To create a Result<TValue> instance do::

	var okResult = Result.Ok("some value");
	var failResult = Result.Fail<string>();

To access the value do::

	var okResult = Result.Ok("some value");
	var value = okResult.Value; // evaluates to "some value"

Accessing the Value property of a fail Result<TValue> throws InvalidOperationException exception::

	var okResult = Result.Ok<string>();
	var error = okResult.Error; //throws exception

You can also use the From method to create a Result<TValue>. It accepts a predicate function and a value. If the predicate evaluates to true the method returns an ok Result<TValue> containing the value, if the predicate evaluates to false it returns a fail Result<TValue>::

	var okResult = Result.From(()=>true, "some value"); 	// creates an ok Result<string> with "some value" as the value
	var failResult = Result.From(()=>false, "some value");	// creates a fail Result<string> 

Result<TValue,TError>: How To 
-----------------------------

The Result<TValue,TError> type is meant to be used when the method does returns a value and if in addition, if it fails, it will return an error.

To create a Result<TValue,TError> instance do::

	var okResult = Result.Ok<string,int>("some value");
	var failResult = Result.Fail<string,int>(0);

To access the value do::

	var okResult = Result.Ok<string,int>("some value");
	var value = okResult.Value; // evaluates to "some value"

To access the error do::

	var failResult = Result.Fail<string,int>(0);
	var error = failResult.Error; // evaluates to 0

Accessing the Value property of a fail Result<TValue,TError> throws InvalidOperationException exception::

	var failResult = Result.Fail<string,int>(0);
	var value = failResult.Value; //throws exception

Accessing the Error property of an ok Result<TValue,TError> throws InvalidOperationException exception::

	var okResult = Result.Ok<string,int>("some value");
	var error = okResult.Error; //throws exception

You can also use the From method to create a Result<TValue,TError>. It accepts a predicate function, a value and an error. If the predicate evaluates to true the method returns an ok Result<TValue,TError> containing the value, if the predicate evaluates to false it returns a fail Result<TValue,TError> containing the error::

	var okResult = Result.From<string,int>(()=>true, "some value",0); 	// creates an ok Result<string,int> with "some value" as the value
	var failResult = Result.From<string,int>(()=>false, "some value",0);	// creates a fail Result<string,int> with 0 as the error 
