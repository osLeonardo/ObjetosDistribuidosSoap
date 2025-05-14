const soap = require("soap");

const url = process.argv[2];
if (!url) {
  console.error('Uso: node consultarStatus.js <servicoSoapUrl>');
  process.exit(1);
}

const numeroPedido = "456";

soap.createClient(url, function (err, client) {
  if (err) return console.error("Erro ao criar cliente SOAP:", err);

  client.ConsultarStatus({ numeroPedido }, function (err, result) {
    if (err) return console.error("Erro ao consultar status:", err);

    console.log("Status atual do pedido:", result.ConsultarStatusResult);
  });
});
