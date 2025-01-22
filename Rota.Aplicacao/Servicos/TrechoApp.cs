using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoMapper;
using Rota.Aplicacao.DTO;
using Rota.Aplicacao.Interfaces;
using Rota.Dominio.Entidades;
using Rota.Dominio.Interfaces.Servicos;

namespace Rota.Aplicacao.Servicos
{
    public class TrechoApp : CrudAppBase<Trecho, TrechoDTO>, ITrechoApp
    {
        public TrechoApp(IMapper iMapper, ITrechoServico servico) : base(iMapper, servico)
        {

        }
    }
}
