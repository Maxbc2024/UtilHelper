using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_UTIL
{
    public class ExceptionDominio : ArgumentException
    {
        public List<string> MensagesErrores { get; set; }

        public ExceptionDominio(List<string> p_mensagesErrores)
        {
            MensagesErrores = p_mensagesErrores;
        }
    }


  
}
