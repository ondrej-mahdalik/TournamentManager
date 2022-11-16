﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TournamentManager.Shared.Models;

public record MatchModel(
    int Score1,
    int Score2,
    int Round,
    int Order) : ModelBase
{
    public Guid TournamentId { get; set; }
    public TournamentModel? Tournament { get; set; }
    
    public Guid? Team1Id { get; set; }
    public TeamModel? Team1 { get; set; }
    
    public Guid? Team2Id { get; set; }
    public TeamModel? Team2 { get; set; }
}