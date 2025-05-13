using Transportadora.Models;

namespace Transportadora.Utils;

public class PedidoUtils
{
    public static string ValidarPedido(Pedido pedido)
    {
        if (pedido == null)
            return "Pedido não pode ser nulo.";

        if (string.IsNullOrWhiteSpace(pedido.NumeroPedido))
            return "Número do pedido não pode ser vazio.";

        if (string.IsNullOrWhiteSpace(pedido.EnderecoEntrega))
            return "Endereço de entrega não pode ser vazio.";

        if (string.IsNullOrWhiteSpace(pedido.Destinatario))
            return "Destinatário não pode ser vazio.";

        if (pedido.Itens == null || pedido.Itens.Count == 0)
            return "Itens do pedido não podem ser vazios.";

        return string.Empty;
    }
}