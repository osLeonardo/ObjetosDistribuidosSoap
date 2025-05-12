using Microsoft.Extensions.Caching.Memory;
using Transportadora.Interfaces;
using Transportadora.Models;

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
        var status = "Aguardando coleta";
        _cache.Set(pedido.NumeroPedido, status);
        return $"Pedido {pedido.NumeroPedido} registrado com status '{status}'.";
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