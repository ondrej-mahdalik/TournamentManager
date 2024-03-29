﻿using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public class SportModel : ModelBase
{
    public SportModel(string name)
    {
        Name = name;
    }
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
    
    public override object Clone()
    {
        var clone = (SportModel)MemberwiseClone();
        clone.Name = Name[..Name.Length];
        return clone;
    }
}