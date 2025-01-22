using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Rota.Dominio.Entidades;
using Rota.Dominio.Interfaces.Repositorio;
using Rota.Infra.Data.Contextos;

namespace Rota.Infra.Data.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade>
        where TEntidade : EntidadeBase
    {
        protected readonly Contexto contexto;

        public RepositorioBase(Contexto contexto)
            : base()
        {
            ///Verifica a criação da tabela
            try
            {
                contexto.Set<TEntidade>().Find(0);

            } catch (Microsoft.Data.Sqlite.SqliteException e)
            {
                if (e.SqliteErrorCode == 1)
                {
                    contexto.Database.ExecuteSqlRaw(contexto.Database.GenerateCreateScript());
                }
            }

            this.contexto = contexto;
        }

        public TEntidade Incluir(TEntidade entidade)
        {
            var e = contexto.Set<TEntidade>().Add(entidade).Entity;
            contexto.SaveChanges();
            return e;
        }

        public void Alterar(TEntidade entidade)
        {
            contexto.Set<TEntidade>().Attach(entidade);
            contexto.Entry(entidade).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Excluir(object id)
        {
            var entidade = SelecionarPorId(id);
            if (entidade != null)
            {
                contexto.Set<TEntidade>().Remove(entidade);
                contexto.SaveChanges();
            }
        }

        public void Excluir(TEntidade entidade)
        {
            contexto.Set<TEntidade>().Remove(entidade);
            contexto.SaveChanges();
        }

        public TEntidade SelecionarPorId(object id)
        {
            var e = contexto.Set<TEntidade>().Find(id);
            if (e != null) contexto.Entry(e).State = EntityState.Detached;
            return e;
        }

        public IEnumerable<TEntidade> SelecionarTodos()
        {
            return contexto.Set<TEntidade>().ToList();
        }
    }
}
