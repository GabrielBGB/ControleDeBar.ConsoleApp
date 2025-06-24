using ControleDeBar.ConsoleApp.Compartilhado;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            if (string.IsNullOrEmpty(Nome) || Nome.Trim().Length < 3 || Nome.Trim().Length > 100)
                erros.Add("O campo \"Nome\" é obrigatório, deve ter entre 3 e 100 caracteres.");

            if (string.IsNullOrEmpty(Cpf.Trim()))
                erros.Add("O campo \"CPF\" é obrigatório");
            else
            {
                string patternCpf = @"^\d{3}\.\d{3}\.\d{3}\-\d{2}$"; // Exemplo: 123.456.789-00
                if (!Regex.IsMatch(Cpf, patternCpf))
                    erros.Add("O CPF deve estar no formato XXX.XXX.XXX-XX.");
            }

            return erros.ToArray();
        }
    }
}