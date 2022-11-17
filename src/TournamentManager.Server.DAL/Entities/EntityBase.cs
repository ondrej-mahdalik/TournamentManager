using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Server.DAL.Entities;

public abstract record EntityBase : IEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}