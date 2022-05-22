using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Reciclaje.Info.Shared.Utils
{
    public static class ExtensionMethods
    {
        public static string NormalizarJson(this string str)
        {
            ///Json Deserialize
            str = str.Replace("@", "_");
            
            return Regex.Replace(str, @"\t|\n|\r", "");             
        }

        public static string LimpiarDatos(this string str)
        {
            // Elimina Caracteres Especiales de la cadena 
            return Regex.Replace(str, @"[^\w\.@-]", "",RegexOptions.None);            
        }


    }



   
}
