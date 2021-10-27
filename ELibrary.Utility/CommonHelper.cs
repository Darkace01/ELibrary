using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Utility
{
    public class CommonHelper
    {
        public static List<string> ConvertStringToList(string strings, char seperator)
        {
            if (string.IsNullOrEmpty(strings) || string.IsNullOrWhiteSpace(strings))
                return new List<string>();
            return strings.Split(seperator).ToList();
        }
    }
}
