using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_UTIL
{
    public class ValidadorDominio
    {
        private readonly List<string> _mensajesErrores;

        private ValidadorDominio()
        {
            _mensajesErrores = new List<string>();
        }

        public static ValidadorDominio Crear()
        {
            return new ValidadorDominio();
        }

        public ValidadorDominio When(bool p_NoPasaValidacion,string p_mensajeError)
        {
            if (p_NoPasaValidacion == false)
                _mensajesErrores.Add(p_mensajeError);
            return this;
        }


        //-------------------------------------------------------------------------------------
        public ValidadorDominio WhenIfTrueWhen(bool p_NoPasaValidacion_1, string p_mensajeError_1, bool p_NoPasaValidacion_2, string p_mensajeError_2)
        {
            if (p_NoPasaValidacion_1 == false)
            {
                _mensajesErrores.Add(p_mensajeError_1);
            }
            else
            {
                if (p_NoPasaValidacion_2 == false)
                {
                    _mensajesErrores.Add(p_mensajeError_2);
                }
            }
            return this;
        }
        //-------------------------------------------------------------------------------------
        public void Clear()
        {
            _mensajesErrores.Clear();
        }

        public void DispararExcecaoSeExistir()
        {
            if (_mensajesErrores.Any())
                throw new ExceptionDominio(_mensajesErrores);
        }
    }

   
}
