# ObjetosDistribuidosSoap

Projeto de Distribuição de Objetos via SOAP, composto por um servidor SOAP em C# (.NET 8) e um cliente SOAP em Node.js.

## Estrutura do Projeto

- **Principal**: Cliente Node.js para invocar operações SOAP.
- **Secundario/Transportadora**: Servidor SOAP em C# (.NET 8), implementando o serviço de transporte.

## Requisitos

- Node.js v14+ instalado
- .NET 8 SDK instalado

## Instalação e Execução

### Servidor SOAP (C#)

```powershell
cd Secundario/Transportadora
dotnet restore
dotnet run
```

O servidor ficará aguardando requisições em `http://localhost:5000/`.

### Cliente SOAP (Node.js)

```powershell
cd Principal
npm install
node main.js
```

## Funcionalidades do Cliente

- `atualizarStatus.js`: envia requisição para atualizar o status de um pedido.
- `consultarPedido.js`: consulta detalhes de um pedido.
- `consultarStatus.js`: obtém o status de um pedido específico.
- `consultarTodosPedidos.js`: lista todos os pedidos existentes.

## Documentação

Para especificação completa e requisitos do trabalho, consulte `Trabalho_SOAP.pdf` na raiz do projeto.

## Autores

- Gustavo Fontana
- Leonardo Spilere