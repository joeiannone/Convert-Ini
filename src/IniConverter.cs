using System;
using System.Collections;
using System.Collections.Generic;

namespace Convert_Ini
{
    /// <summary>
    /// 
    /// </summary>
    public class IniConverter
    {
        private IniParser _parser;

        public IniConverter()
        {
            _parser = new IniParser();
        }


        /// <summary>
        /// Converts ini file contents into a Dictionary
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> From(string inputObject)
        {
            return _parser.Parse(inputObject);
        }


        /// <summary>
        /// Converts inputObject to ini file string
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public static string To(Dictionary<string, dynamic> inputObject)
        {
            string output = string.Empty;

            foreach (var section in inputObject.Keys)
            {
                bool hasSection = inputObject[section].GetType() == typeof(Hashtable);


                if (hasSection)
                {
                    output += $"[{section}]{Environment.NewLine}";
                
                    foreach (DictionaryEntry item in inputObject[section])
                    {
                        
                        output += $"{item.Key}={item.Value}{Environment.NewLine}";
                        
                    }
                }
                else
                {
                    output = $"{section}={inputObject[section]}{Environment.NewLine}{output}";
                }
                
            }

            return output;
        }

    }
}
