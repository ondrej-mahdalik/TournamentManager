using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class MatchMapperProfile : Profile
{
    public MatchMapperProfile()
    {
        CreateMap<MatchEntity, MatchModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Team1, action => action.Ignore())
            .ForMember(x => x.Team2, action => action.Ignore())
            .ForMember(x => x.Tournament, action => action.Ignore());
    }
}