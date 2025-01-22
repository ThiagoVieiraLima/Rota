using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using Rota.Aplicacao.DTO;
using Rota.Aplicacao.Interfaces;
using Rota.Dominio.Entidades;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace Rota.Servicos.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RotaController : Controller
    {
        readonly protected IRotaApp app;

        /// <summary>
        /// API para consulta da melhor Rota
        /// </summary>
        /// <param name="app"></param>
        public RotaController(IRotaApp app)
        {
            this.app = app;
        }

        /// <summary>
        /// Lista as Rotas com base na ordem solicitada.
        /// </summary>
        /// <param name="ordem">Ordenação do resultado.</param>
        /// <returns>Dados do Trecho referente ao ID informado.</returns>
        [HttpGet]
        [Route("{ordem}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ResumoRotaDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult Obter(string origem, string destino, Dominio.Entidades.Enumerador.Ordem ordem)
        {
            try
            {
                var e = app.Obter(origem, destino, ordem);
                var r = e.FirstOrDefault();
                var retorno = new ResumoRotaDTO()
                {
                    Id = r.Id,
                    Conexao = r.Conexao,
                    Valor = r.Valor,
                    Rotas = e
                };

                return new OkObjectResult(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
