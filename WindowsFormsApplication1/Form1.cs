using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String gyujto(Ember peldany,int szint)
        {
            if (peldany.MarBejarva)
                return "";

            peldany.MarBejarva = true;
            

            StringBuilder sb = new StringBuilder();

            if (peldany.parja != null)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(peldany.parja.Nev);
                sb.Append(";");

                sb.Append(gyujto(peldany.parja,szint));
            }

            foreach( Ember e in peldany.gyermekek)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(e.Nev);
                sb.Append("[penwidth="+szint+",weight="+szint*2+",color=\""+ColorTranslator.ToHtml(peldany.Szin)+"\"];");      
                sb.Append(gyujto(e,szint+1));
               
            }

            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ember anya = new Ember("Anya",Color.Green);
            Ember apa = new Ember("Apa");
            Ember gy1 = new Ember("Gy1");
            Ember gy2 = new Ember("Gy2");
            Ember u1 = new Ember("U");
            Ember uu1 = new Ember("UU1");
            Ember uu1parja = new Ember("UU1parja",Color.Red);

            apa.parja = anya;
            anya.parja = apa;
            uu1parja.parja = uu1;
            uu1.parja = uu1parja;

            apa.gyermekek.Add(gy1);
            apa.gyermekek.Add(gy2);
            anya.gyermekek.Add(gy1);
            anya.gyermekek.Add(gy2);

            gy1.gyermekek.Add(u1);

            u1.gyermekek.Add(uu1);
            u1.gyermekek.Add(new Ember("UU2"));
            u1.gyermekek.Add(new Ember("UU3"));

            uu1parja.gyermekek.Add(new Ember("UUU1"));
            uu1parja.gyermekek.Add(new Ember("UUU2"));

            String s = gyujto(apa,1);

            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);


            var wrapper = new GraphGeneration(getStartProcessQuery,
                                              getProcessStartInfoQuery,
                                              registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph("digraph{"+s+"}", Enums.GraphReturnType.Png);

            using (MemoryStream ms = new MemoryStream(output))
            {
               Image i = Image.FromStream(ms);
                pictureBox1.Image = i;
                    }
        }
    }
}
