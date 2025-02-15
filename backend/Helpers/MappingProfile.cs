using AutoMapper;
using Mappa.Entities;
using Mappa.Dtos;
using System.Threading.Tasks.Dataflow;

namespace Mappa.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityBaseDto>();
        CreateMap<Ethnicity, EthnicityDto>();
        CreateMap<Gender, GenderDto>();
        CreateMap<Genre, GenreDto>();
        CreateMap<Language, LanguageDto>();
        CreateMap<OrdinaryPerson, OrdinaryPersonBaseDto>();
        CreateMap<Profession, ProfessionDto>();
        CreateMap<Religion, ReligionDto>();
        CreateMap<SecondarySource, SecondarySourceBaseDto>();
        CreateMap<Entities.Type, TypeDto>();
        CreateMap<UnordinaryPerson, UnordinaryPersonBaseDto>();
        CreateMap<WrittenSource, WrittenSourceBaseDto>();
        // CreateMap<WrittenSource, WrittenSourceGeneralDto>()
        //     .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre != null ? new GenreDto { Id = src.Genre.Id, Name = src.Genre.Name } : null))
        //     .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language != null ? new LanguageDto { Id = src.Language.Id, Name = src.Language.Name } : null));

        // CreateMap<WrittenSource, WrittenSourceDetailDto>()
        //     .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre != null ? new GenreDto { Id = src.Genre.Id, Name = src.Genre.Name } : null))
        //     .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language != null ? new LanguageDto { Id = src.Language.Id, Name = src.Language.Name } : null))
        //     .ForMember(dest => dest.TranslatedLanguages, opt => opt.MapFrom(src => src.TranslatedLanguages.Select(tl => new LanguageDto { Id = tl.Id, Name = tl.Name }).ToList()))
        //     .ForMember(dest => dest.CitiesMentioningTheSources, opt => opt.MapFrom(src => src.CitiesMentioningTheSources.Select(cmts => new CityBaseDto { Id = cmts.Id, Name = cmts.Name }).ToList()))
        //     .ForMember(dest => dest.CitiesWhereSourcesAreWritten, opt => opt.MapFrom(src => src.CitiesWhereSourcesAreWritten.Select(cwsaw => new CityBaseDto { Id = cwsaw.Id, Name = cwsaw.Name }).ToList()));

        // CreateMap<WrittenSourceCreateRequest, WrittenSource>();
        // CreateMap<WrittenSourceUpdateRequest, WrittenSource>();
        
    }
}
