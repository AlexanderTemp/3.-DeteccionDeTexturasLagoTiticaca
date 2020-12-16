using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//ALEXANDER HUMBERTO NINA PACAJES             5950236 LP
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        ///Almaceno los upntos de interés para detección de texturas de cada tipo usando matrices
        int[,] vectoragua ={{10,30,50},{20,50,60},{ 10,40,70},{ 10,45,55},{ 10,39,55},{9,55,70 },{20,70,80}, {9,34,50}, {9,50,50},{ 35,52,90},{ 35,60,75},{ 30,50,71} ,{ 16,41,98} };
        int[,] vectortierra = { { 115,110,95},{ 140,130,115},{ 165,150,135},{ 168,157,138},{ 192,174,147},{165,170,165 },{148,128,105},{ 133,130,125},{ 110,110,110},{ 185,175,165} ,{ 197,175,160},{210,195,175 },{ 155,135,112},{ 85,85,75},{ 95,90,81} ,{ 179,147,129},{ 75,96,66},{ 160,145,120} ,{ 182,175,65} };
        int[,] vectorbosque = { { 35, 60, 50 }, { 15, 30, 30 }, { 34, 34, 40 }, { 40, 70, 58 }, { 30, 45, 40 } ,{ 9,35,35},{ 85,75,58},{ 50,75,65} ,{ 35,60,35},{ 30,60,35} ,{ 36,73,41},{ 58,94,58} };

        int cR, cG, cB;
        //almacena la media de puntos del canal
        int cmR, cmG, cmB;
        public Form1()
        {
            InitializeComponent();
        }
        private void label5_Click(object sender, EventArgs e)
        {
        }
        ///FUNCION PRINCIPAL analiza cada 5 pares de pixeles para detectar que tipo de textura corresponde
        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            Color d = new Color();
            int ciR, ciG, ciB;
            for (int i = 0; i < bmp.Width - 5; i = i + 5)
                for (int j = 0; j < bmp.Height - 5; j = j + 5)
                {
                    ciR = 0;
                    ciG = 0;
                    ciB = 0;
                    for (int x = i; x < i + 5; x++)
                        for (int y = j; y < j + 5; y++)
                        {
                            c = bmp.GetPixel(x, y);
                            ciR = ciR + c.R;
                            ciG = ciG + c.G;
                            ciB = ciB + c.B;
                        }
                    ciR = ciR / 25;
                    ciG = ciG / 25;
                    ciB = ciB / 25;
                    ////dato para el switch 
                    int valor= 0;
                    for (int z = 0; z < vectoragua.GetLength(0); z++)
                        if (((vectoragua[z, 0] - 10 < ciR) && (ciR < vectoragua[z, 0] + 10)) && ((vectoragua[z, 1] - 10 < ciG) && (ciG < vectoragua[z, 1] + 10)) && ((vectoragua[z, 2] - 10 < ciB) && (ciB < vectoragua[z, 2] + 10)))
                        {
                            valor=1;
                        }
                    for (int z = 0; z < vectorbosque.GetLength(0); z++)
                        if (((vectorbosque[z, 0] - 10 < ciR) && (ciR < vectorbosque[z, 0] + 10)) && ((vectorbosque[z, 1] - 10 < ciG) && (ciG < vectorbosque[z, 1] + 10)) && ((vectorbosque[z, 2] - 10 < ciB) && (ciB < vectorbosque[z, 2] + 10)))
                        {
                            valor= 2;
                        }
                    for (int z = 0; z < vectortierra.GetLength(0); z++)
                        if (((vectortierra[z, 0] - 10 < ciR) && (ciR < vectortierra[z, 0] + 10)) && ((vectortierra[z, 1] - 10 < ciG) && (ciG < vectortierra[z, 1] + 10)) && ((vectortierra[z, 2] - 10 < ciB) && (ciB < vectortierra[z, 2] + 10)))
                        {
                            valor= 3;
                        }
                    ///agua=azul neutro   bosque=rojo  tierra= amarillo
                    switch (valor)
                    {
                        case 0:
                            for (int x = i; x < i + 5; x++)
                                for (int y = j; y < j + 5; y++)
                                {
                                    d = bmp.GetPixel(x, y);
                                    bmp2.SetPixel(x, y, Color.FromArgb(d.R, d.G, d.B));
                                }
                            break;

                        case 1:
                            for (int x = i; x < i + 5; x++)
                                for (int y = j; y < j + 5; y++)
                                {
                                    bmp2.SetPixel(x, y, Color.Blue);
                                }
                            break;
                        case 2:
                            for (int x = i; x < i + 5; x++)
                                for (int y = j; y < j + 5; y++)
                                {
                                    bmp2.SetPixel(x, y, Color.Red);
                                }
                            break;
                        case 3:
                            for (int x = i; x < i + 5; x++)
                                for (int y = j; y < j + 5; y++)
                                {
                                    bmp2.SetPixel(x, y, Color.Yellow);
                                }
                            break;
                        default:
                            for (int x = i; x < i + 5; x++)
                                for (int y = j; y < j + 5; y++)
                                {
                                    d = bmp.GetPixel(x,y);
                                    bmp2.SetPixel(x, y, Color.FromArgb(d.R, d.G, d.B));
                                }
                            break;
                    }
                }
            pictureBox1.Image = bmp2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Imagenes JPG|*.jpg";
            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ///Obtener las posiciones con system drawing y el drawing.imaging
            ///hay que leer un punto para eso leemos la imagen en mapa de bit
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            ///el COLOR = pixel 
            Color c = new Color();
            //picture box permite reducir el tamaño de la imagen esta vez no 
            c = bmp.GetPixel(e.X, e.Y);
            ///los tonos de la imagen original los pasamos globales
            cR = c.R;
            cG = c.G;
            cB = c.B;

            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
            cmR = 0;
            cmG = 0;
            cmB = 0;
            ///para textura
            for (int i=e.X;i<e.X+5;i++)
                for(int j = e.Y; j < e.Y+5; j++)
                {
                    c = bmp.GetPixel(i, j);
                    cmR = cmR + c.R;
                    cmG = cmG + c.G;
                    cmB = cmB + c.B;
                }
            cmR /= 25;
            cmB /= 25;
            cmG /= 25;
            ///ya tengo el promedio del color en esa area
            textBox1.Text = cmR.ToString();
            textBox2.Text = cmG.ToString();
            textBox3.Text = cmB.ToString();
        }
    }
}
