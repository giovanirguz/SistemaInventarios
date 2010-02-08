using AutoMapper;

namespace SistemaInventarios.UI
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<InventariosProfile>());
        }
    }
}