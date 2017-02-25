using CPRChecker;
using System;

namespace CPRChecker
{
    public class DynamicsCPRValidator : IValidator
    {
        private readonly ISerialNumberParser _serialNumberParser;

        public DynamicsCPRValidator(ISerialNumberParser serialNumberParser)
        {
            _serialNumberParser = serialNumberParser;
        }

        public bool IsValid(string cpr)
        {
            if (cpr == null) throw new ArgumentNullException(nameof(cpr), "Cannot be null");

            return IsLengthValid(cpr) && IsDateValid(cpr);
        }

        private bool IsLengthValid(string cpr)
        {
            return cpr.Length == 10;
        }

        private bool IsDateValid(string cpr)
        {
            var day = int.Parse(cpr.Substring(0, 2));
            var month = int.Parse(cpr.Substring(2, 2));
            var yearEnding = int.Parse(cpr.Substring(4, 2));
            var serialNumber = int.Parse(cpr.Substring(6, 1));

            var year = _serialNumberParser.Parse(yearEnding, serialNumber);

            return day > 0 && day <= DateTime.DaysInMonth(year, month);
        }
    }
}
