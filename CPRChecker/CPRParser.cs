using CPRChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRChecker
{
    public class CPRParser : ICPRParser
    {
        private ITrimmer _trimmer;
        private IValidator _validator;
        private ISerialNumberParser _serialParser;

        public CPRParser(
            ITrimmer trimmer, IValidator validator, ISerialNumberParser serialParser)
        {
            _trimmer = trimmer;
            _validator = validator;
            _serialParser = serialParser;
        }

        public string Parse(string cpr)
        {
            try
            {
                var trimmedCpr = _trimmer.Trim(cpr);

                if (_validator.IsValid(trimmedCpr)) return trimmedCpr;
                else return null;
            }
            catch(Exception)
            {
                return null;
            }
        }

        //Extracting Birthdate from CPR number
        public DateTime GetBirthDate(string cpr)
        {
            var trimmedCpr = Parse(cpr);

            if (string.IsNullOrEmpty(trimmedCpr))
                throw new ArgumentException("The cpr number was invalid", nameof(cpr));

            var day = int.Parse(cpr.Substring(0, 2));
            var month = int.Parse(cpr.Substring(2, 2));
            var yearEnding = int.Parse(cpr.Substring(4, 2));
            var serialNumber = int.Parse(cpr.Substring(6, 1));
            var birthYear = _serialParser.Parse(yearEnding, serialNumber);

            return new DateTime(birthYear, month, day);

        }

        public long GetGender(string cpr)
        {
            if (cpr == null)
                throw new ArgumentNullException(nameof(cpr), "Cannot be null");

            var trimmedCpr = Parse(cpr);
            long cprNumber;

            if (trimmedCpr == string.Empty || !long.TryParse(trimmedCpr, out cprNumber))
                throw new ArgumentException("The cpr number was invalid", nameof(cpr));

            return cprNumber % 2;
        }
    }
}
