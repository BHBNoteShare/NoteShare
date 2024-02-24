using AutoMapper;
using NoteShare.Data.Entities;
using NoteShare.Models.Auth;
using NoteShare.Models.School;
using NoteShare.Models.StudentPreferences;
using NoteShare.Models.Subject;

namespace NoteShare.Core.MapperConfigs
{
    public class StudentPreferenceMapperConfig : Profile
    {
        public StudentPreferenceMapperConfig()
        {
            CreateMap<SubjectDto, Subject>().ReverseMap();
            CreateMap<SchoolDto, School>().ReverseMap();
            CreateMap<StudentPreferenceDto, StudentPreference>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.SubjectLevel))
                .ReverseMap()
                .ForMember(dest => dest.SubjectLevel, opt => opt.MapFrom(src => src.Level));
        }
    }
}
