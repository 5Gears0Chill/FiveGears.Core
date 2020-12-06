namespace FiveGears.Core.Interfaces.Patterns
{
    public interface IUnitOfWork
    {
        TRepository Get<TRepository>();
    }
}
