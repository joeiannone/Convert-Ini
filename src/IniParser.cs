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
    public class IniParser
    {
        private string _section { get; set; }
        public IniParser() 
        {
            _section = "_";
        }
        /// <summary>
        /// Parses ini file contents into a Dictionary
        /// </summary>
        /// <param name="iniString"></param>
        /// <returns></returns>
        public Dictionary<string, Hashtable> Parse(string iniString)
        {

            Dictionary<string, Hashtable> result = new Dictionary<string, Hashtable>();

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
                        _section = sectionMatch.Groups[1].Value.Trim();
                    }

                    else if (entryMatch.Success) 
                    {
                        
                        if (entryMatch.Groups[1].Value.StartsWith(";"))
                        {
                            continue;

                        }

                        if (!result.ContainsKey(_section))
                        {
                            result.Add(_section, new Hashtable()); 
                        }

                        string iniEntryKey = entryMatch.Groups[1].Value.Trim();
                        string iniEntryValue = entryMatch.Groups[2].Value.Trim();

                        if (result[_section].ContainsKey(iniEntryKey))
                        {
                            // override assignment when there is a duplicate key
                            result[_section][iniEntryKey] = iniEntryValue;
                        }
                        else
                        {
                            result[_section].Add(iniEntryKey, iniEntryValue);
                        }
                        
                    }
                }
            }

            
            return result;
        }
    }
}
