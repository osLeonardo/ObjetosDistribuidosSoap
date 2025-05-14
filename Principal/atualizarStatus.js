const soap = require("soap");

const url = process.argv[2];
if (!url) {
  console.error('Uso: node atualizarStatus.js <servicoSoapUrl>');
  process.exit(1);
}

const readline = require('readline');
const rl = readline.createInterface({ input: process.stdin, output: process.stdout });

rl.question('Número do pedido: ', (numeroPedido) => {
  console.log('Status disponíveis:');
  console.table([
    { description: 'Pedido não encontrado' },
    { description: 'Aguardando coleta' },
    { description: 'Em transporte' },
    { description: 'Entregue' },
    { description: 'Falha na entrega' }
  ]);
  rl.question('Novo status (número): ', (novoStatusInput) => {
    const novoStatus = parseInt(novoStatusInput, 10);
    soap.createClient(url, function (err, client) {
      if (err) {
        console.error("Erro ao criar cliente SOAP:", err);
        rl.close();
        process.exit(1);
      }
      client.AtualizarStatus({ numeroPedido, novoStatus }, function (err, result) {
        if (err) {
          console.error("Erro ao atualizar status:", err);
          rl.close();
          process.exit(1);
        }
        console.log("Resposta da atualização de status:", result.AtualizarStatusResult);
        rl.close();
      });
    });
  });
});
