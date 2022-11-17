using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<DAL.Entities.UserEntity, UserModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Tournaments, action => action.Ignore())
            .ForMember(x => x.TeamsAsLeader, action => action.Ignore())
            .ForMember(x => x.TeamsAsMember, action => action.Ignore());
    }
}