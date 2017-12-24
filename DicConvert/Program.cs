using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DicConvert
{
    class Program
    {

        static void Main(string[] args)
        {

            var dic = new Convert(args);
            dic.Start();
        }

    }

   
}
