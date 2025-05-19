const soap = require("soap");

const url = process.argv[2];
soap.createClient(url, function (err, client) {
  if (err) {
    console.error("Erro ao criar cliente SOAP:", err);
    process.exit(1);
  }

  client.ConsultarTodosPedidos({}, function (err, result) {
    if (err) {
      console.error("Erro ao listar pedidos:", err);
      process.exit(1);
    }
    const res = result.ConsultarTodosPedidosResult;
    if (!res.Success) {
      console.error(res.Message);
      process.exit(1);
    }

    const raw = res.Data.SituacaoPedido;
    let lista = [];
    if (Array.isArray(raw)) {
      lista = raw;
    } else if (raw) {
      lista = [raw];
    }
    console.log(JSON.stringify(lista, null, 2));
  });
});
