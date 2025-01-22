using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Rota.Dominio.Entidades;

namespace Rota.Infra.IoC.Validacao
{
    public class Validacao<TEntidade> where TEntidade : new()
    {
        public Validacao(TEntidade entidade)
        {
            var validator = new DataAnnotationsValidator.DataAnnotationsValidator();
            var validationResults = new List<ValidationResult>();
            validator.TryValidateObjectRecursive(entidade, validationResults);

            throw new ValidacaoException(validationResults.Join(", "));
        }
    }

}
