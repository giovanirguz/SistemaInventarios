using SharpArch.Core.DomainModel;

namespace SistemaInventarios.UI.Mappers
{
    public interface IMapper<TModel, TForm> where TModel : Entity, new()
    {
        TForm Map(TModel model);
        TModel Map(TForm form);
        TForm[] Map(TModel[] model);
    }
}