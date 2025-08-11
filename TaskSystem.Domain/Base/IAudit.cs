using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSystem.Domain.Base
{
    public interface IAudit
    {
        string InsertedUser { get; set; }

        DateTime? InsertedDate { get; set; }

        string UpdatedUser { get; set; }

        DateTime? UpdatedDate { get; set; }

        string DeletedUser { get; set; }

        DateTime? DeletedDate { get; set; }

        bool IsDeleted { get; set; }
    }
}
