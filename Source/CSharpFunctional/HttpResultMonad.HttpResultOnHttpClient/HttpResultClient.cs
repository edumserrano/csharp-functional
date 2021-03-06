﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HttpResultMonad.State;

namespace HttpResultMonad.HttpResultOnHttpClient
{
    public class HttpResultClient : IDisposable
    {
        public HttpResultClient(HttpClient httpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        protected HttpClient HttpClient { get; }

        public async Task<HttpResult> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await HttpClient
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
            
            IHttpState httpState = new HttpClientState(response);
            return response.IsSuccessStatusCode
                ? HttpResult.Ok(httpState)
                : HttpResult.Fail(httpState);
        }
        
        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
