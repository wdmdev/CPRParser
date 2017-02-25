using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CPRChecker
{
    /// <summary>
    /// Class for trimming CPR numbers
    /// </summary>
    public class DynamicsCPRTrimmer : ITrimmer
    {
        public string Trim(string cpr)
        {
            if (cpr == null) throw new ArgumentNullException(nameof(cpr), "Cannot be null");
            if (cpr == string.Empty) throw new ArgumentException("Cannot be an empty string", nameof(cpr));

            var noCharactersCPR = TrimCharacters(cpr);

            var noDecimalsCPR = TrimDecimals(noCharactersCPR);

            return noDecimalsCPR;
        }

        //Removes all symbols in cpr besides digits . and ,
        private string TrimCharacters(string cpr)
        {
            var matches = Regex.Matches(cpr, "[0-9|.|,]");

            var trimmedCPR = "";

            foreach (Match m in matches)
            {
                trimmedCPR += m.Value;
            }

            return trimmedCPR;
        }

        //Removes every symbol after a . og , in the cpr string
        private string TrimDecimals(string cpr)
        {
            var match = Regex.Match(cpr, @"\.[\d,]*|,[\d.]*");

            return cpr.Remove(match.Index, match.Value.Length);
        }
    }
}
