using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormConsumirServicio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }
        protected void InicializarControles()
        {
            textBox1.Text = "";
            RestClient cliente = new RestClient("https://localhost:44306/");
            var solictud = new RestRequest("api/EEstudiantes");
            var respuesta = cliente.Get(solictud);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<Estudiantes> data = JsonConvert.DeserializeObject<List<Estudiantes>>(respuesta.Content);
                dataGridView1.DataSource = data;
                dataGridView1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim() == "" )
            {
                MessageBox.Show("Ingrese un Identificador Válido", "Aviso", MessageBoxButtons.OK);
            }
            else
            {
                RestClient cliente = new RestClient("https://localhost:44306/");
                var solicitud = new RestRequest($"api/EEstudiantes/{Convert.ToInt32(textBox1.Text.Trim())}");
                var respuesta = cliente.Get(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = respuesta.Content;
                    var jsonResult = JsonConvert.DeserializeObject(content).ToString();
                    List<Estudiantes> est = new List<Estudiantes>();
                    var result = JsonConvert.DeserializeObject<Estudiantes>(jsonResult);
                    if(result != null)
                    {
                       est.Add(
                           new Estudiantes(
                           result.IdEstudiante, result.Matricula, result.Nombre, result.Apellido, result.Telefono, result.Direccion));
                    }
                    if(est.ToList().Count > 0)
                    {
                        dataGridView1.DataSource = est;
                        dataGridView1.Refresh();
                    }
                   
                }
            }
        }
        //
        private void button2_Click(object sender, EventArgs e)
        {
            InicializarControles();
            LimpiarControles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ( txtIdEst.Text.Trim() == "" || txtNombres.Text.Trim() == "" || txtApellidos.Text.Trim() == "")
            {
                MessageBox.Show("Nombres y Apellidos Requeridos", "Aviso", MessageBoxButtons.OK);
                txtNombres.Focus();
            }
            else
            {
                RestClient cliente = new RestClient("https://localhost:44306/");
                var solicitud = new RestRequest($"api/EEstudiantes/{Convert.ToInt32(txtIdEst.Text.Trim())}");
                solicitud.AddJsonBody(new Estudiantes(){
                    IdEstudiante = Convert.ToInt32(txtIdEst.Text.Trim()),
                    Matricula = txtMatricula.Text,
                    Nombre= txtNombres.Text,
                    Apellido= txtApellidos.Text,
                    Telefono= txtTelefono.Text,
                    Direccion= txtDireccion.Text });
               var respuesta =  cliente.Put(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    InicializarControles();
                    LimpiarControles();
                }
            }

        }
        protected void LimpiarControles()
        {
            txtIdEst.Text = "";
            txtMatricula.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Estudiantes estudiantes = (Estudiantes)dataGridView1.CurrentRow.DataBoundItem;
            txtMatricula.Text = estudiantes.Matricula;
            txtNombres.Text = estudiantes.Nombre;
            txtApellidos.Text = estudiantes.Apellido;
            txtTelefono.Text = estudiantes.Telefono;
            txtDireccion.Text = estudiantes.Direccion;
            txtIdEst.Text = Convert.ToString(estudiantes.IdEstudiante);
        }
        
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
            this.panel1.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text.Trim() == "")
            {
                MessageBox.Show("El número de Matricula es Requerida", "Aviso", MessageBoxButtons.OK);
                txtMatricula.Focus();
            }
            else if (txtNombres.Text.Trim() == "" )
            {
                MessageBox.Show("Los Nombres son Requeridos", "Aviso", MessageBoxButtons.OK);
                txtNombres.Focus();
            }
            else if (txtApellidos.Text.Trim() == "")
            {
                MessageBox.Show("Los Apellidos son Requeridos", "Aviso", MessageBoxButtons.OK);
                txtNombres.Focus();
            }
            else if(txtTelefono.Text.Trim() == "")
            {
                MessageBox.Show("El Telefono es Requerido", "Aviso", MessageBoxButtons.OK);
                txtNombres.Focus();
            }
            else if (txtDireccion.Text.Trim() == "")
            {
                MessageBox.Show("La Dirección es Requerida", "Aviso", MessageBoxButtons.OK);
                txtMatricula.Focus();
            }

            else
            {
                RestClient cliente = new RestClient("https://localhost:44306/");
                var solicitud = new RestRequest("api/EEstudiantes/");
                solicitud.AddJsonBody(new Estudiantes()
                {
                    
                    Matricula = txtMatricula.Text,
                    Nombre = txtNombres.Text,
                    Apellido = txtApellidos.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text
                });
                var respuesta = cliente.Post(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    InicializarControles();
                    LimpiarControles();
                }
            }
        }
    }
}
