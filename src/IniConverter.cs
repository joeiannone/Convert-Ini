using System.Management.Automation;

namespace ConvertIni
{
    /// <summary>
    /// Handles ini file conversion to and from PSObject
    /// </summary>
    public class IniConverter
    {

        /// <summary>
        /// Parses ini file contents and converts to a PSObject
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public object From(string inputObject)
        {
            return IniParser.Parse(inputObject);
        }


        /// <summary>
        /// Converts a PSObject into ini file string
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public string To(PSObject inputObject)
        {
            return IniWriter.Write(inputObject);
        }

    }
}
