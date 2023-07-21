using Data.Repositories;
using Data.Utils;
using Domain.Models;

namespace Data
{
    public class EnderecoRepository : BaseRepository<Endereco>
    {
        protected override ISqlGenerator<Endereco> SqlGenerator { get; set; }

        public EnderecoRepository(string connectionString) : base(connectionString)
        {
            SqlGenerator = new SqlGenerator<Endereco>(nameof(Endereco));
        }
    }
}
