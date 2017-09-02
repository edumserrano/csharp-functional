.. include:: links.rst

HttpResult monad
================

Please read :ref:`about the result monad <result-monad>` first because the HttpResult monad is essentially the same with an added state property of type :ref:`IHttpState <http-state>`.
As the result monad, there are four variations of the HttpResult monad:

	* HttpResult
	* HttpResultWithError<TError>
	* HttpResult<TValue>
	* HttpResult<TValue,TError> 

All variations of the HttpResult monad contain an IsSuccess and it's inverse IsFailure property that indicate if the operation was successful or not; as well as an HttpState property that represents the http operation that was performed.

From a developers perspective there is one big difference between the HttpResult monad and the Result monad: whilst the Result monad can start to be used without any extra coding effort, the HttpResult monad requires a bit more coding effort. Please keep reading to understand how to take advantage of the HttpResult monad.

Installing
----------

The HttpResult monad NuGet package can be found at |HttpResultNuGet|_.
Installing is performed via NuGet::

	PM> Install-Package HttpResultMonad


HttpResult monads: How To 
-------------------------

Please read the :ref:`how-to for each variation of the Result monad <how-to-result-monad>` to understand how to use the HttpResult monads.
The only difference is that the HttpResult also requires an HttpState in its Ok and Fail methods. See :ref:`the HttpState <http-state>` to understand how to create an IHttpState instance.

To use this you should create a wrapper on the methods that do your http communication. For instance, if you are using the |HttpClient| class you could do the following::

    private HttpClient _httpClient = new HttpClient();

    public async Task<HttpResult> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken = default(CancellationToken))
    {
        var response = await _httpClient
                        .SendAsync(request, cancellationToken)
                        .ConfigureAwait(false);
        
        IHttpState httpState = new HttpClientState(response);
        return response.IsSuccessStatusCode
            ? HttpResult.Ok(httpState)
            : HttpResult.Fail(httpState);
    }

The above example shows the idea for using the HttpResult class based on |HttpClient|_. This code can be found at |HttpResultClientClass|_ and you can use it by installing |HttpResultOnHttpClientNuGet|_. 

Obviously you can use other implementations but what is important is that you are responsible for capturing the IHttpState as well as deciding whether you should return an ok or a fail HttpResult. This was just a very simple and generic example. Let's say that you knew that you only deal with json responses and you want to have type safety. Then you could implement something like::

    public async Task<HttpResult<T>> SendAsync<T>(
            HttpRequestMessage request,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
    {
        var response = await _httpClient
                        .SendAsync(request, cancellationToken)
                        .ConfigureAwait(false);
        
        var jsonBody = await response.Content.ReadAsStringAsync();
        var deserializedObj = /*deserialize the string jsonBody into an instance of type T*/

        IHttpState httpState = new HttpClientState(response);
        return response.IsSuccessStatusCode
            ? HttpResult.Ok<T>(deserializedObj,httpState)
            : HttpResult.Fail<T>(httpState);
    }

It is up to you to chose the best integration of the HttpResult monad with the class of your choosing that does the http communication in order to meat your requirements.

.. _http-state:

IHttpState: How To 
------------------

The IHttpState represents an http operation. See `here <https://github.com/edumserrano/csharp-functional/blob/master/Source/CSharpFunctional/HttpResultMonad/State/IHttpState.cs>`_ for the specification of this interface.

As explained in the previous section, to use HttpResult you will have to wrap the methods that you use do the http communication with it. When doing that you will also decide whether or not to capture the http state. If you do not pass any http state into the OK and Fail methods of the HttpResult then the http state will always be an `empty http state <https://github.com/edumserrano/csharp-functional/blob/master/Source/CSharpFunctional/HttpResultMonad/State/EmptyHttpState.cs>`_. 

See the |HttpClientState|_ class for an implementation of the IHttpState that is used with an HttpResult that uses the |HttpClient|. This HttpClientState implements the IHttpState interface based on System.Net.Http.HttpResponseMessage. Note that the Dispose method is responsible for disposing the System.Net.Http.HttpResponseMessage and System.Net.Http.HttpRequestMessage. Usually you do this by applying a using statement such as::

    
    private HttpClient _httpClient = new HttpClient();

    using(var httpResponse = await _httpClient.GetAsync("https://github.com"))
    {
        //do something with the httpResponse
    }

However in the |HttpResultClientClass|_ the HttpResponse is not disposed because the implementation of the IHttpState interface by the |HttpClientState|_ class does not immediately retrieve everything it needs to from it, namely the body. This was an implementation choice. I could have decided that I was happy with loading upfront the whole request and response bodies into the |HttpResultClientClass|. If I did that than I could dispose of the HttpResponseMessage on the |HttpResultClientClass| and the implementation of the Dispose method in the |HttpClientState| could be empty.

It is up to you to chose the best implementation for IHttpState that meats your requirements.

If you need to create an empty state do::

    using HttpResultMonad.State;

    var emptyState = HttpState.Empty;

To check if an HttpState is empty do::

	var emptyState = HttpState.Empty;
	var isEmptyState = emptyState.Equals(HttpState.Empty); //evaluates to true

HttpResult monad is disposable 
------------------------------

The Dispose method of the HttpResult just calls the Dispose method on its HttpState property which is of type IHttpState. This means two things:

* You should always dispose the HttpResult instances once you don't need them anymore.
* When implementing the IHttpState you should take care of releasing any disposable resources.

