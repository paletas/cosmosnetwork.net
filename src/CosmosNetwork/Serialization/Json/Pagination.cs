namespace CosmosNetwork.Serialization.Json
{
    public record PaginationFilter(ulong? Limit = null, bool CountTotal = false, PaginationKeyOrOffset? Continuation = null);

    public record PaginationKeyOrOffset(string? Key = null, ulong? Offset = null);

    public record Pagination(string? NextKey, ulong? Total)
    {
        public CosmosNetwork.Pagination ToModel()
        {
            return new CosmosNetwork.Pagination(this.NextKey, this.Total);
        }
    }
}
