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
    public class ServicoAppBase<TEntidade, TEntidadeDTO> : IAppBase<TEntidade, TEntidadeDTO>
        where TEntidade : EntidadeBase
        where TEntidadeDTO : DTOBase
    {
        protected readonly ICrudBase<TEntidade> servico;
        protected readonly IMapper iMapper;

        public ServicoAppBase(IMapper iMapper, ICrudBase<TEntidade> servico)
            : base()
        {
            this.iMapper = iMapper;
            this.servico = servico;
        }

    }
}
