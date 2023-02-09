using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace ConvertIni
{
    public class IniWriter
    {
        /// <summary>
        /// Convert PSObject to ini string
        /// </summary>
        /// <param name="inputObject"></param>
        /// <param name="compressed"></param>
        /// <returns></returns>
        public static string Write(PSObject inputObject, bool compressed = false)
        {

            string output = string.Empty;

            // initialize an empty list to put entries with no section
            List<string> noSection = new List<string>();

            // determine if extra new lines will be used based on compressed flag
            string room = compressed ? string.Empty : Environment.NewLine;

            // iterate through properties
            foreach (PSNoteProperty item in inputObject.Properties)
            {
                // make sure item value is set to empty string when null
                var itemValue = (item.Value == null) ? string.Empty : item.Value;

                // when the current item is an object
                if (itemValue.GetType() == typeof(PSObject))
                {
                    // set section
                    output += $"[{item.Name}]{Environment.NewLine}";

                    // recursive call for child object
                    output += Write((PSObject)itemValue, compressed);

                }

                else
                {
                    // add entry to no section list to be dealt with later
                    noSection.Add($"{item.Name}={itemValue.ToString()}{Environment.NewLine}");
                }

            }

            // prepend items with no section
            output = $"{string.Join("", noSection)}{room}{output}";

            return output;

        }

    }
}
