﻿using Transportadora.Enums;

namespace Transportadora.Models;

public class PedidoStatus
{
    public Pedido Pedido { get; set; }
    public StatusEnum Status { get; set; }
}