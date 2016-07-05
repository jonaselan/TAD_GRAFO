using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadGrafo {
    class Program {
        
        static List<Vertice> vertices = new List<Vertice>();
        static List<Aresta> arestas = new List<Aresta>();
        static Aresta[,] m = new Aresta[4, 4];

        static void Main(string[] args) {
            Vertice v0 = new Vertice(0, 00);
            Vertice v1 = new Vertice(1, 11);
            Vertice v2 = new Vertice(2, 22);
            Vertice v3 = new Vertice(3, 33);

            
            vertices.Add(v0);
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            Aresta a0 = new Aresta(0, 99);
            Aresta a1 = new Aresta(1, 88);
            Aresta a2 = new Aresta(2, 77);
            Aresta a3 = new Aresta(3, 66);

            
            arestas.Add(a0);
            arestas.Add(a1);
            arestas.Add(a2);
            arestas.Add(a3);    

            // preenchendo tabela
            m[0, 1] = a0;
            m[0, 2] = a3;
            m[1, 0] = a0;
            m[2, 0] = a3;
            m[3, 1] = a1;
            m[3, 2] = a2;
            m[1, 3] = a1;
            m[2, 3] = a2;
            
            /*
            public List<Vertice> finalVertices(Aresta a){
                List<Vertice> l = new List<Vertice>();
                l.Add(a.Vertice1);
                l.Add(a.Vertice2);
                return l;
            }
            */
            // finalVertices
            //l.finalVertices(a1).ForEach(i => Console.WriteLine(i.Valor));
            finalVertices(a2).ForEach(i => Console.WriteLine(i.Index));

            /*
            public Vertice oposto(Vertice v, Aresta a) {
                if (a.Vertice1.Equals(v)) {
                    return a.Vertice1;
                }
                else if (a.Vertice2.Equals(v)) {
                    return a.Vertice2;
                }

                return null;
            }
            */
            //oposto
            //l.oposto(v1, a1);

            Console.ReadKey();

        }
        /*
        static public List<Vertice> finalVertices1(Aresta a) {
            List<Vertice> vs = new List<Vertice>();

            for (int i = 0; i < vertices.Count; i++) {
                for (int j = 0; j < vertices.Count; j++) {
                    if ((m[i, j] != null) && (m[i, j] == a)) { // encontrou a aresta
                        vs.Add(getVerticebyIndex(vertices, i));
                        vs.Add(getVerticebyIndex(vertices, j));
                        return vs;
                    }
                }
            }

            return null;
        }
        */
        static public List<Vertice> finalVertices(Aresta a) {
            List<Vertice> vs = new List<Vertice>();

            Tuple<Vertice, Vertice> tuple = getVerticesAresta(a);

            if (tuple != null) { // a aresta existe
                vs.Add(tuple.Item1);
                vs.Add(tuple.Item2);
                return vs;
            }
             
            return null; 
        }
        
        static public Vertice oposto(Vertice v, Aresta a) {
            Tuple<Vertice, Vertice> tuple = getVerticesAresta(a);

            Vertice linha = tuple.Item1;
            Vertice coluna = tuple.Item2;

            if (v.Equals(linha)) { // se for a linha
                return coluna;
            }
            else if (v.Equals(coluna)) { // se for a coluna
                return linha; 
            }
             
            return null; // o vertice não faz parte da aresta
        }

        static public bool eAdjacente(Vertice vert1, Vertice vert2) {
            if (m[vert1.Index, vert2.Index] != null) {
                return true;
            }
            return false;
        } 

        /* auxiliares */        
        static public Tuple<Vertice, Vertice> getVerticesAresta(Aresta a) {
            for (int i = 0; i < vertices.Count; i++) {
                for (int j = 0; j < vertices.Count; j++) {
                    if ((m[i, j] != null) && (m[i, j] == a)) { // encontrou a aresta
                        return Tuple.Create(getVerticebyIndex(i), getVerticebyIndex(j));
                    }
                }
            }

            return null;
        }
        static public Vertice getVerticebyIndex(int index)
        {
            // verificar se o Vertice da vez tem index igual ao que foi passado por paramentro
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Index.Equals(index))
                {
                    return vertices[i];
                }
            }

            return null;
        }
    
    }
}
