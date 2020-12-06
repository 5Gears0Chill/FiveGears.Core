using FiveGears.Core.Interfaces.Patterns;
using System;

namespace FiveGears.Core.Patterns
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext: IDbContext
    {
        private readonly IRepositoryResolver _repositoryResolver;

        public UnitOfWork(IRepositoryResolver repositoryResolver)
        {
            _repositoryResolver = repositoryResolver;
        }

        public TRepository Get<TRepository>()
        {
            var repository = _repositoryResolver.Resolve<TRepository>();
            if(repository == null)
            {
                throw new ArgumentNullException($"{typeof(TRepository).Name} could not be injected by the IServiceProvider.");
            }
            return repository;
        }
    }
}
