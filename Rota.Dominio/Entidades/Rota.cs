using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Rota.Dominio.Entidades
{
    public class Rota : EntidadeBase
    {
        public int Id { get; set; }

        public IEnumerable<Rota> Rotas{ get; set; }

        public string Conexao { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }

        public double Valor { get; set; }
    }
}
