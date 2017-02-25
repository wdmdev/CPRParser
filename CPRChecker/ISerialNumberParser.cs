using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRChecker
{
    public interface ISerialNumberParser
    {
        int Parse(int yearEnding, int serialNumber);
    }
}
