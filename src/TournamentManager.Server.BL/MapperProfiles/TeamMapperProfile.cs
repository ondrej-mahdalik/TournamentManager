using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class TeamMapperProfile : Profile
{
    public TeamMapperProfile()
    {
        CreateMap<TeamEntity, TeamModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Matches, action => action.Ignore())
            .ForMember(x => x.Members, action => action.Ignore())
            .ForMember(x => x.Participatings, action => action.Ignore())
            .ForMember(x => x.Leader, action => action.Ignore());
    }
}