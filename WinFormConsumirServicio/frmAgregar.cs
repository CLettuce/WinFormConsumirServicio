using Newtonsoft.Json;
using RestSharp;
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
    public partial class frmAgregar : Form
    {
        public frmAgregar()
        {
            InitializeComponent();
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }
        
        //Mandamos a traer todos los datos de la API al momento de cargar el Frm desde el DTO
        protected void InicializarControles()
        {
            //textBox1.Text = "";
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
        //Modificadores de acceso
        protected void LimpiarControles()
        {
            txtIdEst.Text = "";
            txtMatricula.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Estudiantes estudiantes = (Estudiantes)dataGridView1.CurrentRow.DataBoundItem;
            txtMatricula.Text = estudiantes.Matricula;
            txtNombres.Text = estudiantes.Nombre;
            txtApellidos.Text = estudiantes.Apellido;
            txtTelefono.Text = estudiantes.Telefono;
            txtDireccion.Text = estudiantes.Direccion;
            txtIdEst.Text = Convert.ToString(estudiantes.IdEstudiante);
        }
        private void btnRecargar_Click(object sender, EventArgs e)
        {
            InicializarControles();
            LimpiarControles();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text.Trim() == "")
            {
                MessageBox.Show("El número de Matricula es Requerida", "Aviso", MessageBoxButtons.OK);
                txtMatricula.Focus();
            }
            else if (txtNombres.Text.Trim() == "")
            {
                MessageBox.Show("Los Nombres son Requeridos", "Aviso", MessageBoxButtons.OK);
                txtNombres.Focus();
            }
            else if (txtApellidos.Text.Trim() == "")
            {
                MessageBox.Show("Los Apellidos son Requeridos", "Aviso", MessageBoxButtons.OK);
                txtNombres.Focus();
            }
            else if (txtTelefono.Text.Trim() == "")
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

        //OCULTAMOS EL FORMULARIO Y MOSTRAMOS EN frmPrincipal en forma de dialogo modal dsds
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
