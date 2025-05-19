const soap = require("soap");
const readline = require("readline");

const url = process.argv[2];
const metodo = process.argv[3];

if (!url || !metodo) {
  console.error("Uso: node clienteSoapInput.js <servicoSoapUrl> <MetodoSOAP>");
  process.exit(1);
}

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

function questionAsync(query) {
  return new Promise((resolve) =>
    rl.question(query, (answer) => resolve(answer.trim()))
  );
}

async function main() {
  try {
    console.log("Digite os dados do pedido:");

    const NumeroPedido = await questionAsync("Número do pedido: ");
    const EnderecoEntrega = await questionAsync("Endereço de entrega: ");
    const Destinatario = await questionAsync("Nome do destinatário: ");

    let itens = [];
    while (true) {
      const nomeItem = await questionAsync(
        "Nome do item (ou vazio para terminar): "
      );
      if (!nomeItem) break;
      const quantidadeStr = await questionAsync("Quantidade do item: ");
      const quantidade = parseInt(quantidadeStr, 10);
      if (isNaN(quantidade) || quantidade <= 0) {
        console.log("Quantidade inválida, tente novamente.");
        continue;
      }
      itens.push({ Nome: nomeItem, Quantidade: quantidade });
    }

    if (itens.length === 0) {
      console.log("Nenhum item informado. Encerrando.");
      rl.close();
      process.exit(1);
    }

    const pedido = {
      NumeroPedido,
      EnderecoEntrega,
      Destinatario,
      Itens: { ItemPedido: itens },
    };

    rl.close();

    soap.createClient(url, function (err, client) {
      if (err) return console.error("Erro ao criar cliente SOAP:", err);

      client[metodo]({ pedido }, function (err, result) {
        if (err)
          return console.error(`Erro ao executar método ${metodo}:`, err);

        const res = result[metodo + "Result"];
        if (!res.Success) {
          console.error(res.Message);
          return;
        }
        console.log("Resposta do serviço:");
        console.log(res.Data);
      });
    });
  } catch (e) {
    console.error("Erro inesperado:", e);
    rl.close();
  }
}

main();
