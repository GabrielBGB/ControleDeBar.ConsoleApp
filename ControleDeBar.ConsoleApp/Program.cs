using ControleDeBar.ConsoleApp.ModuloConta;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using System;

namespace ControleDeBar.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repositorioMesa = new RepositorioMesa();
            var repositorioGarcom = new RepositorioGarcom();
            var repositorioProduto = new RepositorioProduto();
            var repositorioConta = new RepositorioConta();

            var telaMesa = new TelaMesa(repositorioMesa, repositorioConta);
            var telaGarcom = new TelaGarcom(repositorioGarcom, repositorioConta);
            var telaProduto = new TelaProduto(repositorioProduto, repositorioConta);
            var telaConta = new TelaConta(repositorioConta, repositorioMesa, repositorioGarcom, repositorioProduto, telaMesa, telaGarcom, telaProduto);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Controle de Bar - Menu Principal");
                Console.WriteLine("[1] Mesas");
                Console.WriteLine("[2] Garçons");
                Console.WriteLine("[3] Produtos");
                Console.WriteLine("[4] Contas");
                Console.WriteLine("[S] Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine().ToUpper();

                switch (opcao)
                {
                    case "1":
                        ExecutarMenu(telaMesa);
                        break;
                    case "2":
                        ExecutarMenu(telaGarcom);
                        break;
                    case "3":
                        ExecutarMenu(telaProduto);
                        break;
                    case "4":
                        ExecutarMenu(telaConta);
                        break;
                    case "S":
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Pressione ENTER para tentar novamente...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        
        private static void ExecutarMenu(dynamic tela)
        {
            
            bool ehTelaConta = tela is TelaConta;

            while (true)
            {
                Console.Clear();
                string opcao = tela.ApresentarMenu(); 
                if (opcao == "S")
                    break;

                if (ehTelaConta)
                {
                    
                    switch (opcao)
                    {
                        case "1": tela.AbrirNovaConta(); break;
                        case "2": tela.AdicionarPedido(); break;
                        case "3": tela.RemoverPedido(); break;
                        case "4": tela.FecharConta(); break;
                        case "5": tela.VisualizarContasEmAberto(true); break;
                        case "6": tela.VisualizarFaturamentoDoDia(); break;
                        case "7": tela.VisualizarContasFechadas(); break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                else
                {
                    
                    switch (opcao)
                    {
                        case "1": tela.Inserir(); break;
                        case "2": tela.Editar(); break;
                        case "3": tela.Excluir(); break;
                        case "4": tela.Visualizar(true); break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }
}






