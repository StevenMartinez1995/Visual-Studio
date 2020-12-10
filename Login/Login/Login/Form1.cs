using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
            MessageBox.Show("La conexion fue un exito");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            SqlDataAdapter da = new SqlDataAdapter("SELECT COUNT(*) FROM USUARIOS WHERE USERS='" + 
            textBox1.Text + "' AND PASSWORDS ='" + textBox2.Text + "'", Conexion.Conectar());

            DataTable dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows [0][0].ToString()=="1")
            {
                this.Hide();
                new Inicio().Show();
            }
            else
            {
                MessageBox.Show("Acceso Denegado");
            }
        }
    }
}
