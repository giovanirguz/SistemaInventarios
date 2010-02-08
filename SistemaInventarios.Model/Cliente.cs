using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace SistemaInventarios.Model
{
    public class Cliente : Entity
    {
        public virtual int NoControl { get; set; }

        [NotNullNotEmpty]
        public virtual string Nombre { get; set; }

        public virtual string ApellidoPaterno { get; set; }

        public virtual string ApellidoMaterno { get; set; }

        public virtual string Rfc { get; set; }

        public virtual string Direccion { get; set; }

        public virtual string Telefono { get; set; }
    }
}
