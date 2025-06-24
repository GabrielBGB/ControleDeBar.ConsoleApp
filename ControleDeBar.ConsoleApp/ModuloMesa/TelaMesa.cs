using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;
using System;
using System.Linq;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase
    {
        private readonly RepositorioMesa repositorioMesa;
        private readonly RepositorioConta repositorioConta;

        public TelaMesa(RepositorioMesa repositorioMesa, RepositorioConta repositorioConta) : base("Cadastro de Mesas")
        {
            this.repositorioMesa = repositorioMesa;
            this.repositorioConta = repositorioConta;
        }

        public void Inserir()
        {
            ApresentarCabecalho();
            Console.WriteLine("Inserindo nova Mesa...");

            Mesa novaMesa = ObterMesa();

            // Validação
            var erros = novaMesa.Validar();
            if (erros.Length > 0)
            {
                foreach (var erro in erros)
                    MostrarMensagem(erro, ConsoleColor.Red);
                return;
            }

            if (repositorioMesa.SelecionarTodos().Any(m => m.Numero == novaMesa.Numero))
            {
                MostrarMensagem("Já existe uma mesa com este número.", ConsoleColor.Red);
                return;
            }

            repositorioMesa.Inserir(novaMesa);
            MostrarMensagem("Mesa inserida com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            ApresentarCabecalho();
            Visualizar(false);
            Console.Write("\nDigite o ID da mesa para editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return;
            }
            var mesaExistente = repositorioMesa.SelecionarPorId(id);
            if (mesaExistente == null)
            {
                MostrarMensagem("Mesa não encontrada.", ConsoleColor.Red);
                return;
            }

            Mesa mesaAtualizada = ObterMesa();

            var erros = mesaAtualizada.Validar();
            if (erros.Length > 0)
            {
                foreach (var erro in erros)
                    MostrarMensagem(erro, ConsoleColor.Red);
                return;
            }

            if (repositorioMesa.SelecionarTodos()
                .Any(m => m.Numero == mesaAtualizada.Numero && m.Id != id))
            {
                MostrarMensagem("Já existe outra mesa com este número.", ConsoleColor.Red);
                return;
            }

            repositorioMesa.Editar(id, mesaAtualizada);
            MostrarMensagem("Mesa editada com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            ApresentarCabecalho();
            Visualizar(false);
            Console.Write("\nDigite o ID da mesa para excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return;
            }

            bool mesaEstaEmUso = repositorioConta.SelecionarTodos()
                .Any(conta => conta.Mesa.Id == id && conta.Status == StatusConta.Aberta);

            if (mesaEstaEmUso)
            {
                MostrarMensagem("Não é possível excluir esta mesa, pois ela tem uma conta aberta.", ConsoleColor.Red);
                return;
            }

            repositorioMesa.Excluir(id);
            MostrarMensagem("Mesa excluída com sucesso!", ConsoleColor.Green);
        }

        public void Visualizar(bool mostrarCabecalho)
        {
            if (mostrarCabecalho)
                ApresentarCabecalho();

            Console.WriteLine("Visualizando Mesas...");
            var mesas = repositorioMesa.SelecionarTodos();

            if (mesas.Count == 0)
            {
                MostrarMensagem("Nenhuma mesa cadastrada.", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("{0,-5} | {1,-10} | {2,-15} | {3,-10}", "ID", "Número", "Quantidade Lugares", "Status");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (var mesa in mesas)
            {
                Console.WriteLine("{0,-5} | {1,-10} | {2,-15} | {3,-10}", mesa.Id, mesa.Numero, mesa.QuantidadeLugares, mesa.Status);
            }
        }

        private Mesa ObterMesa()
        {
            Console.Write("Digite o número da mesa: ");
            string numero = Console.ReadLine();

            Console.Write("Digite a quantidade de lugares: ");
            if (!int.TryParse(Console.ReadLine(), out int lugares))
            {
                lugares = 0;
            }

            return new Mesa(numero, lugares);
        }
    }
}