using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;
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
            _section = string.Empty;
        }

        public Dictionary<string, dynamic> ThisIsATest(string iniString)
        {
            Console.WriteLine($"Input: {iniString}");
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            
            return result;
            
        }


        /// <summary>
        /// Parses ini file contents into a Dictionary
        /// </summary>
        /// <param name="iniString"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> Parse(string iniString)
        {

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();

            using (StringReader reader = new StringReader(iniString))
            {
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

                        string iniEntryKey = entryMatch.Groups[1].Value.Trim();
                        string iniEntryValue = entryMatch.Groups[2].Value.Trim();

                        if (_section == string.Empty)
                        {
                            if (result.ContainsKey(iniEntryKey))
                                result[iniEntryKey] = iniEntryValue;
                            else
                                result.Add(iniEntryKey, iniEntryValue);
                        }
                        else {

                            if (!result.ContainsKey(_section))
                                result.Add(_section, new Hashtable());


                            if (result[_section].ContainsKey(iniEntryKey))
                            {
                                result[_section][iniEntryKey] = iniEntryValue;
                            }
                            else
                            {
                                result[_section].Add(iniEntryKey, iniEntryValue);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
