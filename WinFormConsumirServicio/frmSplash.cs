using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormConsumirServicio
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnSlide.Width += 3;
            if(pnSlide.Width >= 379)
            {
                timer1.Stop();
                frmPrincipal fmLogin = new frmPrincipal();
                fmLogin.Show();
                this.Hide();
            }
           
        }
    }
}
