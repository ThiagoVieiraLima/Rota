using System;
using System.Collections.Generic;
using System.Text;
using Rota.Dominio.Entidades;
using Rota.Dominio.Interfaces.Repositorio;
using Rota.Dominio.Interfaces.Servicos;

namespace Rota.Dominio.Servicos
{
    public class CrudBase<TEntidade> : ICrudBase<TEntidade> where TEntidade : EntidadeBase
    {
        protected readonly IRepositorioBase<TEntidade> repositorio;

        public CrudBase(IRepositorioBase<TEntidade> repositorio)
        {
            this.repositorio = repositorio;
        }

        public void Alterar(TEntidade entidade)
        {
            repositorio.Alterar(entidade);
        }

        public void Excluir(object id)
        {
            repositorio.Excluir(id);
        }

        public void Excluir(TEntidade entidade)
        {
            repositorio.Excluir(entidade);
        }

        public TEntidade Incluir(TEntidade entidade)
        {
            return repositorio.Incluir(entidade);
        }

        public TEntidade SelecionarPorId(object id)
        {
            return repositorio.SelecionarPorId(id);
        }

        public IEnumerable<TEntidade> SelecionarTodos()
        {
            return repositorio.SelecionarTodos();
        }
    }
}
