using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolsLanguage.Classes
{
    public static class Extensions
    {
        /// <summary>
        /// Сравнивает строки по символьно в соотвествии с их индексом.
        /// </summary>
        /// <param name="compared1">Сравниваемая строка</param>
        /// <param name="compared2">Сравниваемая строка</param>
        /// <returns></returns>
        public static bool isMatch(this string compared1, string compared2)
        {
            int countChar = compared2.Length;

            if (compared2 == "")
                return true;

            for (int i = 0; i < countChar; i++)
            {
                if (compared2[i] == compared1[i])
                    continue;
                else
                    return false;
            }

            return true;
        }

    }
}
