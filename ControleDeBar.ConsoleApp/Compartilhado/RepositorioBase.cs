using System.Collections.Generic;

namespace ControleDeBar.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected List<T> listaRegistros = new List<T>();
        protected int contadorRegistros = 0;

        public virtual void Inserir(T registro)
        {
            contadorRegistros++;
            registro.Id = contadorRegistros;
            listaRegistros.Add(registro);
        }

        public virtual void Editar(int id, T registroAtualizado)
        {
            T registroSelecionado = SelecionarPorId(id);
            registroSelecionado.AtualizarInformacoes(registroAtualizado);
        }

        public virtual void Excluir(int id)
        {
            T registroSelecionado = SelecionarPorId(id);
            listaRegistros.Remove(registroSelecionado);
        }

        public T SelecionarPorId(int id)
        {
            foreach (T registro in listaRegistros)
            {
                if (registro.Id == id)
                    return registro;
            }
            return null;
        }

        public List<T> SelecionarTodos()
        {
            return listaRegistros;
        }
    }
}