const soap = require("soap");

const url = process.argv[2];
const numeroPedido = process.argv[3];
if (!url || !numeroPedido) {
  console.error('Uso: node consultarPedido.js <servicoSoapUrl> <numeroPedido>');
  process.exit(1);
}

soap.createClient(url, function (err, client) {
  if (err) {
    console.error("Erro ao criar cliente SOAP:", err);
    process.exit(1);
  }

  client.ConsultarPedido({ numeroPedido }, function (err, result) {
    if (err) {
      console.error("Erro ao consultar pedido:", err);
      process.exit(1);
    }
    console.log("Detalhes do pedido:", JSON.stringify(result.ConsultarPedidoResult, null, 2));
  });
});
