using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public class SportModel : ModelBase
{
    public SportModel(string name)
    {
        this.Name = name;
    }
    public string Name { get; set; }
    
    public IList<TournamentModel> Tournaments { get; set; } = new List<TournamentModel>();
}