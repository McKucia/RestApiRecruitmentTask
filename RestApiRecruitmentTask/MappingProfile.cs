using AutoMapper;
using RestApiRecruitmentTask.Api.ViewModels;
using RestApiRecruitmentTask.Core.Models;

namespace MyWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tire, TireViewModel>()
                .ReverseMap() // from view model to tire and from tire to view model
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // ignore Id in mapping

            CreateMap<Producer, ProducerViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}