using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Arbol
{
    class CNodo
    {
        private Point punto;
        private CNodo C1 = null;
        private CNodo C2 = null;
        private string nombre;

        private bool anutable;
        private int etiqueta;
        //primera, ultima y pos siguiente 

        public CNodo(int x,int y,CNodo izq,CNodo der,string nom)
        {
            punto = new Point(x, y);
            C1 = izq;
            C2 = der;
            nombre = nom;
        }
        public Point getPc()
        {
            return punto;
        }
        public CNodo NodoC1()
        {
            return C1;
        }
        public CNodo NodoC2()
        {
            return C2;
        }
        public string getNombre()
        {
            return nombre;
        }
    }
}
