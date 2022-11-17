using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class UserMapperProfile : Profile
{
    protected UserMapperProfile()
    {
        CreateMap<DAL.Entities.UserEntity, UserModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Tournaments, action => action.Ignore())
            .ForMember(x => x.TeamsAsLeader, action => action.Ignore())
            .ForMember(x => x.TeamsAsMember, action => action.Ignore());
    }
}