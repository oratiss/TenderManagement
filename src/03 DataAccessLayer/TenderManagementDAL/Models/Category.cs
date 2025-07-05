using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}