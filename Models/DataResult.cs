namespace FiveGears.Core.Models
{
    public class DataResult<TEntity> : NonDataResult
    {
        public TEntity Data { get; private set; }

        public DataResult(int rowsAffected, TEntity data)
            :base(rowsAffected)
        {
            this.Data = data;
        }
    }
}
