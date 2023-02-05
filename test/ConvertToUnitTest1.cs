using System.Collections;

namespace Convert_Ini.Test
{
    public class ConvertToTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            string testIniFilepath = "C:\\Users\\josep\\DotNetProjects\\Convert-Ini\\Convert-Ini.Test\\testfiles\\test-002.ini";
            var t = IniConverter.From(File.ReadAllText(testIniFilepath));

            foreach (var dkey in t.Keys)
            {
                Console.WriteLine(dkey);

                foreach (DictionaryEntry ht in t[dkey]) {

                    Console.WriteLine($"{ht.Key} = {ht.Value}");
                    
                }
                
                
                Console.WriteLine("-----");
            }
            Assert.Pass();
        }
    }
}