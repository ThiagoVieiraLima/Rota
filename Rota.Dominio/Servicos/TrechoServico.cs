using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Rota.Dominio.Entidades;
using Rota.Dominio.Interfaces.Repositorio;
using Rota.Dominio.Interfaces.Servicos;

namespace Rota.Dominio.Servicos
{
    public class TrechoServico : CrudBase<Trecho>, ITrechoServico
    {
        public TrechoServico(ITrechoRepositorio repositorio) : base(repositorio)
        {

        }
    }
}
