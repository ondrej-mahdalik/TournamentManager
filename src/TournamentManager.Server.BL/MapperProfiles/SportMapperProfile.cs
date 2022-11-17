using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class SportMapperProfile : Profile
{
    public SportMapperProfile()
    {
        CreateMap<SportEntity, SportModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Tournaments, action => action.Ignore());
    }
}