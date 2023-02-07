using Microsoft.PowerShell;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
namespace Convert_Ini
{

    [Cmdlet(VerbsData.ConvertTo, "Ini")]
    [OutputType(typeof(string))]
    public class ConvertToIniCmdlet : PSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public dynamic InputObject { get; set; }


        protected override void ProcessRecord()
        {
            
            try
            {
                string jsonStr = JsonConvert.SerializeObject(InputObject, Formatting.None, new PSObjectConverter(typeof(PSObject)));
                Dictionary<string, dynamic> input = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonStr);
                WriteObject(IniConverter.__To(input));
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, "1", ErrorCategory.WriteError, InputObject));
            }


        }


        /// Not the most elegant solution. Would like to refactor this somehow.
        /// <summary>
        /// Retrieve corresponding key/value pairs from PSObject and convert to Dictionary<string, Hashtable>
        /// </summary>
        /// <param name="psObject"></param>
        /// <returns></returns>
        private static Dictionary<string, dynamic> PSObjectToDictionary(PSObject psObject)
        {
            
            Dictionary<string, dynamic> input = new Dictionary<string, dynamic>();

            var keys = (ICollection)psObject.Properties.Where(p => p.Name == "Keys").FirstOrDefault().Value;
            var vals = (ICollection)psObject.Properties.Where(p => p.Name == "Values").FirstOrDefault().Value;

            Hashtable kht = new Hashtable();
            
            int i = 0;
            foreach (var key in keys)
            {
                if (!input.ContainsKey(key.ToString()))
                    input.Add(key.ToString(), new Hashtable());

                kht[i] = key.ToString();
                i++;
            }


            i = 0;
            foreach (var val in vals)
            {
                if (val.GetType() == typeof(Hashtable))
                {

                    foreach (DictionaryEntry d in (Hashtable)val)
                    {
                        input[kht[i].ToString()].Add(d.Key, d.Value);
                    }

                }
                else if (val.GetType() == typeof(string))
                {
                    input[kht[i].ToString()] = val;
                }
                i++;
            }
            
            

            return input;
            
        }



        
    }

}
