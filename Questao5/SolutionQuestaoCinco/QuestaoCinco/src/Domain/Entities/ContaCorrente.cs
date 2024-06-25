namespace QuestaoCinco.Domain.Entities;

public class ContaCorrente : BaseAuditableEntity
{
    public required int Numero { get; set; }
    public required string Nome { get; set; }
    public required bool Ativo { get; set; }
}
