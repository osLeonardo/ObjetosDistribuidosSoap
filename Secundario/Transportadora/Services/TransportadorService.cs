using Microsoft.Extensions.Caching.Memory;
using Transportadora.Interfaces;
using Transportadora.Models;
using Transportadora.Utils;

namespace Transportadora.Services;

public class TransportadorService : ITransportadorService
{
    private readonly IMemoryCache _cache;

    public TransportadorService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string RegistrarPedido(Pedido pedido)
    {
        var erro = PedidoUtils.ValidarPedido(pedido);
        if (!string.IsNullOrEmpty(erro))
            return erro;

        var status = "Aguardando coleta";
        var pedidoStatus = new PedidoStatus { Pedido = pedido, Status = status };
        _cache.Set(pedido.NumeroPedido, pedidoStatus);
        return $"Pedido {pedido.NumeroPedido} registrado com status '{status}'.";
    }

    public PedidoStatus ConsultarPedido(string numeroPedido)
    {
        if (_cache.TryGetValue(numeroPedido, out PedidoStatus pedidoStatus))
        {
            return pedidoStatus;
        }
        return null;
    }

    public string ConsultarStatus(string numeroPedido)
    {
        if (_cache.TryGetValue(numeroPedido, out string status))
        {
            return status;
        }
        return "Pedido não encontrado";
    }

    public bool AtualizarStatus(string numeroPedido, string novoStatus)
    {
        if (_cache.TryGetValue(numeroPedido, out _))
        {
            _cache.Set(numeroPedido, novoStatus);
            return true;
        }
        return false;
    }
}