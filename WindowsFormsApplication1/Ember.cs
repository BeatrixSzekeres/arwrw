using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Ember
    {
        public bool MarBejarva = false;

        public Ember parja;
        public List<Ember> gyermekek = new List<Ember>();
        public string Nev;
        public Color Szin = Color.Black;

        public Ember(string Nev,Color Szin)
        {
            this.Nev = Nev;
            this.Szin = Szin;
        }
        public Ember(string Nev)
        {
            this.Nev = Nev;
        }
    }
}
