using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Convert_Ini
{
    /// <summary>
    /// 
    /// </summary>
    public static class IniParser
    {
        /// <summary>
        /// Parses ini file contents into a Dictionary
        /// </summary>
        /// <param name="iniString"></param>
        /// <returns></returns>
        public static Dictionary<string, Hashtable> Parse(string iniString)
        {

            Dictionary<string, Hashtable> result = new Dictionary<string, Hashtable>();
            string section = "_";
            using (StringReader reader = new StringReader(iniString))
            {
                // default section when none is specified

                Regex iniSectionRgx = new Regex(@"^\[(.+)\]$");
                Regex iniEntryRgx = new Regex(@"^\s*([^#].+?)\s*=\s*(.*)");

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var sectionMatch = iniSectionRgx.Match(line);
                    var entryMatch = iniEntryRgx.Match(line);

                    if (sectionMatch.Success)
                    {
                        section = sectionMatch.Groups[1].Value.Trim();
                    }

                    else if (entryMatch.Success) 
                    {
                        
                        if (entryMatch.Groups[1].Value.StartsWith(";"))
                        {
                            continue;

                        }

                        if (!result.ContainsKey(section))
                        {
                            result.Add(section, new Hashtable()); 
                        }

                        string iniEntryKey = entryMatch.Groups[1].Value.Trim();
                        string iniEntryValue = entryMatch.Groups[2].Value.Trim();

                        if (result[section].ContainsKey(iniEntryKey))
                        {
                            // override assignment when there is a duplicate key
                            result[section][iniEntryKey] = iniEntryValue;
                        }
                        else
                        {
                            result[section].Add(iniEntryKey, iniEntryValue);
                        }
                        
                    }
                }
            }

            
            return result;
        }
    }
}
