using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Rota.Aplicacao.DTO;
using Rota.Aplicacao.Interfaces;
using Rota.Dominio.Entidades;
using Rota.Dominio.Interfaces.Servicos;

namespace Rota.Aplicacao.Servicos
{
    public class CrudAppBase<TEntidade, TEntidadeDTO> : ICrudAppBase<TEntidade, TEntidadeDTO>
        where TEntidade : EntidadeBase
        where TEntidadeDTO : DTOBase
    {
        protected readonly ICrudBase<TEntidade> servico;
        protected readonly IMapper iMapper;

        public CrudAppBase(IMapper iMapper, ICrudBase<TEntidade> servico)
            : base()
        {
            this.iMapper = iMapper;
            this.servico = servico;
        }

        public void Alterar(TEntidadeDTO entidade)
        {
            servico.Alterar(iMapper.Map<TEntidade>(entidade));
        }

        public void Excluir(object id)
        {
            servico.Excluir(id);
        }

        public void Excluir(TEntidadeDTO entidade)
        {
            servico.Excluir(iMapper.Map<TEntidade>(entidade));
        }

        public TEntidade Incluir(TEntidadeDTO entidade)
        {
            return servico.Incluir(iMapper.Map<TEntidade>(entidade));
        }

        public TEntidadeDTO SelecionarPorId(object id)
        {
            return iMapper.Map<TEntidadeDTO>(servico.SelecionarPorId(id));
        }

        public IEnumerable<TEntidadeDTO> SelecionarTodos()
        {
            return iMapper.Map<IEnumerable<TEntidadeDTO>>(servico.SelecionarTodos());
        }
    }
}
