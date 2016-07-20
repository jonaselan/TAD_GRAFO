using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tadGrafo {
    class Program {
        
        static List<Vertice> vertices;
        static List<Aresta> arestas;
        static Aresta[,] m;
        public static Exception valorNaoEncontrado { get; set; }

        static void Main(string[] args) {
            Vertice v0 = new Vertice(0, 00);
            Vertice v1 = new Vertice(1, 11);
            Vertice v2 = new Vertice(2, 22);
            Vertice v3 = new Vertice(3, 33);
            vertices = new List<Vertice>();
            
            vertices.Add(v0);
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            m = new Aresta[vertices.Count, vertices.Count];

            Aresta a0 = new Aresta(0, 99, false);
            Aresta a1 = new Aresta(1, 88, false);
            Aresta a2 = new Aresta(2, 77, false);
            Aresta a3 = new Aresta(3, 66, false);
            arestas = new List<Aresta>();

            arestas.Add(a0);
            arestas.Add(a1);
            arestas.Add(a2);
            arestas.Add(a3);    

            // preenchendo matriz (grafo) 
            m[0, 1] = a0;
            m[0, 2] = a3;
            m[1, 0] = a0;
            m[2, 0] = a3;
            m[3, 1] = a1;
            m[3, 2] = a2;
            m[1, 3] = a1;
            m[2, 3] = a2;

            // TODO: verificar quando essas funções retornam zero 

            try { 
                //finalVertices(a2).ForEach(i => Console.WriteLine(i.Index));
                //Console.WriteLine(oposto(v0, a0).Value); 

                // Console.WriteLine(eAdjacente(v0, v3));
                //substituir(v0, 3);
                //substituir(a0, 3);

                /* testar inserir
                Vertice ver = new Vertice(4, 65);
                inserirVertice(ver);
                Aresta are = new Aresta(4, 12);
                inserirAresta(vertices[vertices.Count - 1], vertices[3], are);
                */

                /* testar remover */
                //removerVertice(vertices[1]);
                //removerAresta(a0);

                //arestasIncidentes(vertices[1]);
                //printMatrizIndex();
                 grafoEuler();
            }
            catch (ExceptionGrafo e) {
                
            }
            finally { 
                Console.ReadKey();
            }

        }


        static public List<Vertice> finalVertices(Aresta a) {
            List<Vertice> vs = new List<Vertice>();

            Tuple<Vertice, Vertice> tuple = getVerticesAresta(a);

            if (tuple != null) { // a aresta existe
                vs.Add(tuple.Item1);
                vs.Add(tuple.Item2);
                return vs;
            }
            
            throw new ExceptionGrafo("Erro!");
            //return null;
            
        } // DONE
        
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

            throw new ExceptionGrafo("Erro!");
            // return null; // o vertice não faz parte da aresta

        } //DONE

        static public bool eAdjacente(Vertice vert1, Vertice vert2) {
            // se for direcionada, a ordem importa
            if ((m[vert1.Index, vert2.Index].Direcionada) && (m[vert1.Index, vert2.Index] != null)) {
                return true;
            }
            // não direcionada
            else if (!(m[vert1.Index, vert2.Index].Direcionada) && (m[vert1.Index, vert2.Index] != null)) {
                return true;
	        }
            else if (!(m[vert2.Index, vert1.Index].Direcionada) && (m[vert2.Index, vert1.Index] != null)) {
                return true;
            }
            // não é adjacente
            return false;
        } //DONE

        static public void substituir(Vertice v, int x) {
            for (int i = 0; i < vertices.Count; i++) {
                if (vertices[i].Equals(v)) {
                    vertices[i].Value = x;
                    return;
                }
            }
        } // DONE

        static public void substituir(Aresta a, int x) {
            // modificar no list
            for (int i = 0; i < arestas.Count; i++) {
                if (arestas[i].Equals(a)) {
                    arestas[i].Value = x;
                    return;
                }
            }
            // modificar na matriz
            for (int i = 0; i < vertices.Count; i++) {
                for (int j = 0; j < vertices.Count; j++) {
                    if (m[i,j].Equals(a)) {
                        m[i,j].Value = x;
                        return;
                    }
                }
            }
        } // DONE

        static public void inserirVertice(Vertice v) {
            v.Index = vertices.Count; // fixar o tamanho do index
            vertices.Add(v);
            Aresta[,] newM = new Aresta[vertices.Count, vertices.Count];
            
            for (int i = 0; i < vertices.Count - 1; i++)
                for (int j = 0; j < vertices.Count - 1; j++)
                       newM[i, j] = m[i, j];

            m = newM;
        } // DONE

        static public void inserirAresta(Vertice v1, Vertice v2, Aresta a) {
            a.Index = arestas.Count; // fixar o tamanho do index
            a.Direcionada = false; // não direcionada
            arestas.Add(a);
            if (m[v1.Index, v2.Index] == null){
                // supondo que não há arestas paralelas
                m[v1.Index, v2.Index] = a;
                m[v2.Index, v1.Index] = a;
            }
            else
                Console.WriteLine("já existe um valor nessa posição!");
        } // DONE

        static public int removerVertice(Vertice v) {
            // elemento do vertice que está a ser removido
            int elemntV = v.Value;
            // antes de reestruturar a matriz, guardar as arestas que serão removidas
            List<Aresta> removerArestas = new List<Aresta>();
            removerArestas = arestasIncidentes(v);

            // começar a remoção das linhas/colunas
            Aresta[,] newM = new Aresta[vertices.Count - 1, vertices.Count - 1];
            int l = -1, c = -1;

            for (int i = 0; i < vertices.Count; i++) {
                if ((i != v.Index)) {
                    l++;
                    for (int j = 0; j < vertices.Count; j++) {
                        if (j != v.Index) {
                            c++;
                            newM[l, c] = m[i, j];
                        }
                    }
                    c = -1;
                }
            }
            m = newM;


            // remover arestas incidentes
            for (int o = 0; o < removerArestas.Count; o++) {
                arestas.Remove(removerArestas[o]);
            }
            // organizar por ondem crescente os index
            for (int i = 0; i < arestas.Count; i++){
                arestas[i].Index = i;
            }

            // remover o vertice da list e reorganiza-la
            int indexVerticeRemovido = v.Index;
            vertices.Remove(v);

            for (int i = indexVerticeRemovido; i < vertices.Count; i++) {
                vertices[i].Index = i;
            }

            return elemntV;
        } // DONE

        static public void removerAresta(Aresta a) {
            // remover aresta da matriz
            for (int i = 0; i < vertices.Count; i++) {
                for (int j = 0; j < vertices.Count; j++) {
                    if ((m[i, j] != null) && (m[i, j].Equals(a)))
                        m[i, j] = null;
                    
                }
            }

            // remover a aresta da list e reorganiza-la
            /*int indexArestaRemovida = a.Index;
            arestas.Remove(a);
            
            for (int i = indexArestaRemovida; i < arestas.Count; i++) {
                arestas[i].Index = i;
            }*/
            for (int i = 0; i < arestas.Count; i++) {
                if (arestas[i] == a) {
                    arestas[i] = null;
                }
                break;
            }
        } // DONE

        static public List<Aresta> arestasIncidentes(Vertice v) {
            List<Aresta> a = new List<Aresta>();

            for (int i = 0; i < vertices.Count; i++) {
                if (m[i, v.Index] != null) {
                    a.Add(m[i, v.Index]);
                }
            }
            return a;
        } // DONE
                
        static public List<Vertice> Vertices() {
            return vertices;
        } // DONE

        static public List<Aresta> Arestas() {
            return arestas;
        } // DONE

        static public bool eDirecionada(Aresta a) {
            return a.Direcionada;
        } // DONE
        
        static public void inserirArestaDirecionada(Vertice v1, Vertice v2, Aresta a) {
            a.Index = arestas.Count; // fixar o tamanho do index
            a.Direcionada = true; // direcionada
            arestas.Add(a);
            if (m[v1.Index, v2.Index] == null) {
                // supondo que não há arestas paralelas
                // amarra linha e coluna
                m[v1.Index, v2.Index] = a;
            }
            else
                Console.WriteLine("já existe um valor nessa posição!");
        } // DONE

        static public void grafoEuler() {
            int soma = 0;
            int ocorre = 0;
            // passar por cada vertice
            for (int g = 0; g < arestas.Count; g++) {
                // if soma > 2 return não há caminho
                soma = 0;
                for (int i = 0; i < vertices.Count; i++) {
                    for (int j = 0; j < vertices.Count; j++) {
                        if (arestas[g] == m[i, j]) {
                            soma++;
                        }
                    }
                }
                if (soma > 2) {
                    ocorre++;
                }
                if (ocorre > 2) {
                    Console.WriteLine("não é euleriano");
                    return;
                }
            }
            Console.WriteLine("é euleriano");
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
        static public Vertice getVerticebyIndex(int index) {
            // verificar se o vertice da vez tem index igual ao que foi passado por paramentro
            for (int i = 0; i < vertices.Count; i++) {
                if (vertices[i].Index.Equals(index)) {
                    return vertices[i];
                }
            }
            throw new ExceptionGrafo("Erro!");
            //return null;
        }
        static public void printMatrizIndex() {
            for (int i = 0; i < vertices.Count; i++) {
                for (int j = 0; j < vertices.Count; j++) {
                    if (m[i, j] != null) {
                        Console.Write(m[i, j].Index + " ");     
                    }
                    else 
                        Console.Write("- ");     
                }
                Console.WriteLine();
            }
        }
        static public void printMatrizValue()
        {
            for (int i = 0; i < vertices.Count; i++) {
                for (int j = 0; j < vertices.Count; j++) {
                    if (m[i, j] != null) 
                        Console.Write(m[i, j].Value + " ");
                    else
                        Console.Write("- ");
                }
                Console.WriteLine();
            }
        }

    }
}