namespace QuestaoCinco.Domain.Entities;

public class Movimento : BaseAuditableEntity
{
    public required ContaCorrente ContaCorrente { get; set; }
    public DateTime DataMovimento { get; set; }
    public required string TipoMovimento { get; set; }
    public required decimal Valor { get; set; }
}
