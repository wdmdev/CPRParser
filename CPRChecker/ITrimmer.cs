using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRChecker
{
    /// <summary>
    /// Interface for CPR number trimming classes
    /// </summary>
    public interface ITrimmer
    {
        string Trim(string s);
    }
}
