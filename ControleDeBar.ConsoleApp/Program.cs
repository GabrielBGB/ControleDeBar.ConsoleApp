using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using System;

namespace ControleDeBar.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instanciação dos repositórios
            RepositorioMesa repositorioMesa = new RepositorioMesa();
            RepositorioGarcom repositorioGarcom = new RepositorioGarcom();

            // Instanciação das telas
            TelaMesa telaMesa = new TelaMesa(repositorioMesa);
            TelaGarcom telaGarcom = new TelaGarcom(repositorioGarcom);
            TelaPrincipal telaPrincipal = new TelaPrincipal();

            bool continuar = true;
            while (continuar)
            {
                string opcaoPrincipal = telaPrincipal.ApresentarMenuPrincipal();

                if (opcaoPrincipal == "S")
                {
                    continuar = false;
                    continue;
                }

                TelaBase telaSelecionada = null;

                // Define qual tela será usada com base na opção principal
                if (opcaoPrincipal == "1")
                    telaSelecionada = telaMesa;

                else if (opcaoPrincipal == "2")
                    telaSelecionada = telaGarcom;

                // Se for uma opção de cadastro válida (1 ou 2), mostra o submenu
                if (telaSelecionada != null)
                {
                    string subMenu = telaSelecionada.ApresentarMenu();

                    // Converte a tela base para o tipo específico para chamar os métodos
                    if (telaSelecionada is TelaMesa)
                    {
                        TelaMesa tm = (TelaMesa)telaSelecionada;
                        if (subMenu == "1") tm.Inserir();
                        else if (subMenu == "2") tm.Editar();
                        else if (subMenu == "3") tm.Excluir();
                        else if (subMenu == "4")
                        {
                            tm.Visualizar(true);
                            Console.ReadLine(); // Adiciona a pausa aqui
                        }
                    }
                    else if (telaSelecionada is TelaGarcom)
                    {
                        TelaGarcom tg = (TelaGarcom)telaSelecionada;
                        if (subMenu == "1") tg.Inserir();
                        else if (subMenu == "2") tg.Editar();
                        else if (subMenu == "3") tg.Excluir();
                        else if (subMenu == "4")
                        {
                            tg.Visualizar(true);
                            Console.ReadLine(); // Adiciona a pausa aqui também
                        }
                    }
                }
            }
        }
    }
}