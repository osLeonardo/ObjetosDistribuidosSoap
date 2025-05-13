using System.ServiceModel;
using Transportadora.Models;

namespace Transportadora.Interfaces;

[ServiceContract]
public interface ITransportadorService
{
    [OperationContract]
    string RegistrarPedido(Pedido pedido);

    [OperationContract]
    PedidoStatus ConsultarPedido(string numeroPedido);

    [OperationContract]
    string ConsultarStatus(string numeroPedido);

    [OperationContract]
    bool AtualizarStatus(string numeroPedido, string novoStatus);
}