using Data.Repositories;
using Data.Utils;
using Domain.Models;

namespace Data
{
    public class UfRepository : BaseRepository<Uf>
    {
        protected override ISqlGenerator<Uf> SqlGenerator { get; set; }

        public UfRepository(string connectionString) : base(connectionString)
        {
            SqlGenerator = new SqlGenerator<Uf>("UF");
        }
    }
}
