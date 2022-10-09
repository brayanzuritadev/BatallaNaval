using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaNaval
{
    internal class Jugador
    {
        public string nombre { get; set; }
        public Tablero tableroJugador { get; set; }

        public List<Nave> naves = new List<Nave>();
        
        //se encarga de agregar una nave a la lista
        public int agregarNave(Nave nave)
        {
            //verificamos que no exista una nave con ese nombre dentro de la lista
            if (!buscarNave(nave))
            {
                naves.Add(nave);
                return 1;
            }
            return 0;
        }
        
        //buscamor la nave dentro de la lista
        private bool buscarNave(Nave nave)
        {
            if (naves.Count > 0)
            {
                for (int i = 0; i < naves.Count; i++)
                {
                    if (naves.ElementAt(i).nombre == nave.nombre)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
