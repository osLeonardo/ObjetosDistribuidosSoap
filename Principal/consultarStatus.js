const soap = require("soap");
const readline = require('readline');

const url = process.argv[2];
if (!url) {
  console.error('Uso: node consultarStatus.js <servicoSoapUrl>');
  process.exit(1);
}

const rl = readline.createInterface({ input: process.stdin, output: process.stdout });
rl.question('Número do pedido: ', (numeroPedido) => {
  soap.createClient(url, function (err, client) {
    if (err) return console.error("Erro ao criar cliente SOAP:", err);
    client.ConsultarStatus({ numeroPedido }, function (err, result) {
      if (err) return console.error("Erro ao consultar status:", err);
      const res = result.ConsultarStatusResult;
      if (!res.Success) {
        console.error(res.Message);
      } else {
        console.log(res.Data);
      }
      rl.close();
    });
  });
});
