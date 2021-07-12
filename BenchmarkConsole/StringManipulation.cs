using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkConsole
{

    public class StringManipulation
    {
        public bool CheckIfStringIsEqual(string source, string destination)
        {
            return source == destination;
        }

        public bool CheckIfStringIsEqualInBuilt(string source, string destination)
        {
            return string.Equals(source, destination);
        }
    }
}
