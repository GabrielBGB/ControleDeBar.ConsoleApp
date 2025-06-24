using ControleDeBar.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace ControleDeBar.ConsoleApp.ModuloProduto
{
    public class Produto : EntidadeBase
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public Produto(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Produto produtoAtualizado = (Produto)registroAtualizado;
            this.Nome = produtoAtualizado.Nome;
            this.Preco = produtoAtualizado.Preco;
        }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome) || Nome.Trim().Length < 2 || Nome.Trim().Length > 100)
                erros.Add("O campo \"Nome\" é obrigatório, deve ter entre 2 e 100 caracteres.");

            if (Preco <= 0)
                erros.Add("O campo \"Preço\" deve ser maior que zero.");

            return erros.ToArray();
        }
    }
}