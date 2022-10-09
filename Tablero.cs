using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatallaNaval
{
    public class Tablero
    {

        public char[,] tablero = new char[10, 10];
        public char[,] tableroDisparos = new char[10, 10];
        public Nave nave;
        public void llenarTableroVacio()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    tablero[i, j] = 'O';
                }
            }
        }

        //agregamos un disparo al tablero de disparos
        public bool agregarDisparo(int fila,int columna)
        {
            /*
             * revisamos que en el tablero en la posicion enviada no haya una O
             * de ser asi grabamos lo que se encuentre en el tablero al tableroDisparos
             * caso contrario agregamos una T al tableroDisparos en esa posicion
             */
            if(tablero[fila, columna] != 'O')
            {
                tableroDisparos[fila, columna] = tablero[fila, columna];
                return true;
            }
            tableroDisparos[fila, columna] = 'T';
            return false;
        }

       /* public void mostrarTableroDisparos()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Console.Write(" " + tableroDisparos[r, c]);

                }
                Console.WriteLine();
            }

            Console.WriteLine("Renglones y columnas: Tiros ");
        }*/
        public void mostrarTablero()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Console.Write(" " + tablero[r, c]);

                }
                Console.WriteLine();
            }

            Console.WriteLine("Renglones y columnas: ");
        }

        //agregamos una nave al tablero
        public void agregarBarco(Nave nave, string tipoLlenado)
        {
            if (tipoLlenado == "horizontal")
            {
                for (int i = nave.coordenada.columnaI; i < nave.dimension + nave.coordenada.columnaI; i++)
                {
                    tablero[nave.coordenada.filaI, i] = nave.nombre.FirstOrDefault();
                }
            }
            if (tipoLlenado == "vertical")
            {
                for (int i = nave.coordenada.filaI; i < nave.dimension + nave.coordenada.filaI; i++)
                {
                    tablero[i, nave.coordenada.columnaI] = nave.nombre.FirstOrDefault();
                }
            }
        }


        //Revisamos que no exista ninguna nave en en los espacios que se desea agregar la nueva nave
        public bool buscarEspacio(string tipoLlenado,Nave nave)
        {
            if (tipoLlenado == "horizontal")
            {
                if (nave.coordenada.columnaI+nave.dimension<=10) {
                    for (int i = nave.coordenada.columnaI; i < nave.dimension + nave.coordenada.columnaI; i++)
                    {
                        if (tablero[nave.coordenada.filaI, i] != 'O')
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            if (tipoLlenado == "vertical")
            {
                if (nave.coordenada.filaI + nave.dimension <= 10)
                {
                    for (int i = nave.coordenada.filaI; i < nave.dimension + nave.coordenada.filaI; i++)
                    {
                        if (tablero[i, nave.coordenada.columnaI] != 'O')
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

    }
}
