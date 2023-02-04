using Microsoft.PowerShell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public PSObject InputObject { get; set; }


        protected override void ProcessRecord()
        {            
            WriteObject(IniConverter.To(PSObjectToDictionary(InputObject)));
        }


        /// Not the most elegant solution. Would like to refactor this somehow.
        /// <summary>
        /// Retrieve corresponding key/value pairs from PSObject and convert to Dictionary<string, Hashtable>
        /// </summary>
        /// <param name="psObject"></param>
        /// <returns></returns>
        private static Dictionary<string, Hashtable> PSObjectToDictionary(PSObject psObject)
        {
            Dictionary<string, Hashtable> input = new Dictionary<string, Hashtable>();

            var keys = (ICollection)psObject.Properties.Where(p => p.Name == "Keys").FirstOrDefault().Value;
            var vals = (ICollection)psObject.Properties.Where(p => p.Name == "Values").FirstOrDefault().Value;

            // initialize input Dictionary with keys

            Hashtable kht = new Hashtable();

            int i = 0;
            foreach (var key in keys)
            {
                if (!input.ContainsKey(key.ToString()))
                    input.Add(key.ToString(), new Hashtable());

                kht[i] = key.ToString();
                i++;
            }

            if (!input.ContainsKey("_"))
                input.Add("_", new Hashtable());


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
                    input.Remove(kht[i].ToString());
                    input["_"].Add(kht[i], val);
                }
                i++;
            }

            return input;
        }



        
    }

}
