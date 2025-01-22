using System;
using System.Collections.Generic;
using System.Text;
using Rota.Aplicacao.DTO;
using Rota.Dominio.Entidades;

namespace Rota.Aplicacao.Interfaces
{
    public interface IRotaApp : IAppBase<Dominio.Entidades.Rota, RotaDTO>
    {
        public IEnumerable<RotaDTO> Listar(string origem, string destino);
        public IEnumerable<RotaDTO> Obter(string origem, string destino, Dominio.Entidades.Enumerador.Ordem ordem);
    }
}
