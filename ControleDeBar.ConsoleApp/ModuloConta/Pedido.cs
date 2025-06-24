using ControleDeBar.ConsoleApp.ModuloProduto;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class Pedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public Pedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public decimal CalcularValor()
        {
            return Produto.Preco * Quantidade;
        }
    }
}