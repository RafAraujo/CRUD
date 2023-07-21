using Data;
using Domain.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace WCFServiceHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UfWcfService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UfWcfService.svc or UfWcfService.svc.cs at the Solution Explorer and start debugging.
    public class UfWcfService : IUfWcfService
    {
        private readonly string _connectionString;

        public UfWcfService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        public async Task<IEnumerable<Uf>> ObterListaUf()
        {
            var repository = new UfRepository(_connectionString);
            var listaUf = await repository.GetAll();
            return listaUf;
        }
    }
}
