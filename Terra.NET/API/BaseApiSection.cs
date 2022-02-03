using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using Terra.NET.Exceptions;

namespace Terra.NET.API
{
    internal class BaseApiSection
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseApiSection> _logger;

        public BaseApiSection(TerraApiOptions options, HttpClient httpClient, ILogger<BaseApiSection> logger)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this._logger = logger;

            this.Options = options;
            this.JsonSerializerOptions = options.JsonSerializerOptions;
        }

        protected JsonSerializerOptions JsonSerializerOptions { get; init; }

        protected TerraApiOptions Options { get; init; }

        protected async Task<T?> Get<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            var httpResponse = await this._httpClient.GetAsync(endpoint, cancellationToken);

            if (httpResponse.IsSuccessStatusCode == false)
            {
                this._logger.LogError($"GET {endpoint}: {httpResponse.StatusCode} - {httpResponse.ReasonPhrase}");
                return default;
            }

            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            this._logger.LogTrace($"Get {endpoint} received {stringResponse}");
#endif

            var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<T>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected async Task<TR?> Post<TP, TR>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            var serializedRequest = JsonSerializer.Serialize<TP>(request, options: this.JsonSerializerOptions);
            using var httpContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            this._logger.LogTrace($"Post {endpoint} sending {serializedRequest}");
#endif

            using var httpResponse = await this._httpClient.PostAsync(endpoint, httpContent).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            this._logger.LogTrace($"Post {endpoint} received {stringResponse}");
#endif

            var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected async Task<(TR? ResponseOK, TErr? ResponseError)> Post<TP, TR, TErr>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            var serializedRequest = JsonSerializer.Serialize<TP>(request, options: this.JsonSerializerOptions);
            using var httpContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            this._logger.LogTrace($"Post {endpoint} sending {serializedRequest}");
#endif

            using var httpResponse = await this._httpClient.PostAsync(endpoint, httpContent).ConfigureAwait(false);

#if DEBUG_API
            var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            this._logger.LogTrace($"Post {endpoint} received {stringResponse}");
#endif

            if (httpResponse.IsSuccessStatusCode)
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                return (ResponseOK: await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken), ResponseError: default);
            }
            else
            {
                try
                {
                    var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                    return (ResponseOK: default, ResponseError: await JsonSerializer.DeserializeAsync<TErr>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken));
                }
                catch (JsonException)
                {
#if DEBUG_API == false
                    var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
#endif
                    this._logger.LogError($"Invalid json detected: {stringResponse}");
                    throw new InvalidResponseException(stringResponse);
                }
            }
        }
    }
}
