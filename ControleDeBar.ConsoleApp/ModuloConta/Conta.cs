using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.ModuloGarcom;
using ControleDeBar.ConsoleApp.ModuloMesa;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeBar.ConsoleApp.ModuloConta
{
    public class Conta : EntidadeBase
    {
        public Mesa Mesa { get; set; }
        public Garcom Garcom { get; set; }
        public List<Pedido> Pedidos { get; private set; } = new List<Pedido>();
        public string NomeCliente { get; set; }
        public StatusConta Status { get; set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataAbertura { get; private set; }
        public DateTime DataFechamento { get; private set; }

        public Conta(Mesa mesa, Garcom garcom, string nomeCliente)
        {
            Mesa = mesa;
            Garcom = garcom;
            NomeCliente = nomeCliente;
            Status = StatusConta.Aberta;
            DataAbertura = DateTime.Now;
        }

        public void AdicionarPedido(Pedido novoPedido)
        {
            Pedidos.Add(novoPedido);
            CalcularValorTotal();
        }

        
        public void RemoverPedido(Pedido pedidoParaRemover)
        {
            Pedidos.Remove(pedidoParaRemover);
            CalcularValorTotal();
        }

        public void Fechar()
        {
            Status = StatusConta.Fechada;
            if (Mesa != null)
                Mesa.Status = StatusMesa.Livre;
            DataFechamento = DateTime.Now;
        }

        public void CalcularValorTotal()
        {
            this.ValorTotal = Pedidos.Sum(p => p.CalcularValor());
        }

        public override void AtualizarInformacoes(EntidadeBase registroAtualizado) { }
        public override string[] Validar() { return new string[0]; }
    }

    public enum StatusConta
    {
        Aberta, Fechada
    }
}