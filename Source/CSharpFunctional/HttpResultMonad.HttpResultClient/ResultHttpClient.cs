using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using HttpResultMonad.State;

namespace HttpResultMonad.HttpResultClient
{
#warning refactor this class to accept an request message and expose only one SendAsync method
    public class ResultHttpClient : IDisposable
    {
        public ResultHttpClient(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            HttpClient = httpClient;
        }

        protected HttpClient HttpClient { get; }

        public Task<HttpResult<T>> GetAsync<T>(
            string requestUrl,
            string authHeader,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            return GetHttpResult<T>(
                HttpMethod.Get,
                requestUrl,
                authHeader,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult<T>> GetAsync<T>(
            string requestUrl,
            CancellationToken cancellationToken) where T : class
        {
            return GetHttpResult<T>(
                HttpMethod.Get,
                requestUrl,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult<Stream>> GetStreamAsync(
            string url,
            CancellationToken cancellationToken)
        {
            return GetStreamHttpResult(
                HttpMethod.Get,
                url,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult<Stream>> GetStreamAsync(
            string url,
            string authHeader,
            CancellationToken cancellationToken)
        {
            return GetStreamHttpResult(
                HttpMethod.Get,
                url,
                authHeader,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult<T>> PostAsync<T>(
            string url,
            HttpContent content,
            CancellationToken cancellationToken) where T : class
        {
            return GetHttpResult<T>(
                HttpMethod.Post,
                url,
                reqContent: content,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult<T>> PostAsync<T>(
            string url,
            HttpContent content,
            string authHeader,
            CancellationToken cancellationToken) where T : class
        {
            return GetHttpResult<T>(
                HttpMethod.Post,
                url,
                authHeader,
                content,
                cancellationToken);
        }

        public Task<HttpResult> PostAsync(
            string url,
            HttpContent content,
            CancellationToken cancellationToken)
        {
            return GetHttpResult(
                HttpMethod.Post,
                url,
                reqContent: content,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult> PostAsync(
            string url,
            HttpContent content,
            string authHeader,
            CancellationToken cancellationToken)
        {
            return GetHttpResult(
                HttpMethod.Post,
                url,
                authHeader,
                content,
                cancellationToken);
        }

        public Task<HttpResult<T>> PutAsync<T>(
            string url,
            HttpContent content,
            CancellationToken cancellationToken) where T : class
        {
            return GetHttpResult<T>(
                HttpMethod.Put,
                url,
                reqContent: content,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult<T>> PutAsync<T>(
            string url,
            HttpContent content,
            string authHeader,
            CancellationToken cancellationToken) where T : class
        {
            return GetHttpResult<T>(
                HttpMethod.Put,
                url,
                authHeader,
                content,
                cancellationToken);
        }

        public Task<HttpResult> PutAsync(
            string url,
            HttpContent content,
            CancellationToken cancellationToken)
        {
            return GetHttpResult(
                HttpMethod.Put,
                url,
                reqContent: content,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult> PutAsync(
            string url,
            HttpContent content,
            string authHeader,
            CancellationToken cancellationToken)
        {
            return GetHttpResult(
                HttpMethod.Put,
                url,
                authHeader,
                content,
                cancellationToken);
        }

        public Task<HttpResult> DeleteAsync(
            string url,
            CancellationToken cancellationToken)
        {
            return GetHttpResult(
                HttpMethod.Delete,
                url,
                cancellationToken: cancellationToken);
        }

        public Task<HttpResult> DeleteAsync(
            string url,
            string authHeader,
            CancellationToken cancellationToken)
        {
            return GetHttpResult(
                HttpMethod.Delete,
                url,
                authHeader,
                cancellationToken: cancellationToken);
        }

        private async Task<HttpResult> GetHttpResult(
            HttpMethod httpMethod,
            string url,
            string authHeader = null,
            HttpContent reqContent = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = CreateHttpRequest(httpMethod, url, authHeader, reqContent))
            {
                using (var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    var httpState = await HttpStateBuilder.BuildAsync(response);
                    return response.IsSuccessStatusCode
                        ? HttpResult.Ok(httpState)
                        : HttpResult.Fail(httpState);
                }
            }
        }

        private async Task<HttpResult<T>> GetHttpResult<T>(
            HttpMethod httpMethod,
            string url,
            string authHeader = null,
            HttpContent reqContent = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            using (var request = CreateHttpRequest(httpMethod, url, authHeader, reqContent))
            {
                using (var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    var httpState = await HttpStateBuilder.BuildAsync(response);
                    if (!response.IsSuccessStatusCode)
                    {
#warning sometimes the request fails and depending on the failure the response body will vary. 
                        // To deal with this the caller would have to look at the raw response body and try to parse to the possible failures responses (filtered more based on the status code could help)
                        return HttpResult.Fail<T>(httpState);
                    }

                    var deserializedBody = httpState.ResponseRawBody.DeserializeObject<T>();
                    if (deserializedBody == null)
                    {
#warning what to do here? throw exception , fail or just leave it returning null? leaving it returning null should fail trying to do HttpResult.Ok(deserializedBody, httpState); because should not be able to create an ok httpresult with null value
                        return HttpResult.Fail<T>(httpState);
                        //throw new InvalidOperationException($"Failed to deserialize to type {typeof(T)}");
                    }
                    return HttpResult.Ok(deserializedBody, httpState);
                }
            }
        }

        private async Task<HttpResult<Stream>> GetStreamHttpResult(
            HttpMethod httpMethod,
            string url,
            string authHeader = null,
            HttpContent reqContent = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var request = CreateHttpRequest(httpMethod, url, authHeader, reqContent))
            {
                using (var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    var httpState = await HttpStateBuilder.BuildAsync(response);
                    if (!response.IsSuccessStatusCode)
                    {
                        return HttpResult.Fail<Stream>(httpState);
                    }

                    Stream respStream = new MemoryStream();
                    await response.Content
                        .CopyToAsync(respStream)
                        .ConfigureAwait(false);
                    respStream.Position = 0;
                    return HttpResult.Ok(respStream, httpState);
                }
            }
        }
        private async Task<HttpResult> GetHttpResult(string url)
        {
            using (var response = await HttpClient.GetAsync(url))
            {
                var httpState = await HttpStateBuilder.BuildAsync(response);
                return response.IsSuccessStatusCode
                    ? HttpResult.Ok(httpState)
                    : HttpResult.Fail(httpState);
            }
        }

        private HttpRequestMessage CreateHttpRequest(
            HttpMethod httpMethod,
            string url,
            string authHeader = null,
            HttpContent content = null)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            if (authHeader != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
            }
            if (content != null)
            {
                request.Content = content;
            }
            return request;
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
