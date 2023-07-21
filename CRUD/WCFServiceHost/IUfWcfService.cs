using Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WCFServiceHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUfWcfService" in both code and config file together.
    [ServiceContract]
    public interface IUfWcfService
    {
        [OperationContract]
        Task<IEnumerable<Uf>> ObterListaUf();
    }
}
