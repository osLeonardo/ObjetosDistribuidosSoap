using Transportadora.Enums;

namespace Transportadora.Models;

public class SituacaoPedido
{
    public string NumeroPedido { get; set; }
    public StatusEnum Status { get; set; }
}