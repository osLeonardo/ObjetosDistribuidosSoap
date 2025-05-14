const soap = require("soap");

const url = process.argv[2];
if (!url) {
  console.error('Uso: node consultarTodosPedidos.js <servicoSoapUrl>');
  process.exit(1);
}

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
    console.log("Todos os pedidos:", JSON.stringify(result.ConsultarTodosPedidosResult, null, 2));
  });
});
