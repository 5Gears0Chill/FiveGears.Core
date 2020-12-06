namespace FiveGears.Core.Interfaces.Patterns
{
    public interface IRepositoryCore
    {
        void SetContext(IDbContext context);
        void DisposeContext();
    }
}
