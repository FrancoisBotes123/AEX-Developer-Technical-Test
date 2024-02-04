using Api.Models.CSV;
using AutoMapper;
using TaskLoggerApi.Models.User;

namespace TaskLoggerApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, RegisterUserDTO>();
            CreateMap<RegisterUserDTO, AppUser>();

            CreateMap<AppUser, UserReturnDTO>();
            CreateMap<UserReturnDTO, AppUser>();

            CreateMap<AppUser, UserLoginDTO>();
            CreateMap<UserLoginDTO, AppUser>();

            CreateMap<AppUser, UpdateUserDTO>();
            CreateMap<UpdateUserDTO, AppUser>();

            CreateMap<CsvFile, CsvFileDto>();
            CreateMap<CsvFileDto, CsvFile>();
        }
    }
}
