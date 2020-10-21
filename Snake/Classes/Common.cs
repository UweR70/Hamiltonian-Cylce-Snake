using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Classes
{
    public class Common
    {
        public static bool IsValueEven(int value)
        {
            // "true" for 0, 2, 4 ,6, ...
            return value % 2 == 0;
        }
    }
}
