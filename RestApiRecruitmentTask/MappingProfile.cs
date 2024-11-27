using AutoMapper;
using RestApiRecruitmentTask.Api.ViewModels;
using RestApiRecruitmentTask.Core.Models;

namespace MyWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tire, TireViewModel>().ReverseMap();
            CreateMap<Producer, ProducerViewModel>().ReverseMap();
        }
    }
}