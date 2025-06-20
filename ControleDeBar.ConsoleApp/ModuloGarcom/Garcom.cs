// Local: ModuloGarcom/Garcom.cs
using ControleDeBar.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace ControleDeBar.ConsoleApp.ModuloGarcom
{
    public class Garcom : EntidadeBase
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public Garcom(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Garcom garcomAtualizado = (Garcom)registroAtualizado;
            this.Nome = garcomAtualizado.Nome;
            this.Cpf = garcomAtualizado.Cpf;
        }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome.Trim()) || Nome.Length < 3)
                erros.Add("O campo \"nome\" é obrigatório e precisa ter mais de 2 caracteres");

            if (string.IsNullOrEmpty(Cpf.Trim())) // Validação simples, pode ser melhorada com Regex
                erros.Add("O campo \"CPF\" é obrigatório");

            return erros.ToArray();
        }
    }
}