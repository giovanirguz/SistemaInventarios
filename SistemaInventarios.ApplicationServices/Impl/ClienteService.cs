using SharpArch.Core.PersistenceSupport;
using SistemaInventarios.Model;

namespace SistemaInventarios.ApplicationServices.Impl
{
    public class ClienteService : IClienteService
    {
        readonly IRepository<Cliente> clienteRepository;

        public ClienteService(IRepository<Cliente> clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public Cliente GetClienteById(int id)
        {
            return clienteRepository.Get(id);
        }

        public void SaveCliente(Cliente cliente)
        {
            clienteRepository.SaveOrUpdate(cliente);
        }
    }
}