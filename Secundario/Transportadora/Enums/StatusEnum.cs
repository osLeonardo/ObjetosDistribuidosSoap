using System.ComponentModel;

namespace Transportadora.Enums;

public enum StatusEnum
{
	[Description("Pedido não encontrado")]
    PedidoNaoEncontrado = 0,

	[Description("Aguardando coleta")]
	AguardandoColeta = 1,

	[Description("Em transporte")]
    EmTransporte = 2,

	[Description("Entregue")]
    Entregue = 3,

	[Description("Falha na entrega")]
    FalhaNaEntrega = 4,
}