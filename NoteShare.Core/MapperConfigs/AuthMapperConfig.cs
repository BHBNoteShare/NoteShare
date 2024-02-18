using AutoMapper;
using NoteShare.Data.Entities;
using NoteShare.Models.Auth;

namespace NoteShare.Core.MapperConfigs
{
    public class AuthMapperConfig : Profile
    {
        public AuthMapperConfig()
        {
            CreateMap<RegisterDto, User>()
            .IncludeAllDerived();
            CreateMap<RegisterDto, Teacher>();
            CreateMap<RegisterDto, Student>();
            CreateMap<RegisterDto, Parent>();
        }
    }
}
