using System;

namespace SistemaInventarios.UI.Models
{
    public class ClienteForm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int NoControl { get; set; }
    }
}