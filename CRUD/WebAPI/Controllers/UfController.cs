using Data;
using Data.Repositories.Interfaces;
using Domain.Models;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UfController : ApiController
    {
        private readonly string _connectionString;
        private readonly IRepository<Uf> _ufRepository;

        public UfController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            _ufRepository = new UfRepository(_connectionString);
        }

        public async Task<HttpResponseMessage> Get()
        {
            var listaUf = await _ufRepository.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, listaUf);
        }
    }
}
