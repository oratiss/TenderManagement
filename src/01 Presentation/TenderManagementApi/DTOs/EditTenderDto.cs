namespace TenderManagementApi.DTOs;

public class EditTenderDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public int CategoryId { get; set; }
    public int StatusId { get; set; }
}