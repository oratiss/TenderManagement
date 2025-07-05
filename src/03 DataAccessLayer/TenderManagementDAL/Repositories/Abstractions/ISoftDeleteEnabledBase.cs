using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenderManagementDAL.Repositories.Abstractions
{
    public interface ISoftDeleteEnabledBase
    {
        bool IsDeleted { get; set; }

        DateTime? ModifiedDate { get; set; }

        long? ModifierUserId { get; set; }
    }
}
