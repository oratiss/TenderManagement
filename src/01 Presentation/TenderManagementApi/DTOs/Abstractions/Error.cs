namespace TenderManagementApi.DTOs.Abstractions;

public class Error
{
    public short? Code { get; set; }

    public string ErrorMessage { get; set; } = null!;
}