namespace BuildingBlock.Pagination
{  
    public record PaginateRequest(int PageIndex = 0, int PageSize = 10);

}
