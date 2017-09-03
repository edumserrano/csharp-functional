using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpResultMonad.HttpResultOnHttpClient.Tests
{
    internal class TestHttpClientHandler : HttpClientHandler
    {
        private readonly Func<Task<HttpResponseMessage>> _func;

        public TestHttpClientHandler(Func<Task<HttpResponseMessage>> func)
        {
            _func = func;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _func();
        }
    }
}
