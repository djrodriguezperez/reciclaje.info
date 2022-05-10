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

            str = str.Replace("@", "_"); ///Json-LD Deserialize
            return Regex.Replace(str, @"\t|\n|\r", ""); // Clean especial characters

            
        }

        

    }
}
