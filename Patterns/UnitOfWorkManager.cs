using FiveGears.Core.Helpers;
using FiveGears.Core.Interfaces.Configuration;
using FiveGears.Core.Interfaces.Patterns;
using FiveGears.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiveGears.Core.Patterns
{
    public class UnitOfWorkManager<TContext> : IUnitOfWorkManager where TContext : IDbContext
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkManager(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = unitOfWork;
        }

        public TContext CreateContextInstance()
        {
            return ObjectCreator.NewInstance<TContext>(GetAppSettings());
        }

        private IAppSettings GetAppSettings()
        {
            return (IAppSettings)_serviceProvider.GetService(typeof(IAppSettings));
        }

        public async Task<TResult> ExecuteAsyncComplete<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery)
        {
            TResult result;
            result = await runQuery(_unitOfWork);

            return result;
        }

        public async Task<TResult> ExecuteMultiCallAsync<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery)
        {
            TResult result;
            result = await runQuery(_unitOfWork);

            return result;
        }

        public async Task<TResult> ExecuteSingleSaveAsync<TRepository, TResult>(Func<TRepository, Task<TResult>> runQuery)
            where TResult : BaseEntity
            where TRepository : IRepositoryCore
        {
            TResult result;
            var repository = _unitOfWork.Get<TRepository>();
            var baseRepository = repository as Repository<TContext>;
            repository.SetContext(CreateContextInstance());
            result = await runQuery(repository);
            await baseRepository.SaveAsync(result);
            repository.DisposeContext();

            return result;
        }

        public async Task<TResult> ExecuteReadAsync<TResult>(Func<IUnitOfWork, Task<TResult>> runQuery)
        {
            TResult result;
            result = await runQuery(_unitOfWork);

            return result;
        }

        public async Task<TResult> ExecuteSingleAsync<TRepository, TResult>(Func<TRepository, Task<TResult>> runQuery)
             where TRepository : IRepositoryCore
        {
            TResult result;
            var repository = _unitOfWork.Get<TRepository>();
            result = await WrappedContext(repository, runQuery);

            return result;
        }

        public async Task<IEnumerable<TResult>> ExecuteListAsync<TRepository, TResult>(Func<TRepository, Task<IEnumerable<TResult>>> runQuery)
            where TRepository : IRepositoryCore
        {
            IEnumerable<TResult> result;
            var repository = _unitOfWork.Get<TRepository>();
            result = await WrappedContext(repository, runQuery);

            return result;
        }

        private async Task<TResult> WrappedContext<TRepository, TResult>(TRepository repository, Func<TRepository, Task<TResult>> runQuery)
              where TRepository : IRepositoryCore
        {
            TResult result = default;
            using (var context = CreateContextInstance())
            {
                repository.SetContext(CreateContextInstance());
                result = await runQuery(repository);
            }
            return result;
        }
    }
}
