.. include:: links.rst

Maybe monad
===========

The Maybe monad encapsulates an optional value. An instance of Maybe either has a value of the encapsulated type or it doesn't in which case it is a Maybe<T>.Nothing.
This type is meant to be used in cases where your method might or might not return a value. Consider the following::

	public class UserRepository
	{
		public User GetById(int id)
		{
			//some implementation
		}
	}

	User user = _repository.GetById(id);

What happens if the user does not exist? Often a null value is returned. There are at least 2 problems with this:

	* The method GetById is not honest because it says it will return a User but in truth it returns a User instance or null.
	* The compiler has no way to assist you detecting that the variable user can be null. You are responsible for making sure your code always handles null values properly which might be trivial if you consider just this example but on a large code base it is not.

Applying the Maybe monad to the above example would result in the following::

	public class UserRepository
	{
		public Maybe<User> GetById(int id)
		{
			//some implementation
		}
	}

	Maybe<User> user = _repository.GetById(id);

This fixes both of the previous issues because now:
	* The method is honest and anyone using it understands that GetById might or might not return an instance of User.
	* The variable user is never null. 

Installing
----------

The Maybe monad NuGet package can be found at |MaybeNuget|_.
Installing is performed via NuGet::

	PM> Install-Package MaybeMonad

How To
------

To create a Maybe instance without value do::

	var emptyMaybe = Maybe<int>.Nothing;

To create a Maybe instance with value do::

	var maybeWithValue = Maybe.From("some text");

Attempting to create a Maybe instance with a null value results in an empty Maybe::

	var maybeWithNullValue = Maybe.From<string>(null);
	var isEqual = maybeWithNullValue.Equals(Maybe<string>.Nothing); //evaluates to true

To check if a Maybe instance has or does not have value do::

	var emptyMaybe = Maybe<int>.Nothing;
	var hasValue = emptyMaybe.HasValue;      //evaluates to false
	var hasNoValue = emptyMaybe.HasNoValue;  //evaluates to true

Accessing the value of an empty Maybe throws an InvalidOperationException exception::

	var emptyMaybe = Maybe<int>.Nothing;
	var value = emptyMaybe.Value; // throws exception

The Maybe type as an implicit converter for the type that is encapsulated. This means that you can do::

	Maybe<int> maybeInt = 2; //the implicit converter transforms the int 2 into Maybe<int> with a value of 2

Or more useful::

	public class UserRepository
	{
		public Maybe<User> GetById(int id)
		{
			User user;
			//db call to query for the user
			
			if(/*the user was not found*/)
			{
				return null; //the implicit converter means that what is returned is Maybe<User>.Nothing;
			}

			//the user was found, populate the user variable;			
			return user; //the implicit converter means that what is returned is Maybe.From(user);
		}
	}

