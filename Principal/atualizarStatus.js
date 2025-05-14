const soap = require("soap");

const url = process.argv[2];
const numeroPedido = process.argv[3];
const novoStatus = process.argv[4];

if (!url || !numeroPedido || !novoStatus) {
  console.error('Uso: node atualizarStatus.js <servicoSoapUrl> <numeroPedido> <novoStatus>');
  process.exit(1);
}

soap.createClient(url, function (err, client) {
  if (err) {
    console.error("Erro ao criar cliente SOAP:", err);
    process.exit(1);
  }

  client.AtualizarStatus({ numeroPedido, novoStatus: parseInt(novoStatus) }, function (err, result) {
    if (err) {
      console.error("Erro ao atualizar status:", err);
      process.exit(1);
    }
    console.log("Resposta da atualização de status:", result.AtualizarStatusResult);
  });
});
