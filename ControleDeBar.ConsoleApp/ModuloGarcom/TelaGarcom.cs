using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloConta;
using System;
using System.Linq;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class TelaGarcom : TelaBase
    {
        private readonly RepositorioGarcom repositorioGarcom;
        private readonly RepositorioConta repositorioConta;

        public TelaGarcom(RepositorioGarcom repositorioGarcom, RepositorioConta repositorioConta) : base("Cadastro de Garçons")
        {
            this.repositorioGarcom = repositorioGarcom;
            this.repositorioConta = repositorioConta;
        }

        public void Inserir()
        {
            ApresentarCabecalho();
            Garcom novoGarcom = ObterGarcom();

            bool existeDuplicado = repositorioGarcom.SelecionarTodos()
                .Any(g => g.Nome.Equals(novoGarcom.Nome, StringComparison.OrdinalIgnoreCase)
                          && g.Cpf == novoGarcom.Cpf);

            if (existeDuplicado)
            {
                MostrarMensagem("Já existe um garçom com esse nome e CPF cadastrados.", ConsoleColor.Red);
                return;
            }

            var erros = novoGarcom.Validar();
            if (erros.Length > 0)
            {
                foreach (var erro in erros)
                    MostrarMensagem(erro, ConsoleColor.Red);
                return;
            }

            repositorioGarcom.Inserir(novoGarcom);
            MostrarMensagem("Garçom inserido com sucesso!", ConsoleColor.Green);
        }

        public void Editar()
        {
            ApresentarCabecalho();
            Visualizar(false);
            Console.Write("\nDigite o ID do garçom para editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return;
            }
            var garcomExistente = repositorioGarcom.SelecionarPorId(id);
            if (garcomExistente == null)
            {
                MostrarMensagem("Garçom não encontrado.", ConsoleColor.Red);
                return;
            }

            Garcom garcomAtualizado = ObterGarcom();

            bool existeDuplicado = repositorioGarcom.SelecionarTodos()
                .Any(g => g.Id != id &&
                          g.Nome.Equals(garcomAtualizado.Nome, StringComparison.OrdinalIgnoreCase) &&
                          g.Cpf == garcomAtualizado.Cpf);

            if (existeDuplicado)
            {
                MostrarMensagem("Já existe outro garçom com esse nome e CPF cadastrados.", ConsoleColor.Red);
                return;
            }

            var erros = garcomAtualizado.Validar();
            if (erros.Length > 0)
            {
                foreach (var erro in erros)
                    MostrarMensagem(erro, ConsoleColor.Red);
                return;
            }

            repositorioGarcom.Editar(id, garcomAtualizado);
            MostrarMensagem("Garçom editado com sucesso!", ConsoleColor.Green);
        }

        public void Excluir()
        {
            ApresentarCabecalho();
            Visualizar(false);
            Console.Write("\nDigite o ID do garçom para excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return;
            }

            bool garcomEstaEmUso = repositorioConta.SelecionarTodos()
                .Any(c => c.Garcom.Id == id && c.Status == StatusConta.Aberta);

            if (garcomEstaEmUso)
            {
                MostrarMensagem("Não é possível excluir, garçom atendendo em uma conta aberta.", ConsoleColor.Red);
                return;
            }

            repositorioGarcom.Excluir(id);
            MostrarMensagem("Garçom excluído com sucesso!", ConsoleColor.Green);
        }

        public void Visualizar(bool mostrarCabecalho)
        {
            if (mostrarCabecalho) ApresentarCabecalho();
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
            Console.Write("Digite o CPF do garçom (formato XXX.XXX.XXX-XX): ");
            string cpf = Console.ReadLine();
            return new Garcom(nome, cpf);
        }
    }
}