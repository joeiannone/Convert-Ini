using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;

namespace Convert_Ini
{

    [Cmdlet(VerbsData.ConvertFrom, "Ini")]
    [OutputType(typeof(Dictionary<string, dynamic>))]
    public class ConvertFromIniCmdlet : PSCmdlet
    {

        private IniConverter _converter;
        private List<string> _inputBuffer;

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string InputObject { get; set; } = string.Empty;

        /**
         * Initialize
         */
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            _converter = new IniConverter();
            _inputBuffer = new List<string>();
        }

        /**
         * Process each data in pipeline
         */
        protected override void ProcessRecord()
        {
            // Add each pipeline data to input buffer
            _inputBuffer.Add(InputObject);
        }

        /**
         * Convert ini and write out
         */
        protected override void EndProcessing()
        {
            base.EndProcessing();
            
            WriteObject(_converter.From(string.Join($"{Environment.NewLine}", _inputBuffer)));
        }

    }
}
