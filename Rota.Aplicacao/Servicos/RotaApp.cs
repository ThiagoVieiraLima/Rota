using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using AutoMapper;
using Rota.Aplicacao.DTO;
using Rota.Aplicacao.Interfaces;
using Rota.Dominio.Entidades;
using Rota.Dominio.Interfaces.Servicos;

namespace Rota.Aplicacao.Servicos
{
    public class RotaApp : ServicoAppBase<Trecho, TrechoDTO>, IRotaApp
    {
        public RotaApp(IMapper iMapper, ICrudBase<Trecho> servico) : base(iMapper, servico)
        {
        }

        public IEnumerable<RotaDTO> Listar(string origem, string destino)
        {
            var l = servico.SelecionarTodos();

            List<Dominio.Entidades.Rota> lstRota = new List<Dominio.Entidades.Rota>();
            lstRota = ObterRota(l, origem.ToUpper(), origem.ToUpper(), destino.ToUpper()).ToList();
            
            ObterConexao(lstRota);
            ObterValor(lstRota);

            var lstRotaDTO = new List<RotaDTO>();
            foreach(var r in lstRota)
            {
                lstRotaDTO.Add(new RotaDTO()
                {
                    Id = r.Id, Conexao = r.Conexao, Valor = r.Valor
                });
            }

            return lstRotaDTO;
        }

        public IEnumerable<RotaDTO> Obter(string origem, string destino, Enumerador.Ordem ordem)
        {
            var l = Listar(origem, destino);
            return (ordem == Enumerador.Ordem.Quantidade ? l.OrderBy(r => r.Valor).OrderBy(r=>r.Conexao.Length).ToList() : l.OrderBy(r => r.Valor).ToList());
        }

        private string ObterConexao(IEnumerable<Dominio.Entidades.Rota> rotas)
        {
            string retorno = "";

            foreach (var r in rotas)
            {
                retorno = $"{r.Origem}";
                if (r.Rotas != null && r.Rotas.Count()  > 0)
                {
                    retorno += $" - {ObterConexao(r.Rotas)}";
                } else
                {
                    retorno += $" - {r.Destino}";
                }
                r.Conexao = retorno;
            }

            return retorno;
        }

        private double ObterValor(IEnumerable<Dominio.Entidades.Rota> rotas)
        {
            double retorno = 0;

            foreach (var r in rotas)
            {
                retorno = r.Valor;
                if (r.Rotas != null && r.Rotas.Count() > 0)
                {
                    retorno += ObterValor(r.Rotas);
                }
                r.Valor = retorno;
            }

            return retorno;
        }

        private IEnumerable<Dominio.Entidades.Rota> ObterRota(IEnumerable<Trecho> trechos, string origemInicial, string origem, string destino)
        {
            IList<Dominio.Entidades.Rota> retorno = new List<Dominio.Entidades.Rota>();
            var origens = trechos.Where(o => o.Origem.ToUpper() == origem.ToUpper());

            foreach (var o in origens)
            {
                var rota = new Dominio.Entidades.Rota()
                {
                    Id = o.Id,
                    Origem = o.Origem.ToUpper(),
                    Destino = o.Destino.ToUpper(),
                    Valor = o.Valor,
                    Conexao = $"{o.Origem.ToUpper()} - {o.Destino.ToUpper()}",
                    Rotas = new List<Dominio.Entidades.Rota>()
                };

                if (o.Destino.ToUpper() != destino.ToUpper() && o.Destino.ToUpper() != origemInicial.ToUpper())
                {
                    rota.Rotas = ObterRota(trechos, origemInicial, o.Destino.ToUpper(), destino.ToUpper());
                }

                if (o.Destino.ToUpper() != origemInicial.ToUpper())
                {
                    if (rota.Rotas.Count() > 1)
                    {
                        foreach (var rr in rota.Rotas)
                        {
                            retorno.Add(new Dominio.Entidades.Rota()
                            {
                                Id = o.Id,
                                Origem = o.Origem.ToUpper(),
                                Destino = o.Destino.ToUpper(),
                                Valor = o.Valor,
                                Conexao = $"{o.Origem.ToUpper()} - {o.Destino.ToUpper()}",
                                Rotas = new List<Dominio.Entidades.Rota>() { rr }
                            });
                        }
                    }
                    else
                    {
                        retorno.Add(rota);
                    }
                }
            }

            return retorno;
        }
    }
}
