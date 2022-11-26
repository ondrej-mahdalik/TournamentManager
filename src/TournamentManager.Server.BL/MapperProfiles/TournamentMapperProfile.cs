using AutoMapper;
using TournamentManager.Server.DAL.Entities;
using TournamentManager.Common.Models;

namespace TournamentManager.Server.BL.MapperProfiles;

public class TournamentMapperProfile : Profile
{
    public TournamentMapperProfile()
    {
        CreateMap<TournamentEntity, TournamentModel>()
            .MaxDepth(3)
            .ReverseMap()
            .ForMember(x => x.Creator, action => action.Ignore())
            .ForMember(x => x.Matches, action => action.Ignore())
            .ForMember(x => x.Participatings, action => action.Ignore())
            .ForMember(x => x.Sport, action => action.Ignore())
            .ForMember(x => x.WinnerTeam, action => action.Ignore());
    }
}