using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Rota.Aplicacao.DTO;
using Rota.Aplicacao.Interfaces;
using Rota.Dominio.Entidades;
using Swashbuckle.AspNetCore.Annotations;

namespace Rota.Servicos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TrechoController : Controller
    {
        readonly protected ITrechoApp app;

        /// <summary>
        /// API para consulta e manutenção dos trechos de viagens.
        /// </summary>
        /// <param name="app"></param>
        public TrechoController(ITrechoApp app)
        {
            this.app = app;
        }

        /// <summary>
        /// API destinada para dar a uma carga inicial.
        /// </summary>
        /// <returns>True, False</returns>
        [HttpGet]
        [Route("CargaInicial")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso", typeof(bool))]
        public IActionResult CargaInicial()
        {
            try
            {
                var o = app.SelecionarTodos();
                foreach (var item in o) { app.Excluir(item.Id); }

                app.Incluir(new TrechoDTO() { Id = 1, Origem = "GRU", Destino = "BRC", Valor = 10 });
                app.Incluir(new TrechoDTO() { Id = 2, Origem = "BRC", Destino = "SCL", Valor = 5 });
                app.Incluir(new TrechoDTO() { Id = 3, Origem = "GRU", Destino = "CDG", Valor = 75 });
                app.Incluir(new TrechoDTO() { Id = 4, Origem = "GRU", Destino = "SCL", Valor = 20 });
                //app.Incluir(new TrechoDTO() { Id = 5, Origem = "GRU", Destino = "ORL", Valor = 56 });
                app.Incluir(new TrechoDTO() { Id = 6, Origem = "ORL", Destino = "CDG", Valor = 5 });
                app.Incluir(new TrechoDTO() { Id = 7, Origem = "SCL", Destino = "ORL", Valor = 20 });
                app.Incluir(new TrechoDTO() { Id = 8, Origem = "CDG", Destino = "ORL", Valor = 13 });
                app.Incluir(new TrechoDTO() { Id = 9, Origem = "CDG", Destino = "GRU", Valor = 11.25 });
                app.Incluir(new TrechoDTO() { Id = 10, Origem = "CDG", Destino = "SCL", Valor = 9.14 });

                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Lista todos o trechos de viagens cadastrados.
        /// </summary>
        /// <returns>Lista com todos os Trechos.</returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso", typeof(IEnumerable<TrechoDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Valores da requisição inválidos", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Registro não encontrado", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor.", typeof(string[]))]
        public IActionResult Listar()
        {
            try
            {
                var o = app.SelecionarTodos();
                if (o == null || o.Count() <= 0)
                {
                    return NotFound(new string[] { "Registro não encontrado." });
                }
                else
                {
                    return new OkObjectResult(o);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Lista o trecho de viagens especificado com o Id.
        /// </summary>
        /// <param name="id">Código de identificação do Trecho.</param>
        /// <returns>Dados do Trecho referente ao ID informado.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso", typeof(TrechoDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Valores da requisição inválidos", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Registro não encontrado", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor.", typeof(string[]))]
        public IActionResult SelecionarPorId([Required] int id)
        {
            try
            {
                if (id <= 0)
                { return BadRequest(new string[] { "O campo Id precisa ser informado." }); }

                var o = app.SelecionarPorId(id);

                if (o == null || string.IsNullOrEmpty(o.Origem))
                {
                    return NotFound(new string[] { "Registro não encontrado." });
                }
                else
                {
                    return new OkObjectResult(o);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Efetua o cadastro do trecho de viagem.
        /// </summary>
        /// <param name="dado">Dados do Trecho a ser cadastrado.</param>
        /// <returns>Mensagem de retorno</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Valores da requisição inválidos", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Registro não encontrado", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor.", typeof(string[]))]
        public IActionResult Incluir([FromBody][Required] TrechoDTO dado)
        {
            try
            {
                if (dado == null)
                { return BadRequest(new string[] { "Necessário informar os valores corretamente." }); }

                var error = new Infra.IoC.Validacao.TrechoValidacao().Validate(dado);
                if (!error.IsValid) { return BadRequest(error.Errors.Select(e => e.ErrorMessage).ToArray()); }

                var o = app.Incluir(dado);
                if (o == null || string.IsNullOrEmpty(o.Origem))
                {
                     return NotFound(new string[] { "Registro não encontrado." }); 
                }
                else
                {
                    return new OkObjectResult(new string[] { $"Sucesso - ID: {o.Origem}" });
                }
            }
            catch (Exception ex)
            {
                if (ex.Source == "Microsoft.EntityFrameworkCore.Relational")
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        /// <summary>
        /// Atualiza as informações do trecho de viagem.
        /// </summary>
        /// <param name="dado">Dados do Trecho a ser alterado.</param>
        /// <returns>Mensagem de retorno</returns>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Valores da requisição inválidos", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Registro não encontrado", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor.", typeof(string[]))]
        public IActionResult Alterar([FromBody][Required] TrechoDTO dado)
        {
            try
            {
                var error = new Infra.IoC.Validacao.TrechoValidacao().Validate(dado);
                if (!error.IsValid) { return BadRequest(error.Errors.Select(e => e.ErrorMessage).ToArray()); }

                var e = app.SelecionarPorId(dado.Id);
                if (e == null || e.Id <= 0)
                { return NotFound(new string[] { "Registro não encontrado." }); }

                app.Alterar(dado);
                return new OkObjectResult(new string[] { "Sucesso" });
            }
            catch (Exception ex)
            {
                if (ex.Source == "Microsoft.EntityFrameworkCore.Relational")
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        /// <summary>
        /// Detela o trecho de viagem.
        /// </summary>
        /// <param name="id">Código de identificação do Trecho.</param>
        /// <returns></returns>
        /// <returns>Mensagem de retorno</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Sucesso", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Valores da requisição inválidos", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Registro não encontrado", typeof(string[]))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor.", typeof(string[]))]
        public IActionResult Excluir([Required] int id)
        {
            try
            {
                if (id <= 0)
                { return BadRequest(new string[] { "O campo Id precisa ser preenchido." }); }

                var e = app.SelecionarPorId(id);
                if (e == null || e.Id <= 0)
                { return NotFound(new string[] { "Registro não encontrado." }); }

                app.Excluir(id);
                return new OkObjectResult(new string[] { "Sucesso" });
            }
            catch (Exception ex)
            {
                if (ex.Source == "Microsoft.EntityFrameworkCore.Relational")
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
    }
}
