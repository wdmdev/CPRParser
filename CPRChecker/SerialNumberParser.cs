using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRChecker
{
    public class SerialNumberParser : ISerialNumberParser
    {
        public int Parse(int yearEnding, int serialNumber)
        {
            if (serialNumber >= 0 && serialNumber <= 3)
            {
                if (yearEnding >= 0 && yearEnding <= 99)
                {
                    return 1900 + yearEnding;
                }
            }

            if (serialNumber == 4 || serialNumber == 9)
            {
                if (yearEnding >= 0 && yearEnding <= 36)
                {
                    return 2000 + yearEnding;

                }
                if (yearEnding >= 37 && yearEnding <= 99)
                {
                    return 1900 + yearEnding;

                }
            }

            if (serialNumber >= 5 && serialNumber <= 8)
            {
                if (yearEnding >= 0 && yearEnding <= 57)
                {
                    return 2000 + yearEnding;

                }
                if (yearEnding >= 58 && yearEnding <= 99)
                {
                    return 1800 + yearEnding;

                }
            }

            throw new ArgumentException("The given year and/or serial number was not recognized");
        }
    }
}
