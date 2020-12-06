namespace FiveGears.Core.Interfaces.Patterns
{
    public interface IRepositoryResolver
    {
        TRepository Resolve<TRepository>();
    }
}
