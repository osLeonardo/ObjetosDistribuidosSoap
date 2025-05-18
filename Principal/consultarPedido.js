const soap = require("soap");
const readline = require('readline');

const url = process.argv[2];
if (!url) {
  console.error('Uso: node consultarPedido.js <servicoSoapUrl>');
  process.exit(1);
}

const rl = readline.createInterface({ input: process.stdin, output: process.stdout });
rl.question('Número do pedido: ', (numeroPedido) => {
  rl.close();
  enviarConsulta(numeroPedido.trim());
});

function enviarConsulta(numeroPedido) {
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
      const res = result.ConsultarPedidoResult;
      if (!res.Success) {
        console.error(res.Message);
        process.exit(1);
      }
      console.log(JSON.stringify(res.Data, null, 2));
    });
  });
}
