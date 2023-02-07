using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Convert_Ini
{
    public class IniWriter
    {
        public static string Write(Hashtable inputObject)
        {
            string output = string.Empty;
            
            foreach (var section in inputObject.Keys)
            {
                bool isSection = inputObject[section] is ICollection;

                if (isSection)
                {
                    output += $"[{section}]{Environment.NewLine}";

                    foreach (var key in ((Hashtable)inputObject[section]).Keys)
                    {

                        output += $"{key}={((Hashtable)inputObject[section])[key].ToString()}{Environment.NewLine}";

                    }
                }
                else
                {
                    output = $"{section}={inputObject[section].ToString()}{Environment.NewLine}{output}";
                }

            }
            
            return output;
        }
    }
}
