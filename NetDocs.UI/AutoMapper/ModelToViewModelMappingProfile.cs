using AutoMapper;
using NetDocs.Model.Entities;
using NetDocs.UI.ViewModels;

namespace NetDocs.UI.AutoMapper
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
            : base("ModelToViewModelMappings")
        {
                
        }

        protected override void Configure() 
        {
            Mapper.CreateMap<CelulaViewModel, Celula>();
            Mapper.CreateMap<SetorViewModel, Setor>();
        }
    }
}
