using System;

namespace Negocio.Excepciones
{
  
    public class ExcepcionElementoNoExiste : Exception
    {
        public ExcepcionElementoNoExiste() : base() { }

        public ExcepcionElementoNoExiste(string message) : base(message) { }

        public ExcepcionElementoNoExiste(string message, Exception innerException) : base(message, innerException) { }
    }
}
