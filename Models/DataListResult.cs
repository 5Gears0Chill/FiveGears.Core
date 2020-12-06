using System.Collections.Generic;
using System.Linq;

namespace FiveGears.Core.Models
{
    public class DataListResult<TEntity> : DataResult<IEnumerable<TEntity>>
    {
        public int TotalRecords { get; }

        public DataListResult(int rowsAffected, IEnumerable<TEntity> data)
            :base(rowsAffected, data)
        {
            TotalRecords = data.Count();
        }
    }
}
