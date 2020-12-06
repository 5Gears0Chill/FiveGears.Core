using System;

namespace FiveGears.Core.Models
{
    public abstract partial class AuditBaseEntity : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
       
    }
}
