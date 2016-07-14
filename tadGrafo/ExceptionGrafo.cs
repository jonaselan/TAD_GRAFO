using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadGrafo {
    class ExceptionGrafo : Exception
    {
        public ExceptionGrafo(string msg) : base(msg) {
            Console.WriteLine(msg);
        }  
    }
}
