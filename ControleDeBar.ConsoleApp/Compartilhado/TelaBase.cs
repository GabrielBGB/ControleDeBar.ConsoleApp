using System;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class TelaBase
    {
        public string Titulo { get; set; }

        public TelaBase(string titulo)
        {
            Titulo = titulo;
        }

        public virtual string ApresentarMenu()
        {
            Console.Clear();
            Console.WriteLine(Titulo + "\n");
            Console.WriteLine("[1] Inserir");
            Console.WriteLine("[2] Editar");
            Console.WriteLine("[3] Excluir");
            Console.WriteLine("[4] Visualizar");
            Console.WriteLine("[S] Sair");
            Console.Write("\nOpção: ");
            return Console.ReadLine().ToUpper();
        }

        protected void MostrarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.WriteLine();
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }

        protected void ApresentarCabecalho()
        {
            Console.Clear();
            Console.WriteLine(Titulo + "\n");
        }
    }
}