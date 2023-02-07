
namespace Convert_Ini
{
    /// <summary>
    /// 
    /// </summary>
    public class IniConverter
    {

        public IniConverter()
        {
            
        }

        /// <summary>
        /// Converts ini file contents into a Dictionary
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public object From(string inputObject)
        {
            return IniParser.Parse(inputObject);
        }

    }
}
