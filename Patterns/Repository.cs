using FiveGears.Core.Auditing;
using FiveGears.Core.Interfaces.Patterns;
using FiveGears.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiveGears.Core.Patterns
{
    public partial class Repository<TContext> : IRepositoryCore
        where TContext : IDbContext
    {
        private readonly IAuditManager _auditManager;
        public Repository(IAuditManager auditManager)
        {
            _auditManager = auditManager;
        }

        protected TContext Context;

        public void SetContext(IDbContext context)
        {
            Context = (TContext)context;
        }

        public void DisposeContext()
        {
            Context.Dispose();
        }

        public async Task<DataResult<TEntity>> SaveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            int rowsAffected;
            if (entity.Id > 0)
            {
                entity = _auditManager.SetUpdateAudit(entity);
                rowsAffected = await Context.UpdateAsync(entity);
            }
            else
            {
                entity = _auditManager.SetNewAudit(entity);
                rowsAffected = await Context.InsertAsync(entity);
            }
            return new DataResult<TEntity>(rowsAffected, entity);
        }

        public async Task<DataListResult<TEntity>> InsertListAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            entities = _auditManager.SetAuditList(entities);
            var rowsAffected = await Context.InsertEnumerableAsync(entities);
            return new DataListResult<TEntity>(rowsAffected, entities);
        }

        public async Task<DataListResult<TEntity>> UpdateListAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            entities = _auditManager.SetAuditList(entities);
            var rowsAffected = await Context.UpdateEnumerableAsync(entities);
            return new DataListResult<TEntity>(rowsAffected, entities);
        }

        public async Task<DataResult<TEntity>> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var rowsAffected = await Context.DeleteAsync(entity);
            return new DataResult<TEntity>(rowsAffected, entity);
        }

        public async Task<DataListResult<TEntity>> DeleteListAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            var rowsAffected = await Context.DeleteEnumerableAsync(entities);
            return new DataListResult<TEntity>(rowsAffected, entities);
        }
    }
}
