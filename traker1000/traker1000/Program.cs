using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traker1000
{
    class Program
    {
        static void Main(string[] args)
        {
            TrakerBD.CreateDatabase();
            TrakerBD.CreateTables();
        }
    }
}
