using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatallaNaval
{
    public partial class Form1 : Form
    {
        Grafico grafico = new Grafico();

        Tablero t1 = new Tablero();
        Tablero t2 = new Tablero();

        Jugador jugador1 = new Jugador();
        Jugador jugador2 = new Jugador();

        Dictionary<string, int> diccionario = new Dictionary<string, int>()
        {
            {"A",0},
            {"B",1},
            {"C",2},
            {"D",3},
            {"E",4},
            {"F",5},
            {"G",6},
            {"H",7},
            {"I",8},
            {"J",9}
        };

        int turno = 1;
        public Form1()
        {
            InitializeComponent();


            jugador1.nombre = "jugador1";
            jugador2.nombre = "jugador2";

            t1.llenarTableroVacio();
            jugador1.tableroJugador = t1;

            t2.llenarTableroVacio();
            jugador2.tableroJugador = t2;
            turnoJugador();
            llenarCbo();
        }

        private void llenarCbo()
        {
            comboBox1.Items.Add("Porta Aviones");
            comboBox1.Items.Add("Acorazado");
            comboBox1.Items.Add("Crucero");
            comboBox1.Items.Add("Submarino");
            comboBox1.Items.Add("Destructor");
            comboBox1.SelectedIndex = 0;

            for (int i = comboBox2.Items.Count - 1; i >= 0; i--)
            {
                comboBox2.Items.RemoveAt(i);
            }
            comboBox2.Items.Add("horizontal");
            comboBox2.Items.Add("vertical");
            comboBox2.SelectedIndex = 0;
        }
        public void turnoJugador()
        {
            if (turno == 1)
            {
                label24.Text = jugador1.nombre;
                panel3.Visible = false;
                panel1.Visible = true;
                panel1.Invalidate();
            } else {
                label24.Text = jugador2.nombre;
                panel1.Visible = false;
                panel3.Visible = true;
                panel1.Invalidate();
            }
        }

        private void ganador(Jugador jugador){
            int contador = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (jugador.tableroJugador.tableroDisparos[i,j]==jugador.tableroJugador.tablero[i,j])
                    {
                        contador++;
                    }
                }
            }
            if (contador==17)
            {
                label24.Font = new Font(label1.Font.FontFamily, 30);
                label24.Text = "felicidades " + jugador.nombre + " ganaste";
                panel1.Visible = false;
                panel3.Visible = false;
            }
        }

        private void actualizarGrafico()
        {
           for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (turno == 1)
                    {
                        dibujarBarcosJugadorUno(i, j);
                    }
                    if(turno ==2)
                    {
                        dibujarBarcosJugadorDos(i, j);
                    }
                }
            }
        }

        private void dibujarBarcosJugadorUno(int i,int j)
        {
            SolidBrush brocha= new SolidBrush(Color.Blue);

            for (int k = 0; k < jugador1.naves.Count; k++)
            {
                if (jugador1.tableroJugador.tablero[i, j] == jugador1.naves[k].nombre.FirstOrDefault())
                {
                    grafico.x = j * 50;
                    grafico.y = i * 50;
                    grafico.cuadrado(panel1,brocha);
                }
            }
        }

        private void dibujarBarcosJugadorDos(int i, int j)
        {
            SolidBrush brocha = new SolidBrush(Color.Yellow);
            for (int k = 0; k < jugador2.naves.Count; k++)
            {
                if (jugador2.tableroJugador.tablero[i, j] == jugador2.naves[k].nombre.FirstOrDefault())
                {
                    grafico.x = j * 50;
                    grafico.y = i * 50;
                    grafico.cuadrado(panel3,brocha);
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //pintamos el panel1
            panel1.Size = new Size(500,500);
            Graphics g = e.Graphics;
            //lineas Verticales
            g.DrawLine(Pens.Black,50,0,50, 500);
            g.DrawLine(Pens.Black, 100, 0, 100, 500);
            g.DrawLine(Pens.Black, 150, 0, 150, 500);
            g.DrawLine(Pens.Black, 200, 0, 200, 500);
            g.DrawLine(Pens.Black, 250, 0, 250, 500);
            g.DrawLine(Pens.Black, 300, 0, 300, 500);
            g.DrawLine(Pens.Black, 350, 0, 350, 500);
            g.DrawLine(Pens.Black, 400, 0, 400, 500);
            g.DrawLine(Pens.Black, 450, 0, 450, 500);

            
            //lineas horizontales
            g.DrawLine(Pens.Black, 0, 50, 500, 50);
            g.DrawLine(Pens.Black, 0, 100, 500, 100);
            g.DrawLine(Pens.Black, 0, 150, 500, 150);
            g.DrawLine(Pens.Black, 0, 200, 500, 200);
            g.DrawLine(Pens.Black, 0, 250, 500, 250);
            g.DrawLine(Pens.Black, 0, 300, 500, 300);
            g.DrawLine(Pens.Black, 0, 350, 500, 350);
            g.DrawLine(Pens.Black, 0, 400, 500, 400);
            g.DrawLine(Pens.Black, 0, 450, 500, 450);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Coordenada c = new Coordenada(int.Parse(txtFilaI.Text) - 1, diccionario[txtColumnaI.Text.ToUpper()]);
            Nave nave = new Nave()
            {
                nombre = comboBox1.Text,
                coordenada = c,
                dimension = llenarDimension(),
            };

            if (turno==1)
            {
                jugadorUno(nave);
            }
            else {
                jugadorDos(nave);
            }

        }

        private void jugadorUno(Nave nave)
        {
            if (!jugador1.tableroJugador.buscarEspacio(comboBox2.Text, nave) && jugador1.agregarNave(nave) == 1)
            {
                jugador1.tableroJugador.agregarBarco(nave, comboBox2.Text);
                label24.Text = "Se agrego correctamente el Barco";
                actualizarCbo();
                actualizarGrafico();
                if (jugador1.naves.Count == 5)
                {
                    turno= 2;
                    llenarCbo();
                    turnoJugador();
                }
            }
            else
            {
                label24.Text = "Hay una nave en esa posicion o \n" +
                    "excede el espacio permitido";
            }
        }

        private void jugadorDos(Nave nave)
        {
            if (!jugador2.tableroJugador.buscarEspacio(comboBox2.Text, nave) && jugador2.agregarNave(nave) == 1)
            {
                jugador2.tableroJugador.agregarBarco(nave, comboBox2.Text);
                label24.Text = "Se agrego correctamente el Barco";
                actualizarCbo();
                actualizarGrafico();
                if (jugador2.naves.Count==5)
                {
                    turno = 1;
                    limpiarTablero();
                    turnoJugador();
                    panel2.Visible = false;
                }
            }
            else
            {
                label24.Text = "Hay una nave en esa posicion o \n" +
                    "excede el espacio permitido";
            }
        }

        private void limpiarTablero()
        {
            panel1.Invalidate();
            panel2.Invalidate();
        }

        private void actualizarCbo(){
            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            if (comboBox1.Items.Count!=0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        private int llenarDimension()
        {
            int dimension;
            switch (comboBox1.Text)
            {
                case "Porta Aviones":
                    dimension = 5;
                    break;
                case "Acorazado":
                    dimension = 4;
                    break;
                case "Crucero":
                    dimension = 3;
                    break;
                case "Submarino":
                    dimension = 3;
                    break;
                case "Destructor":
                    dimension = 2;
                    break;
                default:
                    dimension = -1;
                    break;
            }

            return dimension ;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel1.Size = new Size(500, 500);
            Graphics g = e.Graphics;
            SolidBrush myBrush = new SolidBrush(Color.Red);
            //lineas Verticales
            g.DrawLine(Pens.Black, 50, 0, 50, 500);
            g.DrawLine(Pens.Black, 100, 0, 100, 500);
            g.DrawLine(Pens.Black, 150, 0, 150, 500);
            g.DrawLine(Pens.Black, 200, 0, 200, 500);
            g.DrawLine(Pens.Black, 250, 0, 250, 500);
            g.DrawLine(Pens.Black, 300, 0, 300, 500);
            g.DrawLine(Pens.Black, 350, 0, 350, 500);
            g.DrawLine(Pens.Black, 400, 0, 400, 500);
            g.DrawLine(Pens.Black, 450, 0, 450, 500);

            //lineas horizontales
            g.DrawLine(Pens.Black, 0, 50, 500, 50);
            g.DrawLine(Pens.Black, 0, 100, 500, 100);
            g.DrawLine(Pens.Black, 0, 150, 500, 150);
            g.DrawLine(Pens.Black, 0, 200, 500, 200);
            g.DrawLine(Pens.Black, 0, 250, 500, 250);
            g.DrawLine(Pens.Black, 0, 300, 500, 300);
            g.DrawLine(Pens.Black, 0, 350, 500, 350);
            g.DrawLine(Pens.Black, 0, 400, 500, 400);
            g.DrawLine(Pens.Black, 0, 450, 500, 450);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            jugador1.tableroJugador = t2;
            jugador2.tableroJugador = t1;
            Console.WriteLine(turno);
            if (turno==1)
            {
                disparar(jugador1,panel1);
            }
            else
            {
                disparar(jugador2,panel3);
            }
        }

        private void disparar(Jugador jugador, Panel panel)
        {
            SolidBrush brocha = new SolidBrush(Color.Red);
            grafico.x = diccionario[textBox2.Text.ToUpper()] * 50;
            grafico.y = (int.Parse(textBox1.Text) - 1) * 50;
            if (jugador.tableroJugador.agregarDisparo(int.Parse(textBox1.Text) - 1, diccionario[textBox2.Text.ToUpper()])){
                grafico.cuadrado(panel,brocha);
                ganador(jugador);
            }
            else
            {
                grafico.cuadrado(panel, new SolidBrush(Color.Black));
                mostrarPanel(jugador,panel);
            }
            
            //jugador.tableroJugador.mostrarTablero();
        }
        private void mostrarPanel(Jugador jugador,Panel panel) {
            if (turno==1)
            {
                turno = 2;
                panel3.Visible = true;
                label24.Text = jugador2.nombre;
            }
            else
            {
                turno=1;
                panel1.Visible=true;
                label24.Text = jugador1.nombre;
            }

        }

        private void txtColumnaI_KeyUp(object sender, KeyEventArgs e)
        {
            bool resultado = Regex.IsMatch(txtColumnaI.Text, @"^[A-Ja-j]{1}$");
            if (!resultado)
            {
                txtColumnaI.Text = "";
            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            bool resultado = Regex.IsMatch(textBox2.Text, @"^[A-Ja-j]{1}$");
            if (!resultado)
            {
                textBox2.Text = "";
            }
        }

        private void txtFilaI_KeyUp(object sender, KeyEventArgs e)
        {
            bool resultado = Regex.IsMatch(txtFilaI.Text, @"^([0-9]|10)$");
            if (!resultado)
            {
                txtFilaI.Text = "";
            }
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            bool resultado = Regex.IsMatch(textBox1.Text, @"^([0-9]|10)$");
            if (!resultado)
            {
                textBox1.Text = "";
            }
        }
    }
}
