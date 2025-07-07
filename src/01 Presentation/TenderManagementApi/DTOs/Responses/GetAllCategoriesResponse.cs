namespace TenderManagementApi.DTOs.Responses
{
    public class GetAllCategoriesResponse
    {
        public List<GetCategoryResponse>? Categories { get; set; } = new();
    }

    public class GetCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
