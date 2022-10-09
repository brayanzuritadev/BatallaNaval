using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatallaNaval
{
    internal class Grafico
    {
        public int x { get; set; } = 0;
        public int y { get; set; } = 0;

        //Se encarga de pintar un cuadrado dentro del panel
        public void cuadrado(Panel cuadrado,SolidBrush brocha)
        {
            Graphics figura;
            Console.WriteLine(x + " " + y);
            figura = cuadrado.CreateGraphics();
            figura.FillRectangle(brocha, x, y, 50, 50);
        }
    }
}
