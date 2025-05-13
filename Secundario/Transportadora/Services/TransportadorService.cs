using Microsoft.Extensions.Caching.Memory;
using Transportadora.Enums;
using Transportadora.Interfaces;
using Transportadora.Models;
using Transportadora.Utils;

namespace Transportadora.Services;

public class TransportadorService : ITransportadorService
{
    private readonly IMemoryCache _cache;
    private readonly List<string> _pedidosKeys = new();
    private readonly StatusEnum _defaultStatus = StatusEnum.AguardandoColeta;

    public TransportadorService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string RegistrarPedido(Pedido pedido)
    {
        try
        {
            var erro = PedidoUtils.ValidarPedido(pedido);
            if (!string.IsNullOrEmpty(erro))
                return erro;

            var pedidoStatus = new PedidoStatus
            {
                Pedido = pedido,
                Status = _defaultStatus
            };
            _cache.Set(pedido.NumeroPedido, pedidoStatus);
            _pedidosKeys.Add(pedido.NumeroPedido);

            return $"Pedido { pedido.NumeroPedido } registrado com status '{ _defaultStatus }'.";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao registrar pedido: { ex.Message }");
        }
    }

    public PedidoStatus ConsultarPedido(string numeroPedido)
    {
        try
        {
            if (_cache.TryGetValue(numeroPedido, out PedidoStatus pedidoStatus))
            {
                return pedidoStatus;
            }

            throw new InvalidOperationException($"Pedido { numeroPedido } não encontrado.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao consultar pedido { numeroPedido }: { ex.Message }.");
        }
    }

    public List<SituacaoPedido> ConsultarTodosPedidos()
    {
        try
        {
            var todosPedidos = new List<SituacaoPedido>();
            foreach (var key in _pedidosKeys)
            {
                if (_cache.TryGetValue(key, out PedidoStatus pedidoStatus))
                {
                    todosPedidos.Add(new SituacaoPedido
                    {
                        NumeroPedido = pedidoStatus.Pedido.NumeroPedido,
                        Status = pedidoStatus.Status
                    });
                }
            }

            return todosPedidos;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao consultar todos os pedidos: { ex.Message }.");
        }
    }

    public SituacaoPedido ConsultarStatus(string numeroPedido)
    {
        try
        {
            if (_cache.TryGetValue(numeroPedido, out StatusEnum status))
            {
                return new SituacaoPedido
                {
                    NumeroPedido = numeroPedido,
                    Status = status
                };
            }
            throw new InvalidOperationException($"Pedido { numeroPedido } não encontrado.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao consultar status do pedido { numeroPedido }: { ex.Message }.");
        }
    }

    public string AtualizarStatus(string numeroPedido, int novoStatus)
    {
        try
        {
            if (_cache.TryGetValue(numeroPedido, out _))
            {
                var status = (StatusEnum)novoStatus;
                var situacao = new SituacaoPedido
                {
                    NumeroPedido = numeroPedido,
                    Status = status
                };

                _cache.Set(numeroPedido, situacao);
                return $"Status do pedido { numeroPedido } atualizado para '{ status }'.";
            }
            return $"Pedido { numeroPedido } não encontrado.";
        }
        catch (Exception ex)
        {
            return $"Erro ao atualizar status do pedido: { ex.Message }";
        }
    }
}