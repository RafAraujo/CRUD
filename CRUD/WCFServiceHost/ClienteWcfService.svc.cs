using Data;
using Domain.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace WCFServiceHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClienteWcfService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClienteWcfService.svc or ClienteWcfService.svc.cs at the Solution Explorer and start debugging.
    public class ClienteWcfService : IClienteWcfService
    {
        private readonly string _connectionString;

        public ClienteWcfService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        public async Task<IEnumerable<Cliente>> ObterClientes()
        {
            var repository = new ClienteRepository(_connectionString);
            var clientes = await repository.GetAll();
            return clientes;
        }

        public async Task<Cliente> ObterCliente(int id)
        {
            var repository = new ClienteRepository(_connectionString);
            var cliente = await repository.GetById(id);
            return cliente;
        }

        public async Task<Cliente> AdicionarCliente(Cliente cliente)
        {
            var repository = new ClienteRepository(_connectionString);
            return await repository.Insert(cliente);
        }

        public async Task<Cliente> EditarCliente(Cliente cliente)
        {
            var repository = new ClienteRepository(_connectionString);
            return await repository.Update(cliente);
        }

        public async Task ExcluirCliente(int id)
        {
            var repository = new ClienteRepository(_connectionString);
            await repository.Remove(id);
        }
    }
}
