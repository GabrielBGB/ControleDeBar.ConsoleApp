using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using ControleDeBar.ConsoleApp.ModuloProduto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class TelaConta : TelaBase
    {
        private readonly RepositorioConta repositorioConta;
        private readonly RepositorioMesa repositorioMesa;
        private readonly RepositorioGarcom repositorioGarcom;
        private readonly RepositorioProduto repositorioProduto;
        private readonly TelaMesa telaMesa;
        private readonly TelaGarcom telaGarcom;
        private readonly TelaProduto telaProduto;

        public TelaConta(
            RepositorioConta rc,
            RepositorioMesa rm,
            RepositorioGarcom rg,
            RepositorioProduto rp,
            TelaMesa tm,
            TelaGarcom tg,
            TelaProduto tp)
            : base("Controle de Contas")
        {
            repositorioConta = rc;
            repositorioMesa = rm;
            repositorioGarcom = rg;
            repositorioProduto = rp;
            telaMesa = tm;
            telaGarcom = tg;
            telaProduto = tp;
        }

        public override string ApresentarMenu()
        {
            ApresentarCabecalho();
            Console.WriteLine("[1] Abrir nova conta");
            Console.WriteLine("[2] Adicionar pedido a uma conta");
            Console.WriteLine("[3] Remover pedido de uma conta");
            Console.WriteLine("[4] Fechar conta");
            Console.WriteLine("[5] Visualizar contas em aberto");
            Console.WriteLine("[6] Visualizar faturamento do dia");
            Console.WriteLine("[7] Visualizar contas fechadas");
            Console.WriteLine("\n[S] Sair");
            Console.Write("\nOpção: ");
            return Console.ReadLine().ToUpper();
        }

        public void AbrirNovaConta()
        {
            ApresentarCabecalho();
            Console.WriteLine("Abrindo nova conta...");

            if (!repositorioMesa.SelecionarTodos().Any(m => m.Status == StatusMesa.Livre))
            {
                MostrarMensagem("Não há mesas livres disponíveis.", ConsoleColor.DarkYellow);
                return;
            }

            if (repositorioGarcom.SelecionarTodos().Count == 0)
            {
                MostrarMensagem("Nenhum garçom cadastrado.", ConsoleColor.DarkYellow);
                return;
            }

            Mesa mesaSelecionada = ObterMesaValida();
            if (mesaSelecionada == null) return;

            Garcom garcomSelecionado = ObterGarcomValido();
            if (garcomSelecionado == null) return;

            Console.Write("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nomeCliente))
            {
                MostrarMensagem("Nome do cliente é obrigatório.", ConsoleColor.Red);
                return;
            }

            Conta novaConta = new Conta(mesaSelecionada, garcomSelecionado, nomeCliente);
            repositorioConta.Inserir(novaConta);
            mesaSelecionada.Status = StatusMesa.Ocupada;
            MostrarMensagem("Conta aberta com sucesso!", ConsoleColor.Green);
        }

        public void AdicionarPedido()
        {
            ApresentarCabecalho();
            Console.WriteLine("Adicionando Pedido em uma Conta...");

            if (!VisualizarContasEmAberto(false)) return;

            Conta contaSelecionada = ObterContaValida();
            if (contaSelecionada == null) return;

            if (repositorioProduto.SelecionarTodos().Count == 0)
            {
                MostrarMensagem("Nenhum produto cadastrado.", ConsoleColor.DarkYellow);
                return;
            }

            bool adicionarMaisPedidos = true;
            while (adicionarMaisPedidos)
            {
                Produto produtoSelecionado = ObterProdutoValido();
                if (produtoSelecionado == null) continue;

                Console.Write($"Digite a quantidade de '{produtoSelecionado.Nome}': ");
                if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
                {
                    MostrarMensagem("Quantidade inválida.", ConsoleColor.Red);
                    continue;
                }

                contaSelecionada.AdicionarPedido(new Pedido(produtoSelecionado, quantidade));
                MostrarMensagem("Pedido adicionado!", ConsoleColor.Green);

                Console.Write("Deseja adicionar outro pedido? (S/N): ");
                adicionarMaisPedidos = Console.ReadLine().Trim().ToUpper() == "S";
            }
        }

        public void RemoverPedido()
        {
            ApresentarCabecalho();
            Console.WriteLine("Removendo Pedido de uma Conta...");

            if (!VisualizarContasEmAberto(false)) return;

            Conta contaSelecionada = ObterContaValida();
            if (contaSelecionada == null) return;

            if (contaSelecionada.Pedidos.Count == 0)
            {
                MostrarMensagem("Esta conta não possui pedidos para remover.", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("Pedidos da conta:");
            for (int i = 0; i < contaSelecionada.Pedidos.Count; i++)
            {
                var pedido = contaSelecionada.Pedidos[i];
                Console.WriteLine($"[{i + 1}] {pedido.Produto.Nome} - Quantidade: {pedido.Quantidade}");
            }

            Console.Write("Digite o número do pedido para remover: ");
            if (!int.TryParse(Console.ReadLine(), out int indice) || indice <= 0 || indice > contaSelecionada.Pedidos.Count)
            {
                MostrarMensagem("Número inválido.", ConsoleColor.Red);
                return;
            }

            contaSelecionada.Pedidos.RemoveAt(indice - 1);
            contaSelecionada.CalcularValorTotal();
            MostrarMensagem("Pedido removido com sucesso.", ConsoleColor.Green);
        }

        public void FecharConta()
        {
            ApresentarCabecalho();
            Console.WriteLine("Fechando Conta...");

            if (!VisualizarContasEmAberto(false)) return;

            Conta contaSelecionada = ObterContaValida();
            if (contaSelecionada == null) return;

            
            contaSelecionada.Fechar();

            
            contaSelecionada.Mesa.Status = StatusMesa.Livre;

            contaSelecionada.CalcularValorTotal();

            MostrarMensagem($"Conta fechada. Total a pagar: R$ {contaSelecionada.ValorTotal:F2}", ConsoleColor.Green);
        }

        public bool VisualizarContasEmAberto(bool mostrarCabecalho)
        {
            if (mostrarCabecalho)
            {
                ApresentarCabecalho();
                Console.WriteLine("Visualizando Contas em Aberto...");
                Console.WriteLine();
            }

            var contas = repositorioConta.SelecionarTodos()
                .Where(c => c.Status == StatusConta.Aberta).ToList();

            if (contas.Count == 0)
            {
                MostrarMensagem("Nenhuma conta em aberto.", ConsoleColor.DarkYellow);
                return false;
            }

            
            Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-15} | {4,-10}", "ID", "Cliente", "Mesa", "Garçom", "Total");
            Console.WriteLine("-------------------------------------------------------------------");

            foreach (var conta in contas)
            {
                conta.CalcularValorTotal();

               
                Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-15} | R$ {4,-10:F2}",
                    conta.Id, conta.NomeCliente, conta.Mesa.Numero, conta.Garcom.Nome, conta.ValorTotal);

               
                if (conta.Pedidos.Any())
                {
                    
                    foreach (var pedido in conta.Pedidos)
                    {
                        Console.WriteLine("  └─> {0}x {1,-20} (R$ {2:F2} cada)",
                            pedido.Quantidade,
                            pedido.Produto.Nome,
                            pedido.Produto.Preco);
                    }
                }

               
                Console.WriteLine("-------------------------------------------------------------------");
                
            }

            return true;
        }

        public void VisualizarFaturamentoDoDia()
        {
            ApresentarCabecalho();

            var contasFechadasHoje = repositorioConta.SelecionarTodos()
                .Where(c => c.Status == StatusConta.Fechada && c.DataFechamento.Date == DateTime.Now.Date)
                .ToList();

            if (contasFechadasHoje.Count == 0)
            {
                MostrarMensagem("Nenhum faturamento registrado para hoje.", ConsoleColor.DarkYellow);
                return;
            }

            decimal totalFaturado = 0;
            foreach (var conta in contasFechadasHoje)
            {
                conta.CalcularValorTotal();
                totalFaturado += conta.ValorTotal;
            }

            Console.WriteLine($"Faturamento do dia ({DateTime.Now:dd/MM/yyyy}): R$ {totalFaturado:F2}");
        }

        public void VisualizarContasFechadas()
        {
            ApresentarCabecalho();

            var contasFechadas = repositorioConta.SelecionarTodos()
                .Where(c => c.Status == StatusConta.Fechada).ToList();

            if (contasFechadas.Count == 0)
            {
                MostrarMensagem("Nenhuma conta fechada encontrada.", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-15} | {4,-10} | {5,-10}",
                "ID", "Cliente", "Mesa", "Garçom", "Data", "Total");
            Console.WriteLine("----------------------------------------------------------------------------");

            foreach (var conta in contasFechadas)
            {
                conta.CalcularValorTotal();
                Console.WriteLine("{0,-5} | {1,-15} | {2,-10} | {3,-15} | {4,-10:dd/MM} | R$ {5,-10:F2}",
                    conta.Id, conta.NomeCliente, conta.Mesa.Numero, conta.Garcom.Nome, conta.DataFechamento, conta.ValorTotal);
            }
        }

        private Mesa ObterMesaValida()
        {
            Console.WriteLine("Mesas disponíveis:");
            var mesasLivres = repositorioMesa.SelecionarTodos()
                .Where(m => m.Status == StatusMesa.Livre)
                .ToList();

            foreach (var mesa in mesasLivres)
                Console.WriteLine($"[{mesa.Id}] Número: {mesa.Numero} Lugares: {mesa.QuantidadeLugares}");

            Console.Write("Digite o ID da mesa: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return null;
            }

            var mesaSelecionada = mesasLivres.FirstOrDefault(m => m.Id == id);

            if (mesaSelecionada == null)
            {
                MostrarMensagem("Mesa inválida ou ocupada.", ConsoleColor.Red);
                return null;
            }

            return mesaSelecionada;
        }

        private Garcom ObterGarcomValido()
        {
            Console.WriteLine("Garçons cadastrados:");
            var garcons = repositorioGarcom.SelecionarTodos();

            foreach (var garcom in garcons)
                Console.WriteLine($"[{garcom.Id}] Nome: {garcom.Nome} CPF: {garcom.Cpf}");

            Console.Write("Digite o ID do garçom: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return null;
            }

            var garcomSelecionado = garcons.FirstOrDefault(g => g.Id == id);

            if (garcomSelecionado == null)
            {
                MostrarMensagem("Garçom inválido.", ConsoleColor.Red);
                return null;
            }

            return garcomSelecionado;
        }

        private Conta ObterContaValida()
        {
            Console.Write("Digite o ID da conta: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return null;
            }

            var contaSelecionada = repositorioConta.SelecionarPorId(id);

            if (contaSelecionada == null || contaSelecionada.Status != StatusConta.Aberta)
            {
                MostrarMensagem("Conta inválida ou não está aberta.", ConsoleColor.Red);
                return null;
            }

            return contaSelecionada;
        }

        private Produto ObterProdutoValido()
        {
            Console.WriteLine("Produtos cadastrados:");
            var produtos = repositorioProduto.SelecionarTodos();
            foreach (var produto in produtos)
            {
                Console.WriteLine($"[{produto.Id}] Nome: {produto.Nome} Preço: R$ {produto.Preco:F2}");
            }

            Console.Write("Digite o ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarMensagem("ID inválido.", ConsoleColor.Red);
                return null;
            }

            var produtoSelecionado = produtos.FirstOrDefault(p => p.Id == id);

            if (produtoSelecionado == null)
            {
                MostrarMensagem("Produto inválido.", ConsoleColor.Red);
                return null;
            }

            return produtoSelecionado;
        }
    }
}