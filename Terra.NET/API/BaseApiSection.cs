using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Terra.NET.API.Serialization.Json;

namespace Terra.NET.API
{
    internal class BaseApiSection
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseApiSection> _logger;

        public BaseApiSection(TerraApiOptions options, HttpClient httpClient, ILogger<BaseApiSection> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;

            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };

            JsonSerializerOptions.Converters.Add(new BigIntegerConverter());
            JsonSerializerOptions.Converters.Add(new SignerModeConverter());

            Options = options;
        }

        protected JsonSerializerOptions JsonSerializerOptions { get; init; }

        protected TerraApiOptions Options { get; init; }

        protected async Task<T?> Get<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            var httpResponse = await _httpClient.GetAsync(endpoint, cancellationToken);

            if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                return default;

            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation($"Get {endpoint} received {stringResponse}");
#endif

            var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<T>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected async Task<TR?> Post<TP, TR>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            var serializedRequest = JsonSerializer.Serialize<TP>(request, options: this.JsonSerializerOptions);
            using var httpContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            _logger.LogInformation($"Post {endpoint} sending {serializedRequest}");
#endif

            using var httpResponse = await _httpClient.PostAsync(endpoint, httpContent).ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();

#if DEBUG_API
            var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation($"Get {endpoint} received {stringResponse}");
#endif

            var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken);
        }

        protected async Task<(TR? ResponseOK, TErr? ResponseError)> Post<TP, TR, TErr>(string endpoint, TP request, CancellationToken cancellationToken = default)
        {
            var serializedRequest = JsonSerializer.Serialize<TP>(request, options: this.JsonSerializerOptions);
            using var httpContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

#if DEBUG_API
            _logger.LogInformation($"Post {endpoint} sending {serializedRequest}");
#endif

            using var httpResponse = await _httpClient.PostAsync(endpoint, httpContent).ConfigureAwait(false);
            
#if DEBUG_API
            var stringResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation($"Get {endpoint} received {stringResponse}");
#endif

            if (httpResponse.IsSuccessStatusCode)
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                return (ResponseOK: await JsonSerializer.DeserializeAsync<TR>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken), ResponseError: default);
            }
            else
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
                return (ResponseOK: default, ResponseError: await JsonSerializer.DeserializeAsync<TErr>(streamResponse, options: this.JsonSerializerOptions, cancellationToken: cancellationToken));
            }
        }
    }
}
