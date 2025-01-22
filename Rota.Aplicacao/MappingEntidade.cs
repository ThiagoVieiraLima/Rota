using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoMapper;
using Rota.Aplicacao.DTO;
using Rota.Aplicacao.Interfaces;
using Rota.Aplicacao.Servicos;
using Rota.Dominio.Entidades;

namespace Rota.Aplicacao
{
    public class MappingEntidade : Profile
    {
        public MappingEntidade()
        {
            CreateMap<Trecho, TrechoDTO>();
            CreateMap<TrechoDTO, Trecho>();
            CreateMap<Dominio.Entidades.Rota, RotaDTO>();
            CreateMap<RotaDTO, Dominio.Entidades.Rota>();
        }
    }
}
