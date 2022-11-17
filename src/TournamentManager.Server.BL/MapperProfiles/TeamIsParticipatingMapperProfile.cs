using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Shared.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class TeamIsParticipatingMapperProfile : Profile
{
    public TeamIsParticipatingMapperProfile()
    {
        CreateMap<TeamIsParticipatingEntity, TeamIsParticipatingModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Team, action => action.Ignore())
            .ForMember(x => x.Tournament, action => action.Ignore());
    }
}