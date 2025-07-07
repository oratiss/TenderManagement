using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Models
{
    public class Category: IEntity, ISoftDeleteEnabled
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifierUserId { get; set; }
    }
}