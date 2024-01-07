using CosmosNetwork.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace CosmosNetwork.API
{
    public abstract class CosmosApiModule
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CosmosApiModule> _logger;

        protected internal CosmosApiModule(string serviceKey, IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory, ILogger<CosmosApiModule> logger)
        {
            CosmosApiOptions options = serviceProvider.GetRequiredKeyedService<CosmosApiOptions>(serviceKey);

            this._httpClientFactory = httpClientFactory;
            this._logger = logger;

            this.Options = options;
            this.JsonSerializerOptions = options.JsonSerializerOptions;
        }

        protected JsonSerializerOptions JsonSerializerOptions { get; init; }

        protected CosmosApiOptions Options { get; init; }

        protected Task<T?> Get<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            return Get<T>(PrepareEndpoint(endpoint), cancellationToken);
        }

        protected async Task<T?> Get<T>(Uri endpoint, CancellationToken cancellationToken = default)
        {
            this._logger.LogTrace("GET {endpoint}", endpoint);

            HttpClient httpClient = GetHttpClient();
            HttpResponseMessage httpResponse = await httpClient.GetAsync(endpoint, cancellationToken);

            if (httpResponse.IsSuccessStatusCode == false)
            {
                this._logger.LogError("GET {endpoint}: {statusCode} - {reasonPhrase}", endpoint, httpResponse.StatusCode, httpResponse.ReasonPhrase);
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default;
                }
                else
                {
                    throw new CosmosApiException(httpResponse.StatusCode, $"an error occurred while calling {endpoint}");
                }
            }

            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            string stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            this._logger.LogTrace("Get {endpoint} received {response}", endpoint, stringResponse);
#endif

            Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<T>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected Task<TR?> Post<TP, TR>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            return Post<TP, TR>(PrepareEndpoint(endpoint), request, cancellationToken);
        }
            
        protected async Task<TR?> Post<TP, TR>(Uri endpoint, TP request, CancellationToken cancellationToken = default)
        {
            string serializedRequest = JsonSerializer.Serialize(request, options: this.JsonSerializerOptions);
            using StringContent httpContent = new(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            this._logger.LogTrace("Post {endpoint} sending {request}", endpoint, serializedRequest);
#endif

            HttpClient httpClient = GetHttpClient();
            using HttpResponseMessage httpResponse = await httpClient.PostAsync(endpoint, httpContent, cancellationToken).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            string stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            this._logger.LogTrace("Post {endpoint} received {response}", endpoint, stringResponse);
#endif

            Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected Task<(TR? ResponseOK, TErr? ResponseError)> Post<TP, TR, TErr>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            return Post<TP, TR, TErr>(PrepareEndpoint(endpoint), request, cancellationToken);
        }
         
        protected async Task<(TR? ResponseOK, TErr? ResponseError)> Post<TP, TR, TErr>(Uri endpoint, TP request, CancellationToken cancellationToken = default)
        {
            string serializedRequest = JsonSerializer.Serialize(request, options: this.JsonSerializerOptions);
            using StringContent httpContent = new(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            this._logger.LogTrace("Post {endpoint} sending {request}", endpoint, serializedRequest);
#endif

            HttpClient httpClient = GetHttpClient();
            using HttpResponseMessage httpResponse = await httpClient.PostAsync(endpoint, httpContent, cancellationToken).ConfigureAwait(false);

#if DEBUG_API
            string stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            this._logger.LogTrace("Post {endpoint} received {response}", endpoint, stringResponse);
#endif

            if (httpResponse.IsSuccessStatusCode)
            {
                Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                return (ResponseOK: await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken), ResponseError: default);
            }
            else
            {
                try
                {
                    Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                    return (ResponseOK: default, ResponseError: await JsonSerializer.DeserializeAsync<TErr>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken));
                }
                catch (JsonException)
                {
#if DEBUG_API == false
                    var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
#endif
                    this._logger.LogError("Invalid json detected: {reponse}", stringResponse);
                    throw new InvalidResponseException(stringResponse);
                }
            }
        }

        private static Uri PrepareEndpoint(string endpoint)
        {
            return new Uri(endpoint.StartsWith('/') ? endpoint[1..] : endpoint, UriKind.Relative);
        }

        private HttpClient GetHttpClient()
        {
            return this.Options.HttpClientName is null
                ? this._httpClientFactory.CreateClient()
                : this._httpClientFactory.CreateClient(this.Options.HttpClientName);
        }
    }
}
