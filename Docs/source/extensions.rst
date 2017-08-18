Extensions
==========

The extension methods available don't cover every possible scenario. I encourage you to take a look at the code in the |repo|_ and create your own.

Out of the box extensions
-------------------------

The extension methods divided into the following NuGet packages::

* |ResultMonadExtensionsNuget|_ : extensions to the result monads that always return another instance of a result monad.
* |HttpResultMonadExtensionsNuget|_ : extensions to the http result monads that always return another instance of an http result monad.
* |ResultMonadExtensionsHttpResultMonadNuget|_ : extensions to the result monad that return an instance of an http result monad.
* |MaybeMonadExtensionsResultMonadNuget|_ : extensions to the maybe monad that return an instance of a result monad.

The |ResultMonadExtensionsHttpResultMonadNuget| is separated from the |ResultMonadExtensionsNuget|, just as the |ResultMonadExtensionsHttpResultMonadNuget| is separated from the |HttpResultMonadExtensionsNuget|, because it allows you to only work with either the Result monad or the HttpResult monad. 
If they were together then installing extensions for the result monad would mean that you would have to install the http result monad NuGet as well even though you didn't want to use it.

Installing
----------

Installing is performed via NuGet. For example to install the |ResultMonadExtensionsNuget| do::

	PM> Install-Package ResultMonad.Extensions

Maybe extensions
----------------

The only Maybe extensions allow to map from a maybe monad to a result monad.

Example usages would be::

	Maybe<User> maybeUser = GetUserById(id);
	Result result1 = maybeUser.ToResult();
	/*
	* if maybeUser.HasValue is true then result1.IsSuccess is true.
	* if maybeUser.HasValue is false then result1.IsFailure is true.
	*/

	Result<User> result2 = maybeUser.ToResultWithValue(); 
	/*
	* if maybeUser.HasValue is true then result2.IsSuccess is true and result2.Value evaluates to maybeUser.Value.
	* if maybeUser.HasValue is false then result2.IsFailure is true.
	*/
	
	Result<User,string> result3 = maybeUser.ToResultWithValueAndError(()=>"no user found"); //result3.Value evaluates to maybeUser.Value if ;
	/*
	* if maybeUser.HasValue is true then result3.IsSuccess is true and result3.Value evaluates to maybeUser.Value.
	* if maybeUser.HasValue is false then result3.IsFailure is true and result3.Value evaluates to "no user found".
	*/

Result and HttpResult extensions
--------------------------------

.. note:: here when the word result is used it refers to either a result monad or an http result monad.

The available extensions are of the type:

* OnSuccess: executes if the IsSuccess property of the result is true;
* OnFailure: executes if the IsFailure property of the result is true;
* Ensure: evaluates a condition and executes if the condition is true;

One that is not yet implemented but is definitily useful would be the OnBoth. OnBoth would execute regardless of the IsSuccess/IsFailure of the result.

An example of using an OnSuccess extension would be::


An example of using an OnFailure extension would be::


An example of using an Ensure extension would be::


Mapping from one monad type to another
--------------------------------------



Putting it all together
-----------------------
