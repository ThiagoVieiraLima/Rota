using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Rota.Dominio.Entidades;
using Rota.Dominio.Interfaces.Repositorio;
using Rota.Infra.Data.Contextos;

namespace Rota.Infra.Data.Repositorios
{
    public class TrechoRepositorio : RepositorioBase<Trecho>, ITrechoRepositorio
    {
        public TrechoRepositorio(Contexto contexto) : base(contexto)
        {
        }
    }
}
