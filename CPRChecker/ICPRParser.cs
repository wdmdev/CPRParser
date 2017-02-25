using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRChecker
{
    public interface ICPRParser
    {
        string Parse(string cpr);
        DateTime GetBirthDate(string cpr);
        long GetGender(string cpr);
    }
}
