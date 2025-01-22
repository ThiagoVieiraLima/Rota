using System;
using System.Collections.Generic;
using System.Text;
using Rota.Aplicacao.DTO;
using Rota.Dominio.Entidades;

namespace Rota.Aplicacao.Interfaces
{
    public interface IAppBase<TEntidade, TEntidadeDTO>
        where TEntidade : EntidadeBase
        where TEntidadeDTO : DTOBase
    {
    }
}
