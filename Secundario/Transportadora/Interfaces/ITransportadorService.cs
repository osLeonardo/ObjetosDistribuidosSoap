using System.ServiceModel;
using Transportadora.Models;

namespace Transportadora.Interfaces;

[ServiceContract]
public interface ITransportadorService
{
    [OperationContract]
    Response<string> RegistrarPedido(Pedido pedido);

    [OperationContract]
    Response<PedidoStatus> ConsultarPedido(string numeroPedido);

    [OperationContract]
    Response<List<SituacaoPedido>> ConsultarTodosPedidos();

    [OperationContract]
    Response<SituacaoPedido> ConsultarStatus(string numeroPedido);

    [OperationContract]
    Response<string> AtualizarStatus(string numeroPedido, int novoStatus);
}