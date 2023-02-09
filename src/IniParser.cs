using System.IO;
using System.Management.Automation;
using System.Text.RegularExpressions;

namespace ConvertIni
{
    /// <summary>
    /// 
    /// </summary>
    public class IniParser
    {

        /// <summary>
        /// Parses ini file contents into an Object
        /// </summary>
        /// <param name="iniString"></param>
        /// <returns></returns>
        public static object Parse(string iniString)
        {

            string section = string.Empty;

            PSObject result = new PSObject();

            using (StringReader reader = new StringReader(iniString))
            {

                // regex pattern for section [<section>]
                Regex iniSectionRgx = new Regex(@"^\[(.+)\]$");

                // regex pattern for entry <key>=<value>
                Regex iniEntryRgx = new Regex(@"^\s*([^#].+?)\s*=\s*(.*)");

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    // check if there is a match for section or entry in each line
                    var sectionMatch = iniSectionRgx.Match(line);
                    var entryMatch = iniEntryRgx.Match(line);

                    // if new section pattern "[<section>]", set section string
                    if (sectionMatch.Success)
                    {
                        section = sectionMatch.Groups[1].Value.Trim();
                    }
                    // if new entry pattern "<key>=<value>"
                    else if (entryMatch.Success)
                    {
                        // ignore lines commented with ;
                        if (entryMatch.Groups[1].Value.StartsWith(";"))
                        {
                            continue;
                        }

                        // set and trim key and value for entry
                        string iniEntryKey = entryMatch.Groups[1].Value.Trim();
                        string iniEntryValue = entryMatch.Groups[2].Value.Trim();

                        // simply add entry propeties to PSObject when there is no section
                        if (section == string.Empty)
                        {
                            result.Properties.Add(new PSNoteProperty(iniEntryKey, iniEntryValue));
                        }
                        else
                        {
                            // check if section is defined in object and initialize to new PSObject if not
                            if (result.Properties[section] == null)
                            {
                                result.Properties.Add(new PSNoteProperty(section, new PSObject()));
                            }

                            // add entry to section object
                            ((PSObject)result.Properties[section].Value).Properties.Add(new PSNoteProperty(iniEntryKey, iniEntryValue));
                        
                        }
                    }
                }
            }

            return result;
            
        }

    }
}
