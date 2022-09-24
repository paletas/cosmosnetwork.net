namespace CosmosNetwork
{
    public record PaginationFilter(ulong? Limit = null, bool CountTotal = false, PaginationKeyOrOffset? Continuation = null);

    public record PaginationKeyOrOffset(string? Key = null, ulong? Offset = null);

    public record Pagination(string? NextKey, ulong? Total);
}
