namespace FiveGears.Core.Models
{
    public abstract partial class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
