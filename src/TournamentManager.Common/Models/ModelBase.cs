using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public abstract class ModelBase : IModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}