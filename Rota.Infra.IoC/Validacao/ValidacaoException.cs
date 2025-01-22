using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Rota.Infra.IoC.Validacao
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException(string message) : base(message) { }

        protected ValidacaoException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }
}
