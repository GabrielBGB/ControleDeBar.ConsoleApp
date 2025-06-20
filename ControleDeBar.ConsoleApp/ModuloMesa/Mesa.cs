using ControleDeBar.ConsoleApp.Compartilhado;

namespace ControleDeBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase
    {
        public string Numero { get; set; }
        public int QuantidadeLugares { get; set; }
        public StatusMesa Status { get; set; } // Usando um enum para o status

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
            // Implementaremos a validação depois
            return new string[0];
        }
    }

    public enum StatusMesa
    {
        Livre, Ocupada
    }
}