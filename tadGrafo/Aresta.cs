using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadGrafo {
    class Aresta {
        private int index;
        private int value;
        //private Vertice vertice1;
        //private Vertice vertice2; 

        public Aresta(int index, int value) {
            this.index = index;
            this.value = value;
            //this.vertice1 = v1;
            //this.vertice2 = v2;
        }

        public int Index {
            get {
                return index;
            }
            set {
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

        /*
        public Vertice Vertice1
        {
            get
            {
                return vertice1;
            }
        }

        public Vertice Vertice2
        {
            get
            {
                return vertice2;
            }
        }
        */

    }
}
