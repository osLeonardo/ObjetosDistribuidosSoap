using System.Runtime.Serialization;

namespace Transportadora.Models;

[DataContract]
public class Pedido
{
    [DataMember]
    public string NumeroPedido { get; set; }

    [DataMember]
    public string EnderecoEntrega { get; set; }

    [DataMember]
    public string Destinatario { get; set; }

    [DataMember]
    public List<ItemPedido> Itens { get; set; }
}

[DataContract]
public class ItemPedido
{
    [DataMember]
    public string Nome { get; set; }

    [DataMember]
    public int Quantidade { get; set; }
}