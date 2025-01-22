using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Rota.Aplicacao.DTO;
using Rota.Dominio.Entidades;

namespace Rota.Infra.IoC.Validacao
{
    public class TrechoValidacao : AbstractValidator<TrechoDTO>
    {
        public TrechoValidacao()
        {
            RuleFor(c => c.Origem)
                .NotEmpty().WithMessage("Informe a Sigla de Origem.")
                .NotNull().WithMessage("Informe a Sigla de Origem.")
                .Length(1, 3).WithMessage("Informe a Sigla de Origem com até 3 caracteres.");

            RuleFor(c => c.Destino)
                .NotEmpty().WithMessage("Informe a Sigla de Destino.")
                .NotNull().WithMessage("Informe a Sigla de Destino.")
                .Length(1, 3).WithMessage("Informe a Sigla de Destino com até 3 caracteres.");

            RuleFor(c => c.Valor)
                .NotEmpty().WithMessage("Informe o valor do trecho.")
                .NotNull().WithMessage("Informe o valor do trecho.")
                .GreaterThan(0).WithMessage("Valor do trecho muito pequeno.")
                .LessThan(999999).WithMessage("Valor do trecho muito grande.");
        }
    }
}
