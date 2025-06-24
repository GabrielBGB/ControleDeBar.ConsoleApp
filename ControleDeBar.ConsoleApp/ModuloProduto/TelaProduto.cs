using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;
using System;
using System.Linq;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class TelaProduto : TelaBase
    {
        private readonly RepositorioProduto repositorioProduto;
        private readonly RepositorioConta repositorioConta;

        public TelaProduto(RepositorioProduto repositorioProduto, RepositorioConta repositorioConta) : base("Cadastro de Produtos")
        {
            this.repositorioProduto = repositorioProduto;
            this.repositorioConta = repositorioConta;
        }

        public void Inserir()
        {
            ApresentarCabecalho();
            Produto novoProduto = ObterProduto();

            var erros = novoProduto.Validar();
            if (erros.Length > 0)
            {
                foreach (var erro in erros)
                    MostrarMensagem(erro, ConsoleColor.Red);
                return;
            }

            if (repositorioProduto.SelecionarTodos()
                .Any(p => p.Nome.Equals(novoProduto.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                MostrarMensagem("Já existe um produto com este nome cadastrado.", ConsoleColor.Red);
                return;
            }

            repositorioProduto.Inserir(novoProduto);
            MostrarMensagem("Produto inserido com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            ApresentarCabecalho();
            Visualizar(false);
            Console.Write("\nDigite o ID do produto para editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return;
            }
            var produtoExistente = repositorioProduto.SelecionarPorId(id);
            if (produtoExistente == null)
            {
                MostrarMensagem("Produto não encontrado.", ConsoleColor.Red);
                return;
            }

            Produto produtoAtualizado = ObterProduto();

            var erros = produtoAtualizado.Validar();
            if (erros.Length > 0)
            {
                foreach (var erro in erros)
                    MostrarMensagem(erro, ConsoleColor.Red);
                return;
            }

            if (repositorioProduto.SelecionarTodos()
                .Any(p => p.Nome.Equals(produtoAtualizado.Nome, StringComparison.OrdinalIgnoreCase) && p.Id != id))
            {
                MostrarMensagem("Já existe outro produto com este nome.", ConsoleColor.Red);
                return;
            }

            repositorioProduto.Editar(id, produtoAtualizado);
            MostrarMensagem("Produto editado com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            ApresentarCabecalho();
            Visualizar(false);
            Console.Write("\nDigite o ID do produto para excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return;
            }

            bool produtoEstaEmUso = repositorioConta.SelecionarTodos()
                .Any(c => c.Status == StatusConta.Aberta && c.Pedidos.Any(p => p.Produto.Id == id));

            if (produtoEstaEmUso)
            {
                MostrarMensagem("Não é possível excluir este produto, pois ele faz parte de uma conta aberta.", ConsoleColor.Red);
                return;
            }

            repositorioProduto.Excluir(id);
            MostrarMensagem("Produto excluído com sucesso!", ConsoleColor.Green);
        }

        public void Visualizar(bool mostrarCabecalho)
        {
            if (mostrarCabecalho) ApresentarCabecalho();
            var produtos = repositorioProduto.SelecionarTodos();
            if (produtos.Count == 0)
            {
                MostrarMensagem("Nenhum produto cadastrado.", ConsoleColor.DarkYellow);
                return;
            }
            Console.WriteLine("{0,-5} | {1,-20} | {2,-15}", "ID", "Nome", "Preço");
            Console.WriteLine("-------------------------------------------------");
            foreach (var produto in produtos)
            {
                Console.WriteLine("{0,-5} | {1,-20} | R$ {2,-13:F2}", produto.Id, produto.Nome, produto.Preco);
            }
        }

        private Produto ObterProduto()
        {
            Console.Write("Digite o nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o preço do produto: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                preco = 0;
            }

            return new Produto(nome, preco);
        }
    }
}