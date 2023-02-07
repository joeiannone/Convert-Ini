using System.IO;
using System.Management.Automation;
using System.Text.RegularExpressions;

namespace Convert_Ini
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

                        string iniEntryKey = entryMatch.Groups[1].Value.Trim();
                        string iniEntryValue = entryMatch.Groups[2].Value.Trim();

                        if (section == string.Empty)
                        {
                            result.Properties.Add(new PSNoteProperty(iniEntryKey, iniEntryValue));
                        }
                        else
                        {

                            if (result.Properties[section] == null)
                            {
                                result.Properties.Add(new PSNoteProperty(section, new PSObject()));
                            }

                            ((PSObject)result.Properties[section].Value).Properties.Add(new PSNoteProperty(iniEntryKey, iniEntryValue));
                        
                        }
                    }
                }
            }

            return result;
            
        }

    }
}
