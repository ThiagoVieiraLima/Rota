using System;
using System.Collections.Generic;
using System.Text;
using Rota.Aplicacao.DTO;
using Rota.Dominio.Entidades;

namespace Rota.Aplicacao.Interfaces
{
    public interface ICrudAppBase<TEntidade, TEntidadeDTO>
        where TEntidade : EntidadeBase
        where TEntidadeDTO : DTOBase
    {
        TEntidade Incluir(TEntidadeDTO entidade);
        void Excluir(object id);
        void Excluir(TEntidadeDTO entidade);
        void Alterar(TEntidadeDTO entidade);
        TEntidadeDTO SelecionarPorId(object id);
        IEnumerable<TEntidadeDTO> SelecionarTodos();
    }
}
