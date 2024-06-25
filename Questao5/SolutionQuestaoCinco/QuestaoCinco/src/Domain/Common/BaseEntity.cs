using System.ComponentModel.DataAnnotations.Schema;

namespace QuestaoCinco.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}
