using Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WCFServiceHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClienteWcfService" in both code and config file together.
    [ServiceContract]
    public interface IClienteWcfService
    {
        [OperationContract]
        Task<IEnumerable<Cliente>> ObterClientes();

        [OperationContract]
        Task<Cliente> ObterCliente(int id);

        [OperationContract]
        Task<Cliente> AdicionarCliente(Cliente cliente);

        [OperationContract]
        Task<Cliente> EditarCliente(Cliente cliente);

        [OperationContract]
        Task ExcluirCliente(int id);
    }
}
