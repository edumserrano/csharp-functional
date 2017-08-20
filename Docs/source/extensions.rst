.. include:: links.rst

Extensions
==========

The extension methods available don't cover every possible scenario. I encourage you to take a look at the code in the |repo|_ and create your own.

One thing to note is that the extensions can perform transformations on the types being extended. What I mean is that an extension can be applied to a Result<T> and return a Result<K> or be applied to a Maybe<T> and return a Result<T>.

I think of these transformations in two kinds:

	* Generic type transformation: for instance from Result<T> to Result<K>. T became K.
	* Monad transformation: for instance from Maybe<T> to Result<T>

For generic type transformations you will need to provide a function for each type you want to transform. For instance an extension like::

	public static Result<KValue> OnSuccessToResultWithValue<TValue, KValue>(
	    this Result<TValue> result,
	    Func<TValue, KValue> onSuccessFunc)
	{
	    if (result.IsFailure)
	    {
	        return Result.Fail<KValue>();
	    }

	    var newValue = onSuccessFunc(result.Value);
	    return Result.Ok<KValue>(newValue);
	}

Mutates KValue to TValue by applying the onSuccessFunc to the instance of type TValue.

.. _monad-transformations:


For monad transformations you might not need any extra function if it's implicitly known how to create one monad from the other::

	public static Result<T> ToResultWithValue<T>(this Maybe<T> maybe)
	{
		/*the transformation from Maybe<T> to Result<T> is implict, no function is required*/
		return maybe.HasNoValue
	    	? Result.Fail<T>()
	    	: Result.Ok(maybe.Value);
	}

Since I know how to create a Result<T> from a Maybe<T> then no function is required. However if this transformation was not implicit then a function would have to be passed in to be applied and transform from one monad to another. 

In the NuGet packages there are extensions that mutate from Maybe monad to Result, Result<T>, Result<TValue,TError>; as well as many others that mutate from a variation of Result monad to a variation of HttpResult monad like from Result<TValue,TError> to HttpResult<KValue,Terror>.

.. note:: The underlying statement that derives from this introduction is that as a rule of thumb you should perform transformations that require only one function because of code readibility:
		  
		  * If you're doing a generic type transformation and at the same time a monad transformation then the monad transformation should be implicit so that you will only require the function to mutate the generic type. 
		  * There is no problem in breaking this advice. You can even mutate more than one generic type at a time and go from Monad<A,B,C> to Monad<X,Y,Z> but at the expense of having 3 mutating functions: A->X, B->Y and C->Z. 
		  * The more functions that need to be passed in to the extension method the worst the readibility of the code becomes. This is subject to personal opinion but I think that due to the C# syntax it becomes harder to read the code if you start to have many functions passed in as arguments on chained method calls. 


NuGet packages
--------------

The extension methods are divided into the following NuGet packages:

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

.. _maybe-extensions-examples:

Maybe extensions: examples
--------------------------

The Maybe extensions allow you to map from a maybe monad to a result monad.

Example usages are::

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

.. _result-and-http-result-extensions-examples:

Result and HttpResult extensions: examples
------------------------------------------

.. note:: keep in mind that:

		  * The examples are focused on the Result monad but the same applies for the HtttpResult monad. 
		  * Although the examples shown are for the type Result<TValue,TError> the same concepts apply to the other variations of the Result monad.

The available extensions are of the type:

* OnSuccess: executes if the IsSuccess property of the result is true;
* OnError: executes if the IsFailure property of the result is true;
* Ensure: evaluates a condition and executes if the condition is true;

One that is not yet implemented but is definitily useful would be the OnBoth. OnBoth would execute regardless of the IsSuccess/IsFailure of the result.

OnSuccess example
~~~~~~~~~~~~~~~~~

An example of using an OnSuccess extension is::

	public static Result<PlaylistId, PlaylistIdError> Create(int playlistId)
	{
		return Result.From(() => playlistId >= 0, playlistId, PlaylistIdError.CanNotBeNegative)
			.OnSuccessToResultWithValueAndError(id => new PlaylistId(id));
	}

In this example we start by creating a result with the From method. The From method will chech if the variable playlistId is positive and if it is then creates an ok Result<int,PlaylistIdError> with its property Value evaluating to the value of playlistId; if playlistId is negative it creates a fail Result<int,PlaylistIdError> with the Error property evaluating to PlaylistIdError.CanNotBeNegative.

After the call to OnSuccessToResultWithValueAndError will only execute if the result is a an ok result. If it is an ok result the onSuccess function is executed wand creates an ok Result<PlaylistId,PlaylistIdError> with an instance of PlaylistId in its Value property; if it is a fail result the  onSuccess function is not executed and a fail Result<PlaylistId,PlaylistIdError> is created and the error from the previous result is propagated, which means the property Error evaluates to PlaylistIdError.CanNotBeNegative.

Ensure example
~~~~~~~~~~~~~~

An example of using an Ensure extension is::

	public static Result<SearchQuery, SearchQueryError> Create(string searchQuery)
	{
		return Result.From(() => !string.IsNullOrEmpty(searchQuery), searchQuery, SearchQueryError.CanNotBeNullOrEmpty)
	        	.EnsureToResultWithValueAndError(query => query.Length <= 100, SearchQueryError.CanNotBeMoreThan100CharactersLong)
	        	.OnSuccessToResultWithValueAndError(query => new SearchQuery(query));
	}

In this example we start by creating a result with the From method. The From method will chech if the variable searchQuery has a value and if it has then creates an ok Result<string,SearchQueryError> with its property Value evaluating to the value of searchQuery; if searchQuery is null or empty it creates a fail Result<string,SearchQueryError> with the Error property evaluating to SearchQueryError.CanNotBeNullOrEmpty.

After the call to EnsureToResultWithValueAndError will only execute if the result is a an ok result. If it is an ok result the predicate function is executed and if it evaluates to true then an ok Result<string,SearchQueryError> propgating the value from the previous result with an instance; if it is an ok result but the predicate function evaluates to false then it creates a fail Result<string,SearchQueryError> and the Error property set to SearchQueryError.CanNotBeMoreThan100CharactersLong; if the previous result is a fail result then predicate function is not executed and a fail Result<string,SearchQueryError> is created and the Error property set to SearchQueryError.CanNotBeMoreThan100CharactersLong.

In the end the OnSuccessToResultWithValueAndError is executed which follows the rules explained in the previous example.

OnError example
~~~~~~~~~~~~~~~

An example of using an OnError extension is::

	/*reusing here the SearchQuery.Create method from the previous example*/
	var tag = "a tag";
	var query = "a query";

	Result<PlaylistSearchRequest,PlaylistSearchRequestError> result = SearchQuery
										.Create(searchQuery)
										.OnErrorToResultWithValueAndError(PlaylistSearchRequestError.From)
										.OnSuccessToResultWithValueAndError(query => new PlaylistSearchRequest(query, tag));

	/*here is the declaration for the method used on the onError function: 
	*
	* public static PlaylistSearchRequestError From(SearchQueryError error)
	*
	*/

In this example we start by creating a Result<SearchQuery,SearchQueryError> with the method SearchQuery.Create.

After the call to OnErrorToResultWithValueAndError will only execute if the result is a a fail result. If it is a fail result the onError function is executed and returns a Result<SearchQuery,PlaylistSearchRequestError> by converting the SearchQueryError instance to a PlaylistSearchRequestError; if the result returned from the SearchQuery.Create method is an ok result then the onError function is not executed and an ok Result<SearchQuery,PlaylistSearchRequestError> is returned. In this case the Value property of the new ok result contains the value from the previous result.

In the end the OnSuccessToResultWithValueAndError is executed which follows the rules explained in the previous example.

.. note:: Notice that the TValue and TError types do not have constrains, they can be whatever you want. 
          In these examples the TValue were primitve types (int, string) as well as custom classes (PlaylistId, SearchQuery, PlaylistSearchRequest).
          In these examples the TError were only custom classes (PlaylistIdError, SearchQueryError, PlaylistSearchRequestError) but they could be whatever you would like to use like Enums for instance.


Mapping from one monad type to another
--------------------------------------

In your application you can use different types of monads at the same type. You can use all the ones mentioned in this documentation and even other's you have defined. To make them work together you will need a way to map from one type to another.

The extensions available will tell you the type that will be returned. Some extensions will not perform any mapping and some will. For instance you can have one extension that takes a Result<TValue> and returns a ResultWithError<TError>. These transformations are indicated by the ToX appended to the extension method names.

Another example is going from a Maybe type to a Result type. You can call ToResult() on a Maybe instance and it will return you a Result instance. See :ref:`here <maybe-extensions-examples>` for more details.

These transformations are essential to allow the railway oriented programming style described in the next section.

Putting it all together: Railway-oriented programming
-----------------------------------------------------

Scott Wlaschin gave a great `talk on NDC  <https://vimeo.com/97344498>`_ about this concept. 

The description of the talk is:

"When you build real world applications, you are not always on the "happy path". You must deal with validation, logging, network and service errors, and other annoyances.
How do you manage all this within a functional paradigm, when you can't use exceptions, or do early returns, and when you have no stateful data?
This talk will demonstrate a common approach to this challenge, using a fun and easy-to-understand "railway oriented programming" analogy. You'll come away with insight into a powerful technique that handles errors in an elegant way using a simple, self-documenting design."

I advise you to watch the talk because it is very good and whatever I do to try and summarize the idea will never be as good.

For the purposes of this documentation I will give you two examples of how you can use the monad NuGets and its extensions. I will show the same method implemented with and without the use of the extensions and you will see that the code using extensions is much more compact and once you get used to this style of programming it is also a more expressive way of coding.

Example A1: in this example we want to get the reposters of a track. Let's do it without extension methods::

	public async Task<HttpResult<UsersSet, GetTrackRepostersError>> GetTrackRepostersAsync(
	    int trackId,
	    int limit,
	    CancellationToken cancellationToken = default(CancellationToken))
	{
	    var trackIdResult = TrackId.Create(trackId);
	    if (trackIdResult.IsFailure)
	    {
	        var trackIdError = GetTrackRepostersError.From(trackIdResult.Error);
	        return HttpResult.Fail<UsersSet, GetTrackRepostersError>(trackIdError);
	    }

	    var limitResult = Limit.Create(limit);
	    if (limitResult.IsFailure)
	    {
	        var limitError = GetTrackRepostersError.From(limitResult.Error);
	        return HttpResult.Fail<UsersSet, GetTrackRepostersError>(limitError);
	    }

	    var requestUrl = _trackUrlBuilder.GetTrackRepostersUrl(trackIdResult.Value, limitResult.Value);
	    var httpGetResult = await HttpClient.GetAsync<UsersSet>(requestUrl, cancellationToken);
	    if (httpGetResult.IsFailure)
	    {
	        return HttpResult.Fail<UsersSet, GetTrackRepostersError>(GetTrackRepostersError.HttpFail);
	    }

	    var usersSet = httpGetResult.Value;
	    return HttpResult.Ok<UsersSet, GetTrackRepostersError>(usersSet);
	}

Example A2: now with extension methods::

	public Task<HttpResult<UsersSet, GetTrackRepostersError>> GetTrackRepostersAsync(
	    int trackId,
	    int limit,
	    CancellationToken cancellationToken = default(CancellationToken))
	{
	    var trackIdResult = TrackId.Create(trackId);
	    var limitResult = Limit.Create(limit);
	    var inputResult = Result.Combine(
	        trackIdResult.OnErrorToResultWithError(GetTrackRepostersError.From),
	        limitResult.OnErrorToResultWithError(GetTrackRepostersError.From));

	    return inputResult
	        .OnSuccessToHttpResultWithValueAndError(() =>
	        {
	            var requestUrl = _trackUrlFactory.GetTrackRepostersUrl(trackIdResult.Value, limitResult.Value);
	            return HttpClient
	                .GetAsync<UsersSet>(requestUrl, cancellationToken)
	                .OnErrorToHttpResultWithValueAndError(() => GetTrackRepostersError.HttpFail);
	        });
	}

Here are the methods used in the examples A1 and A2::
	
	public static Result<TrackId, TrackIdError> Create(int trackId);
	
	public static Result<Limit, LimitError> Create(int limit);
	
	public static GetTrackRepostersError From(TrackIdError trackIdError);
	
	public static GetTrackRepostersError From(LimitError limitError);
	
	public string GetTrackRepostersUrl(TrackId trackId, Limit limit);
	
	public Task<HttpResult<T>> GetAsync<T>(string requestUrl, CancellationToken cancellationToken) where T : class
	
	public static GetTrackRepostersError HttpFail { get; } 
	 
I'll explain the code in both examples but focusing on the example with extension methods.
	
The first thing is to validate the input arguments and we do that by creating an inputResult that combines the results from the TrackId.Create and Limit.Create methods. If both results are ok then the inputResult is an ok ResultWithError<GetTrackRespostersError>; if at least one is a fail result then the inputResult is a fail ResultWithError<GetTrackRespostersError> and its Error property is set to the first failure result of the array passed into the Result.Combine method.

Now we can call OnSuccessToHttpResultWithValueAndError which is only executed if the inputResult is an ok result. If it is a fail result then the onSuccess function is not executed and it returns a fail Task<HttpResult<UsersSet, GetTrackRepostersError>> with the Error property set to the error that was on the inputResult.
If the inputResult is an ok result then the onSuccess function is executed and what it is doing is creating an url to do the http call, calling HttpClient.GetAsync which will return a Task<HttpResult<UsersSet>>.

Finally if the result from the call to HttpClient.GetAsync is an ok result then OnErrorToHttpResultWithValueAndError is not executed and an ok Task<HttpResult<UsersSet, GetTrackRepostersError>> is returned. Its Value property is set to the value that was on the previous result returned from the call to HttpClient.GetAsync.
If an fail result was returned from the call to HttpClient.GetAsync then the onError function is executed and a fail Task<HttpResult<UsersSet, GetTrackRepostersError>> is returned with its Error property set to GetTrackRepostersError.HttpFail.

Example B1: in this example we want to do a sign up. Let's do it without extension methods::

	public async Task<HttpResult<SignUp, SignUpError>> SignUpAsync(
		SignUpRequest signUpRequest,
		CancellationToken cancellationToken = default(CancellationToken))
	{
	    var signUpRequestResult = Result.From(() => signUpRequest != null, signUpRequest, SignUpError.SignUpRequestCanNotBeNull);
	    if (signUpRequestResult.IsFailure)
	    {
	        return HttpResult.Fail<SignUp, SignUpError>(signUpRequestResult.Error);
	    }

	    var emailSignUpTokenResult = await GetEmailSignUpTokenAsync(signUpRequest.Email, cancellationToken);
	    if (emailSignUpTokenResult.IsFailure)
	    {
	        return HttpResult.Fail<SignUp, SignUpError>(SignUpError.HttpFail);
	    }

	    var emailSignUpTokenResp = emailSignUpTokenResult.Value;
	    var signUpResult = await SignUpAsync(emailSignUpTokenResp.EmailSignUpToken, signUpRequest, cancellationToken);
	    if (signUpResult.IsFailure)
	    {
	        return HttpResult.Fail<SignUp, SignUpError>(SignUpError.HttpFail);
	    }

	    var signUp = signUpResult.Value;
	    return HttpResult.Ok<SignUp, SignUpError>(signUp);
	}

Example B2: now with extension methods::

	public Task<HttpResult<SignUp, SignUpError>> SignUpAsync(
	    SignUpRequest signUpRequest,
	    CancellationToken cancellationToken = default(CancellationToken))
	{
	    return Result.From(() => signUpRequest != null, signUpRequest, SignUpError.SignUpRequestCanNotBeNull)
	        .OnSuccessToHttpResultWithValueAndError(request =>
	        {
	            return GetEmailSignUpTokenAsync(signUpRequest.Email, cancellationToken)
	                .OnErrorToHttpResultWithValueAndError(() => SignUpError.HttpFail);
	        })
	        .OnSuccessToHttpResultWithValueAndError(emailSignUpTokenResp =>
	        {
	            return SignUpAsync(emailSignUpTokenResp.EmailSignUpToken, signUpRequest, cancellationToken)
	                .OnErrorToHttpResultWithValueAndError(() => SignUpError.HttpFail);
	        });
	}

Here are the methods used in the examples B1 and B2::

	private Task<HttpResult<EmailSignUpToken>> GetEmailSignUpTokenAsync(Email email, CancellationToken cancellationToken)
	
	private Task<HttpResult<SignUp>> SignUpAsync(string emailSignUpToken, SignUpRequest signUpRequest, CancellationToken cancellationToken);
	
	public static SignUpError SignUpRequestCanNotBeNull { get;}
	
	public static SignUpError HttpFail { get;}

Again, I'll explain the code in both examples but focusing on the example with extension methods.

The first thing is to validate the input. If the signUpRequest is null then the Result.From will return a fail Result<SignUpRequest,SignUpError> with its Error property set to SignUpError.SignUpRequestCanNotBeNull.
If the signupRequest is not null then the Result.From returns an ok Result<SignUpRequest,SignUpError> with its Value property set to what is passed in by signUpRequest.

After we call OnSuccessToHttpResultWithValueAndError. If the previous result was a fail result then the onSuccessFunction is not executed and it returns a fail Task<HttpResult<SignUp, SignUpError>> with the Error property propagated from what was in the previous result.
If the previous result was an ok result then the onSuccessFunction is executed and it tries to get an email signup token. The call to GetEmailSignUpTokenAsync returns an Task<HttpResult<EmailSignUpToken>> which after the call to OnErrorToHttpResultWithValueAndError will either be an ok or a fail Task<HttpResult<EmailSignUpToken, SignUpError>>. If it's an ok result then the Value will contain the value from the result that was returned from the call to GetEmailSignUpTokenAsync; if it's a fail result then the Error property will contain the error from the onErrorFunction which is SignUpError.HttpFail;

Finally, another call to OnSuccessToHttpResultWithValueAndError. Again, if the previous result was a fail result then the onSuccessFunction is not executed and it returns a fail Task<HttpResult<SignUp, SignUpError>> with the Error property propagated from what was in the previous result.
If the previous result was an ok result then the onSuccessFunction is executed and it tries to perform the singup. The call to SignUpAsync returns an Task<HttpResult<SignUp>> which after the call to OnErrorToHttpResultWithValueAndError will either be an ok or a fail Task<HttpResult<SignUp, SignUpError>>. If it's an ok result then the Value will contain the value from the result that was returned from the call to SignUpAsync; if it's a fail result then the Error property will contain the error from the onErrorFunction which is SignUpError.HttpFail;










