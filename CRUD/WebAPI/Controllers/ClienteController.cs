using Data;
using Data.Repositories.Interfaces;
using Domain.Models;
using Domain.Validators;
using FluentValidation;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly string _connectionString;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IValidator<Cliente> _validator;

        public ClienteController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            _clienteRepository = new ClienteRepository(_connectionString);
            _validator = new ClienteValidator();
        }

        public async Task<HttpResponseMessage> Get()
        {
            var clientes = await _clienteRepository.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, clientes);
        }

        public async Task<HttpResponseMessage> Get(int id)
        {
            var cliente = await _clienteRepository.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, cliente);
        }

        public async Task<HttpResponseMessage> Post([FromBody] Cliente cliente)
        {
            var validationResult = _validator.Validate(cliente);

            if (validationResult.IsValid)
            {
                cliente = await _clienteRepository.Insert(cliente);
                return Request.CreateResponse(HttpStatusCode.OK, cliente);
            }
            else
            {
                var message = string.Join(";", validationResult.Errors.Select(vf => vf.ErrorMessage));
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(message));
            }
        }

        public async Task<HttpResponseMessage> Put([FromBody] Cliente cliente)
        {
            var validationResult = _validator.Validate(cliente);

            if (validationResult.IsValid)
            {
                await _clienteRepository.Update(cliente);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                var message = string.Join(";", validationResult.Errors.Select(vf => vf.ErrorMessage));
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(message));
            }
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            await _clienteRepository.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
