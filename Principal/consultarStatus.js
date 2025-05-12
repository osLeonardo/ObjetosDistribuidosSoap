const soap = require("soap");

const url = "http://localhost:5000/TransportadorService.svc?wsdl";
const numeroPedido = "456";

soap.createClient(url, function (err, client) {
  if (err) return console.error("Erro ao criar cliente SOAP:", err);

  client.ConsultarStatus({ numeroPedido }, function (err, result) {
    if (err) return console.error("Erro ao consultar status:", err);

    console.log("Status atual do pedido:", result.ConsultarStatusResult);
  });
});
