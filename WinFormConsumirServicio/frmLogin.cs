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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormConsumirServicio
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            InitializeMyControl();
            txtU.Focus();
        }
        protected void InitializeMyControl()
        {
            txtC.Text = "";
            // El carácter de la contraseña se cambia a asteriscos.
            txtC.PasswordChar = '*';
            // El control no permitirá más de 80 caracteres.
            txtC.MaxLength = 80;
            
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtU.Text.Trim() == "")
            {
                MessageBox.Show("Se Necesita Ingresar un Usuario", "Aviso", MessageBoxButtons.OK);
                txtU.Focus();
            }
            else if (txtC.Text.Trim() == "")
            {
                MessageBox.Show("Se Necesita Ingresar una Contraseña", "Aviso", MessageBoxButtons.OK);
                txtC.Focus();
            }
            else if (txtU.Text == "admin" && txtC.Text == "123")
            {
                frmPrincipal frmInicio = new frmPrincipal();
                this.Hide();
                frmInicio.Show();
            }
            else if (txtU.Text == "usuario" && txtC.Text == "123")
            {
                frmPrincipal frmInicio = new frmPrincipal();
                this.Hide();
                frmInicio.Show();
            }
            else
            {
                MessageBox.Show("Contraseña Incorrecta", "Aviso", MessageBoxButtons.OK);
                txtU.Clear();
                txtC.Clear();
            }
        }

        private void minimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnEntrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtU.Text.Trim() == "")
                {
                    MessageBox.Show("Se Necesita Ingresar un Usuario", "Aviso", MessageBoxButtons.OK);
                    txtU.Focus();
                }
                else if (txtC.Text.Trim() == "")
                {
                    MessageBox.Show("Se Necesita Ingresar una Contraseña", "Aviso", MessageBoxButtons.OK);
                    txtC.Focus();
                }
                else if (txtU.Text == "admin" && txtC.Text == "123")
                {
                    frmPrincipal frmInicio = new frmPrincipal();
                    this.Hide();
                    frmInicio.Show();
                }
                else if (txtU.Text == "usuario" && txtC.Text == "123")
                {
                    frmPrincipal frmInicio = new frmPrincipal();
                    this.Hide();
                    frmInicio.Show();
                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta", "Aviso", MessageBoxButtons.OK);
                    txtU.Clear();
                    txtC.Clear();
                }
            }
        }
    }
}
