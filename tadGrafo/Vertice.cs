using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadGrafo
{
    class Vertice
    {
        private int index;
        private int value;

        public Vertice(int index, int value) {
            this.index = index;
            this.value = value;
        }

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                value = value;
            }
        }

    }
}
