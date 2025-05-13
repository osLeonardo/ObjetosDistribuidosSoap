const soap = require("soap");
const fs = require("fs");

const url = "http://localhost:5000/TransportadorService.svc?wsdl";
const pedido = JSON.parse(fs.readFileSync("pedido.json", "utf8"));

soap.createClient(url, function (err, client) {
  if (err) return console.error("Erro ao criar cliente SOAP:", err);

  client.RegistrarPedido({ pedido }, function (err, result) {
    if (err) return console.error("Erro ao enviar pedido:", err);

    console.log("Resposta do transportador:", result.RegistrarPedidoResult);
  });
});
