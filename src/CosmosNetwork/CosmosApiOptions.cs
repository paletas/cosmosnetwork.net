﻿using CosmosNetwork.Modules.Auth.Serialization.Json;
using CosmosNetwork.Serialization.Json;
using CosmosNetwork.Serialization.Json.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork
{
    public class CosmosApiOptions
    {
        public CosmosApiOptions(ulong? minimumAvailableBlockHeight = default)
        {
            if (minimumAvailableBlockHeight.HasValue)
            {
                this.MinimumAvailableBlockHeight = minimumAvailableBlockHeight.Value;
            }

            this.SkipHttpClientConfiguration = false;
        }

        public string? HttpClientName { get; set; }

        public bool SkipHttpClientConfiguration { get; set; }

        internal NetworkOptions? Network { get; set; }

        internal CosmosMessageRegistry? MessageRegistry { get; set; }

        public ulong MinimumAvailableBlockHeight { get; set; } = 1;

        public CoinDecimal[]? GasPrices { get; set; } = null;

        public decimal GasAdjustment { get; set; } = 1.75M;

        private JsonSerializerOptions? _jsonSerializerOptions;
        public JsonSerializerOptions JsonSerializerOptions => this._jsonSerializerOptions ??= CreateSerializerOptions(this.MessageRegistry);

        public static JsonSerializerOptions DefaultSerializerOptions => CreateSerializerOptions();

        private static JsonSerializerOptions CreateSerializerOptions(CosmosMessageRegistry? messageRegistry = null)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            };

            options.Converters.Add(new JsonStringEnumMemberConverter());
            options.Converters.Add(new Uint128Converter());
            options.Converters.Add(new Int128Converter());
            options.Converters.Add(new BlockFlagConverter());
            options.Converters.Add(new DurationConverter());
            options.Converters.Add(new TimeSpanConverter());
            options.Converters.Add(new TimestampConverter());
            options.Converters.Add(new MessagesConverter(messageRegistry ?? throw new InvalidOperationException()));
            options.Converters.Add(new AccountConverter());

            return options;
        }
    }
}
