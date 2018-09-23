
using AutoMapper;
using NetDocs.Model.Entities;
using NetDocs.UI.ViewModels;
namespace NetDocs.UI.AutoMapper
{
    public class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile()
            : base("ViewModelToModelMappings")
        {
                
        }

        protected override void Configure() 
        {
            Mapper.CreateMap<Celula, CelulaViewModel>();
            Mapper.CreateMap<Setor, SetorViewModel>();
        }
    }
}
