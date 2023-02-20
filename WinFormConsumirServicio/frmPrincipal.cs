using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormConsumirServicio
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
       
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnSlide_Click(object sender, EventArgs e)
        {
           
            if (pnVertical.Width == 228)
                pnVertical.Width = 70;
            else
                pnVertical.Width = 228;
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro de Cerrar La Aplicación?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mini_Click(object sender, EventArgs e)
        {
           
        }
        //private void AbrirFormEnPanel(object Formhijo)
        //{
        //    if (this.pnContenedor.Controls.Count > 0)
        //        this.pnContenedor.Controls.RemoveAt(0);
        //    Form fh = Formhijo as Form;
        //    fh.TopLevel = false;
        //    fh.Dock = DockStyle.Fill;
        //    this.pnContenedor.Controls.Add(fh);
        //    this.pnContenedor.Tag = fh;
        //    fh.Show();
        //}


        //AHORA SE HACE CON LA PROPIEDAD MdiParent sin arreglos y con condicional para
        //evitar la duplicacion de los formularios
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            bool FrmThis = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmRegistro))
                    FrmThis = true;
            }

            if (FrmThis == true)
                MessageBox.Show("NO SE PUEDE ABRIR EL MISMO FORMULARIO MAS DE UNA VEZ");
            else
            {
                frmRegistro info = new frmRegistro();
                info.MdiParent = this;
                info.Dock = DockStyle.Fill;
                //Nota: Este es un arreglo para adaparte dentro de x Objeto el frmQue mandamos a traer...
                //pnContenedor.Controls[0].Controls.Add(info);
                this.pnContenedor.Controls.Add(info);
                info.Show();
            }
        }

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
           
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            bool FrmThis = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmEditar))
                    FrmThis = true;
            }

            if (FrmThis == true)
                MessageBox.Show("NO SE PUEDE ABRIR EL MISMO FORMULARIO MAS DE UNA VEZ");
            else
            {
                frmEditar info = new frmEditar();
                info.MdiParent = this;
                info.Dock = DockStyle.Fill;
                //pnContenedor.Controls[0].Controls.Add(info);
                this.pnContenedor.Controls.Add(info);
                info.Show();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool FrmThis = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(frmAgregar))
                    FrmThis = true;
            }

            if (FrmThis == true)
                MessageBox.Show("NO SE PUEDE ABRIR EL MISMO FORMULARIO MAS DE UNA VEZ");
            else
            {
                frmAgregar info = new frmAgregar();
                info.MdiParent = this;
                info.Dock = DockStyle.Fill;
                //pnContenedor.Controls[0].Controls.Add(info);
                this.pnContenedor.Controls.Add(info);
                info.Show();
                
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Seguro de Cerrar Sesión?", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                frmLogin frmLogin = new frmLogin();
                this.Hide();
                frmLogin.Show();
            }
            
        }
    }
}
