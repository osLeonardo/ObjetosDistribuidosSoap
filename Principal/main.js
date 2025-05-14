#!/usr/bin/env node
const readline = require('readline');
const { spawn } = require('child_process');

const url = "http://localhost:5000/TransportadorService.svc?wsdl";
const scripts = {
  '1': 'clienteSoap.js',
  '2': 'consultarStatus.js',
  '3': 'consultarPedido.js',
  '4': 'consultarTodosPedidos.js',
  '5': 'atualizarStatus.js'
};

console.log('Selecione o script para executar:');
console.log('0) Sair');
console.log('1) Registrar Pedido');
console.log('2) Consultar Status');
console.log('3) Consultar Pedido');
console.log('4) Listar Todos os Pedidos');
console.log('5) Atualizar Status');
console.log('');

const rl = readline.createInterface({ input: process.stdin, output: process.stdout });
rl.question('Opção: ', (answer) => {
  const choice = answer.trim();
  if (choice === '0') {
    console.log('Encerrando.');
    rl.close();
    return;
  }
  const script = scripts[choice];
  if (!script) {
    console.log('Opção inválida.');
    rl.close();
    return;
  }

  console.log(`Executando: ${script}\n`);
  const child = spawn('node', [script, url], { cwd: __dirname, stdio: 'inherit' });
  child.on('exit', (code) => {
    if (code !== 0) {
      console.error(`Script saiu com código ${code}`);
    }
    rl.close();
  });
});
