using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rota.Dominio.Entidades;

namespace Rota.Infra.Data.Mapeamentos
{
    public class TrechoMap : MapBase<Trecho>
    {
        public override void Configure(EntityTypeBuilder<Trecho> builder)
        {
            base.Configure(builder);
            builder.ToTable("trecho");
            builder.Property(c => c.Origem).IsRequired().HasColumnName("Origem").HasMaxLength(3);
            builder.Property(c => c.Destino).IsRequired().HasColumnName("Destino").HasMaxLength(3);
            builder.Property(c => c.Valor).IsRequired().HasColumnName("Valor").HasColumnType("decimal(6,2)");
            builder.HasKey(c => c.Id);
            builder.HasAlternateKey(c => new { c.Origem, c.Destino });
        }
    }
}
