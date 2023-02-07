using Newtonsoft.Json.Linq;
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

  
            foreach (var section in inputObject)
            {
                bool hasSection = inputObject[section.Key].GetType() != typeof(string);

                if (hasSection)
                {
                    output += $"[{section}]{Environment.NewLine}";
                    Console.WriteLine(inputObject[section.Key].GetType());
                    foreach (JProperty item in inputObject[section.Key])
                    {
                        
                        output += $"{item.Name}={item.Value}{Environment.NewLine}";
                        
                    }
                }
                else
                {
                    output = $"{section}={inputObject[section.Key]}{Environment.NewLine}{output}";
                }
                
            }

            return output;
        }


        public static string __To(Dictionary<string, dynamic> inputObject)
        {
            string output = string.Empty;

            Console.WriteLine(inputObject.Count);
            int i = 0;
            foreach (var section in inputObject)
            {
                Console.WriteLine($"{section.Key} : {section.Value.GetType()}");

                if (section.Key == "Keys")
                {
                    foreach (var ja in (JArray)section.Value)
                    {
                        
                    }
                }
                
            }
            

            return output;
        }

    }
}
