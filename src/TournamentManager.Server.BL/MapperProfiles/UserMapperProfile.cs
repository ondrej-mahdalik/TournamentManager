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
            .ForMember(x => x.FirstName, action => action.Ignore())
            .ForMember(x => x.LastName, action => action.Ignore())
            .ForMember(x => x.Email, action => action.Ignore())
            .ReverseMap()
            .ForMember(x => x.Tournaments, action => action.Ignore())
            .ForMember(x => x.TeamsAsLeader, action => action.Ignore())
            .ForMember(x => x.TeamsAsMember, action => action.Ignore());
    }
}