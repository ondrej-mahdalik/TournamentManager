using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class UserIsInTeamMapperProfile : Profile
{
    public UserIsInTeamMapperProfile()
    {
        CreateMap<UserIsInTeamEntity, UserIsInTeamModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Team, action => action.Ignore())
            .ForMember(x => x.User, action => action.Ignore());
    }
}