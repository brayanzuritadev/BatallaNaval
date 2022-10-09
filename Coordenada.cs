using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaNaval
{
    public class Coordenada
    {
        
        public int filaI { get; set; }
        public int columnaI { get; set; }
        public Coordenada(int posFi, int posCi)
        {
            this.filaI = posFi;
            this.columnaI = posCi;
        }
    }
}
