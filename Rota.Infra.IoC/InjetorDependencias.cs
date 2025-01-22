using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Rota.Aplicacao.Interfaces;
using Rota.Aplicacao.Servicos;
using Rota.Dominio.Interfaces.Repositorio;
using Rota.Dominio.Interfaces.Servicos;
using Rota.Dominio.Servicos;
using Rota.Infra.Data.Repositorios;

namespace Rota.Infra.IoC
{
    public class InjetorDependencias
    {
        public static void Registrar(IServiceCollection svcCollection)
        {
            //Aplicação
            svcCollection.AddScoped(typeof(IAppBase<,>), typeof(ServicoAppBase<,>));
            svcCollection.AddScoped(typeof(ICrudAppBase<,>), typeof(CrudAppBase<,>));
            svcCollection.AddScoped<ITrechoApp, TrechoApp>();
            svcCollection.AddScoped<IRotaApp, RotaApp>();

            //Domínio
            svcCollection.AddScoped(typeof(ICrudBase<>), typeof(CrudBase<>));
            svcCollection.AddScoped<ITrechoServico, TrechoServico>();

            //Repositorio
            svcCollection.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
            svcCollection.AddScoped<ITrechoRepositorio, TrechoRepositorio>();
        }
    }
}
