const soap = require("soap");
const fs = require("fs");

const url = process.argv[2];
if (!url) {
  console.error('Uso: node clienteSoap.js <servicoSoapUrl>');
  process.exit(1);
}

const rawPedido = JSON.parse(fs.readFileSync("pedido.json", "utf8"));
const pedido = { ...rawPedido,
  Itens: { ItemPedido: rawPedido.Itens.map(item => ({ Nome: item.Nome, Quantidade: item.Quantidade })) }
};

soap.createClient(url, function (err, client) {
  if (err) return console.error("Erro ao criar cliente SOAP:", err);

  client.RegistrarPedido({ pedido }, function (err, result) {
    if (err) return console.error("Erro ao enviar pedido:", err);
    const res = result.RegistrarPedidoResult;
    if (!res.Success) {
      console.error(res.Message);
      return;
    }
    console.log(res.Data);
  });
});
