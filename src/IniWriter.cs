using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Convert_Ini
{
    public class IniWriter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputObject"></param>
        /// <param name="compressed"></param>
        /// <returns></returns>
        public static string Write(PSObject inputObject, bool compressed = false)
        {

            string output = string.Empty;

            List<string> noSection = new List<string>();

            string room = compressed ? string.Empty : Environment.NewLine;

            foreach (PSNoteProperty item in inputObject.Properties)
            {

                if (item.Value.GetType() == typeof(PSObject))
                {

                    output += $"[{item.Name}]{Environment.NewLine}";

                    output += Write((PSObject)item.Value, compressed);

                }

                else
                {
                    noSection.Add($"{item.Name}={item.Value.ToString()}{Environment.NewLine}");
                }

            }

            // prepend items with no section
            output = $"{string.Join("", noSection)}{room}{output}";

            return output;

        }

    }
}
