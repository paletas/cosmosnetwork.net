using CosmosNetwork.Exceptions;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace CosmosNetwork.API
{
    internal class BaseApiSection
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseApiSection> _logger;

        public BaseApiSection(CosmosApiOptions options, HttpClient httpClient, ILogger<BaseApiSection> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;

            Options = options;
            JsonSerializerOptions = options.JsonSerializerOptions;
        }

        protected JsonSerializerOptions JsonSerializerOptions { get; init; }

        protected CosmosApiOptions Options { get; init; }

        protected Task<T?> Get<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            if (this._httpClient.BaseAddress is null)
                throw new InvalidOperationException();

            return this.Get<T>(new Uri(this._httpClient.BaseAddress, PrepareEndpoint(endpoint)), cancellationToken);
        }

        protected async Task<T?> Get<T>(Uri endpoint, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("GET {endpoint}", endpoint);

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(endpoint, cancellationToken);

            if (httpResponse.IsSuccessStatusCode == false)
            {
                _logger.LogError("GET {endpoint}: {statusCode} - {reasonPhrase}", endpoint, httpResponse.StatusCode, httpResponse.ReasonPhrase);
                return default;
            }

            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            string stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogTrace($"Get {endpoint} received {stringResponse}");
#endif

            Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<T>(streamResponse, options: JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected async Task<TR?> Post<TP, TR>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            string serializedRequest = JsonSerializer.Serialize(request, options: JsonSerializerOptions);
            using StringContent httpContent = new(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            _logger.LogTrace($"Post {endpoint} sending {serializedRequest}");
#endif

            using HttpResponseMessage httpResponse = await _httpClient.PostAsync(PrepareEndpoint(endpoint), httpContent).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            string stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogTrace($"Post {endpoint} received {stringResponse}");
#endif

            Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected async Task<(TR? ResponseOK, TErr? ResponseError)> Post<TP, TR, TErr>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            string serializedRequest = JsonSerializer.Serialize(request, options: JsonSerializerOptions);
            using StringContent httpContent = new(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            _logger.LogTrace($"Post {endpoint} sending {serializedRequest}");
#endif

            using HttpResponseMessage httpResponse = await _httpClient.PostAsync(PrepareEndpoint(endpoint), httpContent).ConfigureAwait(false);

#if DEBUG_API
            string stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogTrace($"Post {endpoint} received {stringResponse}");
#endif

            if (httpResponse.IsSuccessStatusCode)
            {
                Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                return (ResponseOK: await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: JsonSerializerOptions, cancellationToken: cancellationToken), ResponseError: default);
            }
            else
            {
                try
                {
                    Stream streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                    return (ResponseOK: default, ResponseError: await JsonSerializer.DeserializeAsync<TErr>(streamResponse, options: JsonSerializerOptions, cancellationToken: cancellationToken));
                }
                catch (JsonException)
                {
#if DEBUG_API == false
                    var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
#endif
                    _logger.LogError($"Invalid json detected: {stringResponse}");
                    throw new InvalidResponseException(stringResponse);
                }
            }
        }

        private static string PrepareEndpoint(string endpoint)
        {
            return endpoint.StartsWith('/') ? endpoint[1..] : endpoint;
        }
    }
}
