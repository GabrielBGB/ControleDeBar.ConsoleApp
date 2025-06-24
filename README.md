# 💻 Controle de Bar - Sistema de Gerenciamento

Aplicação de console desenvolvida em **C#** com **.NET**, para simular um sistema de gerenciamento de bar com **operações típicas de um PDV** (Ponto de Venda).

> ✅ Projeto modular, baseado em princípios de **POO** (Programação Orientada a Objetos), com foco na **organização, manutenibilidade e validações de negócio**.

---

## ✨ Funcionalidades Principais

O sistema é dividido em **4 módulos principais**:

---

### 📋 1. Módulo de Cadastros

#### 🍽️ Mesas
- ✅ Cadastrar, editar, excluir e visualizar mesas
- 🔄 Controle de status: `Livre` / `Ocupada`
- 🛡️ Validação para impedir **números duplicados**
- 🔒 Impede exclusão se houver conta vinculada

#### 🧑‍🍳 Garçons
- ✅ Cadastrar, editar, excluir e visualizar garçons
- 🧾 Validação de:
  - Nome (mín. 3, máx. 100 caracteres)
  - CPF no formato `XXX.XXX.XXX-XX`
- 🔁 Não permite CPF ou nome duplicado
- 🔒 Impede exclusão se houver conta vinculada

#### 🥤 Produtos
- ✅ Cadastrar, editar, excluir e visualizar produtos
- 💲 Preço com até 2 casas decimais
- 🛑 Impede nomes duplicados
- 🔒 Não permite exclusão se o produto estiver em uso

---

### 💰 2. Módulo de Contas

#### 🧾 Abertura de Conta
- Seleciona **mesa livre**, **garçom** e **nome do cliente**
- Mesa passa automaticamente para `Ocupada`

#### 📦 Registro de Pedidos
- Adiciona **produtos e quantidades** a contas abertas
- 💸 Valor total calculado automaticamente

#### ✅ Fechamento de Conta
- Mostra todos os pedidos da conta
- Exibe valor total
- Libera mesa ao finalizar

#### 📊 Relatórios
- 📋 Listagem de **contas em aberto**
- 📅 Faturamento diário (com soma de valores das contas fechadas no dia)

---

### 📜 3. Validações e Regras de Negócio

- ❌ Impede a exclusão de registros que estão em uso em contas
- 🧠 Valida obrigatoriedade e formato de campos como:
  - Nome
  - CPF
  - Preço
  - Quantidade
  - Número da mesa


---

## 🚀 Como Executar o Projeto

```bash
https://github.com/GabrielBGB/ControleDeBar.ConsoleApp
```

1. Abra o projeto no **Visual Studio**
2. Certifique-se de que `ControleDeBar.ConsoleApp` está como **projeto de inicialização**
3. Pressione `F5` ou clique em **Iniciar**
4. Use o menu no console para navegar pelo sistema

---

## 🎬 Demonstração

> ![GIF mostrando a execução do programa](https://i.imgur.com/aBcDeF1.gif)

---

## 🛠️ Tecnologias Utilizadas

- C# (.NET)
- Programação Orientada a Objetos
- Console Application

---

## 📂 Estrutura do Projeto

```
ControleDeBar.ConsoleApp/
├── ModuloMesa/
├── ModuloGarcom/
├── ModuloProduto/
├── ModuloConta/
├── Compartilhado/
└── Program.cs
```

---

## 📌 Status

✅ Projeto finalizado com todas as funcionalidades e validações do enunciado implementadas.

---

## 👨‍💻 Autor

**Gabriel Fernando da Silva Barbosa**  
[🔗 gabrieltech.dev.br](https://gabrieltech.dev.br)

---
