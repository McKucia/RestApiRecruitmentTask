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
                .ReverseMap(); // from view model to tire and from tire to view model

            CreateMap<Producer, ProducerViewModel>()
                .ReverseMap();
        }
    }
}