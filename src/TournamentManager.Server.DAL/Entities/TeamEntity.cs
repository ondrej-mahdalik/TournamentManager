﻿namespace TournamentManager.Server.DAL.Entities;

public record TeamEntity(string Name,
    string? LogoUrl, bool IsPersonal) : EntityBase
{
    public Guid? LeaderId { get; init; }
    public UserEntity? Leader { get; init; }
    
    public IList<UserIsInTeamEntity> Members { get; init; } = new List<UserIsInTeamEntity>();
    public IList<MatchEntity> Matches { get; init; } = new List<MatchEntity>();
    public IList<TeamIsParticipatingEntity> Participatings { get; init; } = new List<TeamIsParticipatingEntity>();
    public IList<TournamentEntity> WinnedTournaments { get; init; } = new List<TournamentEntity>();
}