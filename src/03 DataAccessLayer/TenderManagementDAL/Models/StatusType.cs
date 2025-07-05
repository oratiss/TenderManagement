using TenderManagementDAL.Models.Abstarctions;

namespace TenderManagementDAL.Models
{
    public sealed record StatusType : StringEnum<StatusType>
    {
        private StatusType(string value) : base(value) { }

        // For Tender
        public static readonly StatusType Open = new("Open");
        public static readonly StatusType Closed = new("Closed");
        public static readonly StatusType Pending = new("Pending");

        // For Bid
        public static readonly StatusType Approved = new("Approved");
        public static readonly StatusType Rejected = new("Rejected");
    }
}
