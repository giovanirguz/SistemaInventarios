using System.Collections;
using SistemaInventarios.Model;

namespace SistemaInventarios.ApplicationServices
{
    public interface IClienteService
    {
        Cliente GetClienteById(int id);
        void SaveCliente(Cliente cliente);
        Cliente[] GetClientesByName(string name);
        Cliente GetClienteByNoControl(int noControl);
    }
}
