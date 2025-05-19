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

    public Response<string> RegistrarPedido(Pedido pedido)
    {
        try
        {
            var erro = PedidoUtils.ValidarPedido(pedido);
            if (!string.IsNullOrEmpty(erro))
            {
                return new Response<string>
                {
                    Success = false,
                    Message = erro
                };
            }

            var pedidoStatus = new PedidoStatus
            {
                Pedido = pedido,
                Status = _defaultStatus
            };
            _cache.Set(pedido.NumeroPedido, pedidoStatus);
            _pedidosKeys.Add(pedido.NumeroPedido);

            var response = $"Pedido { pedido.NumeroPedido } registrado com status '{ _defaultStatus }'.";
            return new Response<string>
            {
                Success = true,
                Data = response
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao registrar pedido: { ex.Message }");
        }
    }

    public Response<PedidoStatus> ConsultarPedido(string numeroPedido)
    {
        try
        {
            if (_cache.TryGetValue(numeroPedido, out PedidoStatus pedidoStatus))
            {
                return new Response<PedidoStatus>
                {
                    Success = true,
                    Data = pedidoStatus
                };
            }

            var erro = $"Pedido { numeroPedido } não encontrado.";
            return new Response<PedidoStatus>
            {
                Success = false,
                Message = erro
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao consultar pedido { numeroPedido }: { ex.Message }.");
        }
    }

    public Response<List<SituacaoPedido>> ConsultarTodosPedidos()
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

            return new Response<List<SituacaoPedido>>
            {
                Success = true,
                Data = todosPedidos
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao consultar todos os pedidos: { ex.Message }.");
        }
    }

    public Response<SituacaoPedido> ConsultarStatus(string numeroPedido)
    {
        try
        {
            if (_cache.TryGetValue(numeroPedido, out PedidoStatus pedidoStatus))
            {
                var situacao = new SituacaoPedido
                {
                    NumeroPedido = pedidoStatus.Pedido.NumeroPedido,
                    Status = pedidoStatus.Status
                };

                return new Response<SituacaoPedido>
                {
                    Success = true,
                    Data = situacao
                };
            }

            var erro = $"Pedido { numeroPedido } não encontrado.";
            return new Response<SituacaoPedido>
            {
                Success = false,
                Message = erro
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao consultar status do pedido { numeroPedido }: { ex.Message }.");
        }
    }

    public Response<string> AtualizarStatus(string numeroPedido, int novoStatus)
    {
        try
        {
            if (_cache.TryGetValue(numeroPedido, out PedidoStatus pedidoStatus))
            {
                pedidoStatus.Status = (StatusEnum)novoStatus;
                _cache.Set(numeroPedido, pedidoStatus);
                var response = $"Status do pedido { numeroPedido } atualizado para '{ pedidoStatus.Status }'.";
                return new Response<string>
                {
                    Success = true,
                    Data = response
                };
            }

            var erro = $"Pedido { numeroPedido } não encontrado.";
            return new Response<string>
            {
                Success = false,
                Message = erro
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao atualizar status do pedido: { ex.Message }");
        }
    }
}