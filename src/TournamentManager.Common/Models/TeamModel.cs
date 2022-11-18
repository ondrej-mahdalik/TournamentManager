﻿using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public record TeamModel(string Name,
    string? LogoUrl,
    Guid LeaderId) : ModelBase
{
    public string Name { get; set; } = Name;
    public string? LogoUrl { get; set; } = LogoUrl;
    
    public Guid LeaderId { get; set; } = LeaderId;
    public UserModel? Leader { get; set; }
    
    public IList<UserIsInTeamModel> Members { get; set; } = new List<UserIsInTeamModel>();
    public IList<MatchModel> Matches { get; set; } = new List<MatchModel>();
    public IList<TeamIsParticipatingModel> Participatings { get; set; } = new List<TeamIsParticipatingModel>();
}