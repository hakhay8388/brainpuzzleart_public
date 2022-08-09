using System;
using System.Collections.Generic;
//using System.Deployment.Application;
using System.Text.RegularExpressions;


namespace Base.Core.nApplication.nConfiguration.nStartParameter
{
    public class cStartParameterController
    {
        public cConfiguration Configuration { get; set; }
        public Dictionary<string, string> ParameterList;
        public cStartParameterController(cConfiguration _Configuration)
        {
            Configuration = _Configuration;
            ParameterList = new Dictionary<string, string>();
            ParseParameters();
        }
        

        private void ParseParameters()
        {
            string __FullString = GetFullString();
            Regex __Regex = new Regex("{[^}]*}");
            MatchCollection __MatchCollection = __Regex.Matches(__FullString); 
            foreach (Match __Match in __MatchCollection)
            {
                Regex __ParamName = new Regex("[^=]*");
                Regex __ParamValue = new Regex("'[^']*'");
                Match __ParamNameMatch = __ParamName.Match(__Match.Value.Substring(1));
                Match __ParamValueMatch = __ParamValue.Match(__Match.Value);
                if (__ParamNameMatch.Success && __ParamValueMatch.Success)
                {
                    string __ParameterNameString = __ParamNameMatch.Value;
                    string __ParameterValueString = __ParamValueMatch.Value.Substring(1);
                    __ParameterValueString = __ParameterValueString.Substring(0, __ParameterValueString.Length - 1);
                    ParameterList.Add(__ParameterNameString, __ParameterValueString);
                }
                else
                {
                    throw new Exception("Geçersiz Parametre : " + __Match.Value);
                }
            }
        }

        private string GetFullString()
        {
            return Environment.CommandLine;

            /*String __Result = "";
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                __Result = ApplicationDeployment.CurrentDeployment.ActivationUri.Query.Replace("?", "");
                __Result = System.Uri.UnescapeDataString(__Result);
                return __Result;
            }
            else
            {
                return Environment.CommandLine;
                // return args without exe name
                string[] __Args = Environment.GetCommandLineArgs();
                string __Value = string.Empty;

                for (int i = 1; i < __Args.Length; i++)
                {
                    __Value += __Args[i] + " ";
                }

                return __Value.Trim();
            }*/
        }
    }
}
