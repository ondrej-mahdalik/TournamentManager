using System.ComponentModel.DataAnnotations;

namespace TournamentManager.Common.Models;

public abstract class ModelBase : IModel, ICloneable
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public object Clone()
    {
        return MemberwiseClone();
    }
}