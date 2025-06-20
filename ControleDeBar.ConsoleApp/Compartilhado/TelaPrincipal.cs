// Local: Compartilhado/TelaPrincipal.cs
using System;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        public string ApresentarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("--- Controle de Bar ---");
            Console.WriteLine("\n[1] Controle de Mesas");
            Console.WriteLine("[2] Controle de Garçons"); // NOVA OPÇÃO
            Console.WriteLine("[3] Controle de Produtos");
            Console.WriteLine("[4] Controle de Contas");
            Console.WriteLine("\n[S] Sair");
            Console.Write("\nOpção: ");
            return Console.ReadLine().ToUpper();
        }
    }
}