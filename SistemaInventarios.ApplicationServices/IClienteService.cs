using SistemaInventarios.Model;

namespace SistemaInventarios.ApplicationServices
{
    public interface IClienteService
    {
        Cliente GetClienteById(int id);
        void SaveCliente(Cliente cliente);
    }
}
