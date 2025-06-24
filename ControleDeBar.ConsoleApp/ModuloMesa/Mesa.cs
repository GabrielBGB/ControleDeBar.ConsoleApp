using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase
    {
        public string Numero { get; set; }
        public int QuantidadeLugares { get; set; }
        public StatusMesa Status { get; set; } 

        public Mesa(string numero, int quantidadeLugares)
        {
            Numero = numero;
            QuantidadeLugares = quantidadeLugares;
            Status = StatusMesa.Livre;
        }

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Mesa mesaAtualizada = (Mesa)registroAtualizado;
            this.Numero = mesaAtualizada.Numero;
            this.QuantidadeLugares = mesaAtualizada.QuantidadeLugares;
        }

        public override string[] Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Numero) || !int.TryParse(Numero, out int numeroMesa) || numeroMesa <= 0)
                erros.Add("O campo \"Número\" é obrigatório, deve ser um número inteiro positivo.");

            if (QuantidadeLugares <= 0)
                erros.Add("O campo \"Quantidade de Lugares\" deve ser maior que zero.");

            return erros.ToArray();
        }
    }

    public enum StatusMesa
    {
        Livre, Ocupada
    }
}