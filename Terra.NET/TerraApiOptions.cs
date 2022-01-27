﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Terra.NET.API.Serialization.Json;

namespace Terra.NET
{
    public class TerraApiOptions
    {
        public TerraApiOptions()
        {
            this.JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };

            this.JsonSerializerOptions.Converters.Add(new BigIntegerConverter());
            this.JsonSerializerOptions.Converters.Add(new SignerModeConverter());
        }

        public TerraApiOptions(int? throttlingEnumeratorsInSeconds = default, long? startingBlockHeightForTransactionSearch = default)
            : this()
        {
            if (throttlingEnumeratorsInSeconds != default)
                this.ThrottlingEnumeratorsInMilliseconds = throttlingEnumeratorsInSeconds;

            if (startingBlockHeightForTransactionSearch.HasValue)
                this.StartingBlockHeightForTransactionSearch = startingBlockHeightForTransactionSearch.Value;
        }

        public string ChainId { get; set; } = "columbus-5";

        public int? ThrottlingEnumeratorsInMilliseconds { get; set; } = 1000;

        public long StartingBlockHeightForTransactionSearch { get; set; } = 1;

        public CoinDecimal[]? GasPrices { get; set; } = null;

        public decimal GasAdjustment { get; set; } = 1.75M;

        public string[]? DefaultDenoms { get; set; } = new[] { "uusd" };

        public JsonSerializerOptions JsonSerializerOptions { get; init; } = null!;
    }
}
