using FiveGears.Core.Models;
using System.Collections.Generic;

namespace FiveGears.Core.Auditing
{
    public interface IAuditManager
    {
        TEntity SetUpdateAudit<TEntity>(TEntity entity) where TEntity : BaseEntity;
        TEntity SetNewAudit<TEntity>(TEntity entity) where TEntity : BaseEntity;
        IEnumerable<TEntity> SetAuditList<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
    }
}
