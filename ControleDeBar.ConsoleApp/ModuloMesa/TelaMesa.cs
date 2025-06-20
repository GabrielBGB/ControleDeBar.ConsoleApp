using System;
using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class TelaMesa : TelaBase
    {
        private readonly RepositorioMesa repositorioMesa;

        public TelaMesa(RepositorioMesa repositorioMesa) : base("Cadastro de Mesas")
        {
            this.repositorioMesa = repositorioMesa;
        }

        public void Inserir()
        {
            ApresentarCabecalho();
            Console.WriteLine("Inserindo nova Mesa...");

            Mesa novaMesa = ObterMesa();

            repositorioMesa.Inserir(novaMesa);

            MostrarMensagem("Mesa inserida com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            ApresentarCabecalho();
            Console.WriteLine("Editando Mesa...");

            Visualizar(false);
            Console.Write("\nDigite o ID da mesa para editar: ");
            int id = int.Parse(Console.ReadLine());

            Mesa mesaAtualizada = ObterMesa();

            repositorioMesa.Editar(id, mesaAtualizada);

            MostrarMensagem("Mesa editada com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            ApresentarCabecalho();
            Console.WriteLine("Excluindo Mesa...");

            Visualizar(false);
            Console.Write("\nDigite o ID da mesa para excluir: ");
            int id = int.Parse(Console.ReadLine());

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

            Console.WriteLine("{0,-5} | {1,-10} | {2,-10}", "ID", "Número", "Status");
            Console.WriteLine("-----------------------------------");
            foreach (var mesa in mesas)
            {
                Console.WriteLine("{0,-5} | {1,-10} | {2,-10}", mesa.Id, mesa.Numero, mesa.Status);
            }
        }

        private Mesa ObterMesa()
        {
            Console.Write("Digite o número da mesa: ");
            string numero = Console.ReadLine();
            Console.Write("Digite a quantidade de lugares: ");
            int lugares = int.Parse(Console.ReadLine());

            return new Mesa(numero, lugares);
        }
    }
}