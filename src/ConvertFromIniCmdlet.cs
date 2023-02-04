using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace Convert_Ini
{

    [Cmdlet(VerbsData.ConvertFrom, "Ini")]
    [OutputType(typeof(Dictionary<string, Hashtable>))]
    public class ConvertFromIniCmdlet : PSCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string InputObject { get; set; } = string.Empty;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            Console.WriteLine("Process call");
            WriteObject(IniConverter.From(InputObject));
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

    }
}
