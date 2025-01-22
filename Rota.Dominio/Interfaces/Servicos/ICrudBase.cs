using System;
using System.Collections.Generic;
using System.Text;
using Rota.Dominio.Entidades;

namespace Rota.Dominio.Interfaces.Servicos
{
    public interface ICrudBase<TEntidade> where TEntidade : EntidadeBase
    {
        TEntidade Incluir(TEntidade entidade);
        void Excluir(object id);
        void Excluir(TEntidade entidade);
        void Alterar(TEntidade entidade);
        TEntidade SelecionarPorId(object id);
        IEnumerable<TEntidade> SelecionarTodos();
    }
}
