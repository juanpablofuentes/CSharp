using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minions
{
    public partial class Form1 : Form
    {
        #region COLORS
        private Color c_panels = System.Drawing.ColorTranslator.FromHtml("#393939");
        private Color c_hoverControls = System.Drawing.ColorTranslator.FromHtml("#535353");
        #endregion

        #region MOVEPANEL
        private const int ButtonDown = 0xA1;
        private const int HtCaption = 0x2;
        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private List<Alumno> alumnos = new List<Alumno>
        {
            new Alumno("Adria_R"),
            new Alumno("Guillem"),
            new Alumno("Sergi"),
            new Alumno("David"),
            new Alumno("Edu"),
            new Alumno("Zaka"),
            new Alumno("Northem"),
            new Alumno("Rodrigo"),
            new Alumno("Jaume"),
            new Alumno("Adria_G"),
            new Alumno("Adam"),
            new Alumno("Manel"),
            new Alumno("Alberto")
        };
        private List<Alumno> alumnosAcabados = new List<Alumno>();
        private void Form1_Load(object sender, EventArgs e)
        {
            #region Barra superior
            ptb_close.SizeMode = PictureBoxSizeMode.StretchImage;
            ptb_close.BackColor = Color.OrangeRed;
            ptb_close.MouseLeave += (se, ev) => ptb_close.BackColor = Color.OrangeRed;
            ptb_close.MouseEnter += (se, ev) => ptb_close.BackColor = Color.Red;
            ptb_close.Click += (se, ev) => this.Close();

            ptb_minimize.SizeMode = PictureBoxSizeMode.StretchImage;
            ptb_minimize.BackColor = Color.OrangeRed;
            ptb_minimize.MouseLeave += (se, ev) => ptb_minimize.BackColor = Color.OrangeRed;
            ptb_minimize.MouseEnter += (se, ev) => ptb_minimize.BackColor = Color.DarkOrange;
            ptb_minimize.Click += (se, ev) => this.WindowState = FormWindowState.Minimized;

            barraSuperior.BackColor = Color.OrangeRed;
            barraSuperior.MouseDown += MovePanel;
            #endregion

            Icon icon = new Icon(Application.StartupPath + "\\Img\\Icon.ico");
            this.Icon = icon;
            foreach (var item in alumnos)
            {
                item.salidas = 1;
            }
            foreach (var item in Controls)
            {
                if (item is PictureBox p)
                {
                    p.ImageLocation = Application.StartupPath + "\\Img\\minion.png";
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void Btn_ruleta_Click(object sender, EventArgs e)
        {
            if (TodosHanPresentado())
            {
                int random = 0;
                lbl_pringado.Text = "";

                Random r = new Random();

                for (int i = 0; i < 10; i++)
                {
                    foreach (var item in Controls)
                    {
                        if (item is PictureBox p)
                        {
                            if (alumnos.Any(x => x.nombre == p.Name))
                            {
                                p.Hide();
                            }
                        }
                    }
                    random = r.Next(alumnos.Count);
                    foreach (var item in Controls)
                    {
                        if (item is PictureBox p)
                        {
                            if (p.Name == alumnos[random].nombre)
                            {
                                p.Show();
                                p.Refresh();
                            }
                        }
                    }
                    this.Refresh();

                    Thread.Sleep(15);
                }

                lbl_pringado.Text = alumnos[random].ToString();
                alumnos[random].salidas--;

                RevisarLosVivos(alumnos[random]);
            }
        }

        private bool TodosHanPresentado()
        {
            if (alumnos.Count == 0)
            {
                MessageBox.Show("Todos ya han presentado!");
                
                this.Close();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void RevisarLosVivos(Alumno alumno)
        {
            if (!alumno.valido)
            {
                alumnos.Remove(alumno);
                alumnosAcabados.Add(alumno);
                foreach (var item in Controls)
                {
                    if (item is PictureBox p)
                    {
                        if (p.Name == alumno.nombre)
                        {
                            p.ImageLocation = Application.StartupPath + "\\Img\\acabado.png";
                            p.Show();
                        }
                    }
                }
            }
        }
        private Image TakeImg(string mode, string name)
        {
            return Image.FromFile(Application.StartupPath + "\\Img\\" + mode + "_" + name + ".png");
        }
        private void MovePanel(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, ButtonDown, HtCaption, 0);
            }
        }
    }
}