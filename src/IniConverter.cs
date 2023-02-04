using System;
using System.Collections;
using System.Collections.Generic;

namespace Convert_Ini
{
    /// <summary>
    /// 
    /// </summary>
    public static class IniConverter
    {
        /// <summary>
        /// Converts inputObject to ini file string
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public static string To(Dictionary<string, Hashtable> inputObject)
        {
            string output = string.Empty;

            foreach (var section in inputObject.Keys)
            {
                if (section != "_")
                {
                    output += $"[{section}]{Environment.NewLine}";
                }
                foreach (DictionaryEntry item in inputObject[section])
                {
                    if (section != "_")
                        output += $"{item.Key}={item.Value}{Environment.NewLine}";
                    else
                        output = $"{item.Key}={item.Value}{Environment.NewLine}{output}";
                }
                
            }

            return output;
        }

        /// <summary>
        /// Converts ini file contents into a Dictionary
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public static Dictionary<string, Hashtable> From(string inputObject)
        {
            return IniParser.Parse(inputObject);
        }

    }
}
