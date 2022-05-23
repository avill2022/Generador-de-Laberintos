using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace Arbol
{
    public partial class Form1 : Form
    {

        Pen pluma = new Pen(Color.Black);

        Font font;
        int tam=25;
        int tamMatrix = 20;
        Graphics g;
        private Graphics pagina;
        private Bitmap bmp;
        int[,] matrix = null;
        Random r = new Random();
        Queue<int> pila = new Queue<int>();
        FileStream Archivo = null;


        int posx = 0;
        int posy = 0;

        public Form1()
        {
            InitializeComponent();
            font = new Font("Arial", 10);
            g = CreateGraphics();
            AdjustableArrowCap acc = new AdjustableArrowCap(5, 5, true);
        
            bmp = new Bitmap(1600, 1024);
            pagina = Graphics.FromImage(bmp);
            matrix = new int[tamMatrix, tamMatrix];
           // Size size = new Size(tamMatrix * tam+tam, tamMatrix * tam+tam*2);
           // this.Size = size;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            dibujaMatrix(matrix, g);

        }
        public void dibujaMatrix(int[,] _matrix,Graphics g) {
            
            for (int x = 0; x < tamMatrix; x++)
            {
                for (int y = 0; y < tamMatrix; y++)
                {
                    if (_matrix[x, y] == 0)
                    {
                        g.DrawRectangle(pluma, x* tam + 3, y* tam+3, tam, tam);
                    }
                    else {
                        g.FillRectangle(Brushes.Black, x * tam + 3, y * tam + 3, tam, tam);
                    }
                }
            }
           // g.FillRectangle(Brushes.Black, posx * tam, posy * tam, tam, tam);
        }
        public int inicializaMatrix(int[,] _matrix) {
            for (int i = 0; i < tamMatrix; i++) {
                for (int j = 0; j < tamMatrix; j++)
                {
                    _matrix[i, j] = 1;
                }
            }
            return 0;
        }
        String s = "";
        public void busqueda(int x, int y, int[,] _matrix) {
            _matrix[x, y] = 0;
            //genera pila de resultados
            Queue<int> nueva = generaLista();
            s = "";
            //imprimo la lista
            foreach (int i in nueva) {
                s += "- " + i.ToString();
            }
            label1.Text = s;
            foreach (int i in nueva) {
                switch (i) {
                    case 0://<-
                        if (x - 1 != -1) {
                            if (y - 1 != -1)
                            {
                                if (y + 1 != tamMatrix)
                                {
                                    if (_matrix[x - 1, y - 1] == 1 && _matrix[x - 1, y + 1] == 1) {
                                        //generaLaberinto(x-1, y, matrix);
                                        if (_matrix[x - 1, y] != 0) { posx = x - 1;posy = y; busqueda(x-1, y, _matrix);}
                                            
                                    }
                                }
                                
                            }
                        }
                        break;
                    case 1://¡
                        if (y - 1 != -1)
                        {
                            if (x - 1 != -1)
                            {
                                if (x + 1 != tamMatrix)
                                {
                                    if (_matrix[x - 1, y - 1] == 1 && _matrix[x + 1, y - 1] == 1)
                                    {
                                        if (_matrix[x - 1, y - 1] != 0) { posx = x; posy = y-1; busqueda(x, y-1, _matrix);}
                                        
                                    }
                                }

                            }
                        }
                        break;
                    case 2://->
                        if (x + 1 != tamMatrix)
                        {
                            if (y - 1 != -1)
                            {
                                if (y + 1 != tamMatrix)
                                {
                                    if (_matrix[x + 1, y - 1] == 1 && _matrix[x + 1, y + 1] == 1)
                                    {
                                        if (_matrix[x + 1, y] != 0) { posx = x +1; posy = y; busqueda(x+1, y, _matrix);}
                                        
                                    }
                                }

                            }
                        }
                        break;
                    case 3://!
                        if (y+ 1 != tamMatrix)
                        {
                            if (x - 1 != -1)
                            {
                                if (x + 1 != tamMatrix)
                                {
                                    if (_matrix[x - 1, y + 1] == 1 && _matrix[x + 1, y + 1] == 1)
                                    {
                                        if (_matrix[x, y + 1] != 0) { posx = x; posy = y+1;busqueda(x, y+1, _matrix); }
                                        
                                    }
                                }

                            }
                        }
                        break;
                }
            }
            
        }
        public bool verificar(int x, int y, int[,] _matrix) {
            return false;
        }
        int entero = 0;
        public Queue<int> generaLista() {
            Queue<int> nu = new Queue<int>();

            while (nu.Count!=4) {
                entero = r.Next(4);
                if (agregaPila(nu, entero)){
                    nu.Enqueue(entero);
                }
            }
            return nu;
        }
        public bool agregaPila(Queue<int> n,int i) {
            foreach (int es in n) {
                if (es == i)
                    return false;
            }
            return true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            posx = r.Next(tamMatrix-1);
            posy = r.Next(tamMatrix-1);
            inicializaMatrix(matrix);
            busqueda(posx, posy, matrix);
            this.Form1_Paint(this, null);

            Archivo = new FileStream("Nivel1.txt", System.IO.FileMode.Create);
            System.IO.StreamWriter Escritura;
            Escritura = new StreamWriter(Archivo, Encoding.ASCII);


            for (int x = 0; x < tamMatrix; x++)
            {
                for (int y = 0; y < tamMatrix; y++)
                {
                    if (matrix[x, y] == 0)
                    {
                        Escritura.Write("00");
                        Escritura.Write(" ");
                    }
                    else {
                        Escritura.Write("08");
                        Escritura.Write(" ");
                    }
                }
                Escritura.Write("\n");
            }
            Escritura.Close();
            Archivo.Close();
        }
    }
}
