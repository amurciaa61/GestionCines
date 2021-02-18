using System;

namespace GestionCines
{
    public class MisExcepciones : Exception
    {
        public MisExcepciones(string message) : base(message)
        {
        }
    }
}
