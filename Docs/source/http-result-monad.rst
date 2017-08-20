.. include:: links.rst

HttpResult monad
================

Please read :ref:`about the result monad <result-monad>` first because the HttpResult monad is essentially the same with an added state property of type HttpState.
As the result monad, there are four variations of the HttpResult monad:

	* HttpResult
	* HttpResultWithError<TError>
	* HttpResult<TValue>
	* HttpResult<TValue,TError> 

All variations of the HttpResult monad contain an IsSuccess and it's inverse IsFailure property that indicate if the operation was successful or not; as well as an HttpState property that represents the http operation that was performed.

To use this you should create a wrapper on the methods that do your http communication. For instance, if you are using the HttpClient class you could do the following::

	private HttpClient _httpClient;

 	private async Task<HttpResult> GetHttpResult(string url)
    {
        using (var response = await _httpClient.GetAsync(url))
        {
            Maybe<HttpState> httpState = await HttpStateBuilder.BuildAsync(response);
            if (!response.IsSuccessStatusCode)
            {
                return HttpResult.Fail(httpState); 
            }

            return HttpResult.Ok(httpState);
        }
    }

The above example shows the idea for using the HttpResult class. You are responsible for capturing the HttpState and then deciding wheter you should return an ok or a fail HttpResult. To create an HttpState you should use the :ref:`HttpStateBuilder <http-state-builder>` class.

For an example class using HttpClient see the |HttpResultClientClass|_. If you want to use HttpResultClient install the |HttpResultOnHttpClientNuget|_.


Installing
----------

The HttpResult monad NuGet package can be found at |HttpResultNuget|_.
Installing is performed via NuGet::

	PM> Install-Package HttpResultMonad


HttpResult monads: How To 
-------------------------

Please read the :ref:`how-to for each variation of the Result monad <how-to-result-monad>` to understand how to use the HttpResult monads.
The only difference is that the HttpResult also requires an HttpState in its Ok() and Fail() methods. See :ref:`the HttpState <http-state>` and :ref:`HttpStateBuilder <http-state-builder>` sections to understand how to create an HttpState instance.

.. _http-state:

HttpState: How To 
------------------

The HttpState represents an the result of an http operation. It contains the following properties::

	public Uri Url { get; }

	public HttpMethod HttpMethod { get; }

	public HttpStatusCode HttpStatusCode { get; }

	public List<KeyValuePair<string, IEnumerable<string>>> RequestHeaders { get; }

	public string RequestRawBody { get; }

	public List<KeyValuePair<string, IEnumerable<string>>> ResponseHeaders { get; }

	public string ResponseRawBody { get; }

If you need to create an empty state do::

	var emptyState = HttpState.Empty;

To check if an HttpState is emtpy do::

	var emptyState = HttpState.Empty;
	var isEmptyState = emptyState.Equals(HttpState.Empty); //evaluates to true

.. _http-state-builder:

HttpStateBuilder: How To 
-------------------------------

The HttpStateBuilder allows you to contruct an HttpState. You can use the With methods and chose which properties you want to add to the HttpState and in the end call Build() as such::

	var state = new HttpStateBuilder()
                .WithHttpMethod(HttpMethod.Get)
                .WithUrl(new Uri("https://github.com"))
                .WithHttpStatusCode(HttpStatusCode.OK)
                .WithRequestRawBody("raw request body A")
                .WithResponseRawBody("raw response body A")
                .WithRequestHeaders(requestHeaders)
                .WithResponseHeaders(responseHeaders)
                .Build();

Or you can use the BuildAsync to create one from an HttpResponseMessage instance::

    HttpResponseMessage httpResponse = /*using HttpClient will return an instance of HttpResponseMessage*/
	var state = await HttpStateBuilder.BuildAsync(httpResponse);


