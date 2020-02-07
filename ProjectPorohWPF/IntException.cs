using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPorohWPF
{
    class IntException : Exception
    {
        public IntException(int code)
        {
            if(code == 0)
            {

            }
        }
    }
}
