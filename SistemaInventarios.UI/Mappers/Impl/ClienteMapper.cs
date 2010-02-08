using SharpArch.Core.PersistenceSupport;
using SistemaInventarios.Model;
using SistemaInventarios.UI.Models;

namespace SistemaInventarios.UI.Mappers.Impl
{
    public class ClienteMapper: AutoFormMapper<Cliente, ClienteForm>, IClienteMapper
    {
        public ClienteMapper(IRepository<Cliente> repository)
            : base(repository)
        {
        }

        protected override int GetIdFromMessage(ClienteForm message)
        {
            return message.Id;
        }

        protected override void MapToModel(ClienteForm message, Cliente model)
        {
            model.NoControl = message.NoControl;
			model.Nombre = message.Nombre;
            model.ApellidoPaterno = message.ApellidoPaterno;
            model.ApellidoMaterno = message.ApellidoMaterno;
            model.Rfc = message.Rfc;
            model.Direccion = message.Direccion;
            model.Telefono = message.Telefono;
        }
    }
}