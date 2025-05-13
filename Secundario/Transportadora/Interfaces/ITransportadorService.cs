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
    List<SituacaoPedido> ConsultarTodosPedidos();

    [OperationContract]
    SituacaoPedido ConsultarStatus(string numeroPedido);

    [OperationContract]
    string AtualizarStatus(string numeroPedido, int novoStatus);
}