using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public abstract record ModelBase : IModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}