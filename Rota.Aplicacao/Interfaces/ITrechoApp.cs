using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Rota.Aplicacao.DTO;
using Rota.Dominio.Entidades;

namespace Rota.Aplicacao.Interfaces
{
    public interface ITrechoApp : ICrudAppBase<Trecho, TrechoDTO>
    {
    }
}
