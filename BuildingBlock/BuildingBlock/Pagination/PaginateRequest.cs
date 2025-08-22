namespace BuildingBlock.Pagination
{
     /// <summary>
     /// Represents a request for a paginated list of data.
     /// </summary>
     /// <remarks>
     /// This record is commonly used to encapsulate pagination parameters such as page number,
     /// page size, search text, and sorting details. All parameters have sensible defaults
     /// to make paging optional and flexible.
     /// </remarks>
     public record PaginateRequest(
          /// <summary>
          /// The index of the page to retrieve. Defaults to 0 (first page).
          /// </summary>
          int PageIndex = 0,
          /// <summary>
          /// The number of items per page. Defaults to 10.
          /// </summary>
          int PageSize = 10,
          /// <summary>
          /// Optional search string to filter results. Defaults to null (no search).
          /// </summary>
          string? Search = null,
          /// <summary>
          /// Optional column name to sort results by. Defaults to null (no specific sort column).
          /// </summary>
          string? SortColumn = null,
          /// <summary>
          /// Sort order direction, e.g., "asc" or "desc". Defaults to "asc".
          /// </summary>
          string SortOrder = "asc");
}
