#!/usr/bin/env node
const readline = require("readline");
const { spawn } = require("child_process");

const url = "http://localhost:5000/TransportadorService.svc?wsdl";

const scripts = {
  1: { file: "clienteSoap.js", metodo: "RegistrarPedido" },
  2: { file: "consultarStatus.js" },
  3: { file: "consultarPedido.js" },
  4: { file: "consultarTodosPedidos.js" },
  5: { file: "atualizarStatus.js" },
};

function mostrarMenu() {
  console.log("");
  console.log("========================================");
  console.log("Selecione o script para executar:");
  console.table([
    { description: "Sair" },
    { description: "Registrar Pedido" },
    { description: "Consultar Status" },
    { description: "Consultar Pedido" },
    { description: "Listar Todos os Pedidos" },
    { description: "Atualizar Status" },
  ]);
  console.log("");
}

function perguntar() {
  const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout,
  });

  rl.question("Opção: ", (answer) => {
    const choice = answer.trim();
    if (choice === "0") {
      console.log("Encerrando.");
      rl.close();
      process.exit(0);
    }

    const scriptInfo = scripts[choice];
    if (!scriptInfo) {
      console.log("Opção inválida.");
      rl.close();
      mostrarMenu();
      return perguntar();
    }

    rl.close();

    const args = [];
    if (scriptInfo.metodo) {
      args.push(url, scriptInfo.metodo);
    } else {
      args.push(url);
    }

    console.log(`Executando: ${scriptInfo.file}\n`);
    const child = spawn("node", [scriptInfo.file, ...args], {
      cwd: "./",
      stdio: "inherit",
    });

    child.on("exit", (code) => {
      if (code !== 0) {
        console.error(`Script saiu com código ${code}`);
      }
      mostrarMenu();
      perguntar();
    });
  });
}

mostrarMenu();
perguntar();
