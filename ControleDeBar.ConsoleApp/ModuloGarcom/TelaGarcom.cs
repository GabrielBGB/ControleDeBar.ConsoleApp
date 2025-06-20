// Local: ModuloGarcom/TelaGarcom.cs
using ControleDeBar.ConsoleApp.Compartilhado;
using System;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class TelaGarcom : TelaBase
    {
        private readonly RepositorioGarcom repositorioGarcom;

        public TelaGarcom(RepositorioGarcom repositorioGarcom) : base("Cadastro de Garçons")
        {
            this.repositorioGarcom = repositorioGarcom;
        }

        public void Inserir()
        {
            ApresentarCabecalho();
            Console.WriteLine("Inserindo novo Garçom...");
            Garcom novoGarcom = ObterGarcom();
            repositorioGarcom.Inserir(novoGarcom);
            MostrarMensagem("Garçom inserido com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            ApresentarCabecalho();
            Console.WriteLine("Editando Garçom...");
            Visualizar(false);
            Console.Write("\nDigite o ID do garçom para editar: ");
            int id = int.Parse(Console.ReadLine());
            Garcom garcomAtualizado = ObterGarcom();
            repositorioGarcom.Editar(id, garcomAtualizado);
            MostrarMensagem("Garçom editado com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            ApresentarCabecalho();
            Console.WriteLine("Excluindo Garçom...");
            Visualizar(false);
            Console.Write("\nDigite o ID do garçom para excluir: ");
            int id = int.Parse(Console.ReadLine());
            repositorioGarcom.Excluir(id);
            MostrarMensagem("Garçom excluído com sucesso!", ConsoleColor.Green);
        }

        public void Visualizar(bool mostrarCabecalho)
        {
            if (mostrarCabecalho)
                ApresentarCabecalho();

            Console.WriteLine("Visualizando Garçons...");
            var garcons = repositorioGarcom.SelecionarTodos();

            if (garcons.Count == 0)
            {
                MostrarMensagem("Nenhum garçom cadastrado.", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("{0,-5} | {1,-20} | {2,-15}", "ID", "Nome", "CPF");
            Console.WriteLine("-------------------------------------------------");
            foreach (var garcom in garcons)
            {
                Console.WriteLine("{0,-5} | {1,-20} | {2,-15}", garcom.Id, garcom.Nome, garcom.Cpf);
            }
        }

        private Garcom ObterGarcom()
        {
            Console.Write("Digite o nome do garçom: ");
            string nome = Console.ReadLine();
            Console.Write("Digite o CPF do garçom: ");
            string cpf = Console.ReadLine();

            return new Garcom(nome, cpf);
        }
    }
}