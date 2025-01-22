using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rota.Dominio.Entidades
{
    public class Trecho : EntidadeBase
    {
        /// <summary>
        /// Id do Terminal de Origem
        /// </summary>
        [Required]
        [Column(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// Sigla do Terminal de Origem
        /// </summary>
        [Required]
        [Column(Order = 2)]
        public string Origem { get; set; }

        /// <summary>
        /// Sigla do Terminal de Destino
        /// </summary>
        [Required]
        [Column(Order = 3)]
        public string Destino { get; set; }

        /// <summary>
        /// Valor cobrado no Trecho
        /// </summary>
        [Required]
        [Column(Order = 4)]
        public double Valor { get; set; }
    }
}
