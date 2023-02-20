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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormConsumirServicio
{
    public partial class frmEditar : Form
    {
        public frmEditar()
        {
            InitializeComponent();
        }

        private void frmEditar_Load(object sender, EventArgs e)
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIdEst.Text.Trim() == "" || txtNombres.Text.Trim() == "" || txtApellidos.Text.Trim() == "")
            {
                MessageBox.Show("Nombres y Apellidos Requeridos", "Aviso", MessageBoxButtons.OK);
                txtNombres.Focus();
            }
            else
            {
                RestClient cliente = new RestClient("https://localhost:44306/");
                var solicitud = new RestRequest($"api/EEstudiantes/{Convert.ToInt32(txtIdEst.Text.Trim())}");
                solicitud.AddJsonBody(new Estudiantes()
                {
                    IdEstudiante = Convert.ToInt32(txtIdEst.Text.Trim()),
                    Matricula = txtMatricula.Text,
                    Nombre = txtNombres.Text,
                    Apellido = txtApellidos.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text
                });
                var respuesta = cliente.Put(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    InicializarControles();
                    LimpiarControles();
                }
            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            InicializarControles();
            LimpiarControles();
        }
        //OCULTAMOS EL FORMULARIO Y MOSTRAMOS EN frmPrincipal en forma de dialogo modal
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnBuscarId_Click(object sender, EventArgs e)
        {
            if (txtBuscarId.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese un Identificador Válido", "Aviso", MessageBoxButtons.OK);
            }
            else
            {
                RestClient cliente = new RestClient("https://localhost:44306/");
                var solicitud = new RestRequest($"api/EEstudiantes/{Convert.ToInt32(txtBuscarId.Text.Trim())}");
                var respuesta = cliente.Get(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = respuesta.Content;
                    var jsonResult = JsonConvert.DeserializeObject(content).ToString();
                    List<Estudiantes> est = new List<Estudiantes>();
                    var result = JsonConvert.DeserializeObject<Estudiantes>(jsonResult);
                    if (result != null)
                    {
                        est.Add(
                            new Estudiantes(
                            result.IdEstudiante, result.Matricula, result.Nombre, result.Apellido, result.Telefono, result.Direccion));
                    }
                    if (est.ToList().Count > 0)
                    {
                        dataGridView1.DataSource = est;
                        dataGridView1.Refresh();
                    }

                }
            }
        }

        private void btnBuscarPersona_Click(object sender, EventArgs e)
        {
            if (txtBuscarPersona.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese un Identificador Válido", "Aviso", MessageBoxButtons.OK);
            }

            else

            {
                RestClient cliente = new RestClient("https://localhost:44306/");
                var solicitud = new RestRequest($"api/EEstudiantes/?name={txtBuscarPersona.Text.Trim()}");
                var respuesta = cliente.Get(solicitud);
                if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Estudiantes> data = JsonConvert.DeserializeObject<List<Estudiantes>>(respuesta.Content);
                    dataGridView1.DataSource = data;
                    dataGridView1.Refresh();
                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Estudiantes estudiantes = (Estudiantes)dataGridView1.CurrentRow.DataBoundItem;
            txtMatricula.Text = estudiantes.Matricula;
            txtNombres.Text = estudiantes.Nombre;
            txtApellidos.Text = estudiantes.Apellido;
            txtTelefono.Text = estudiantes.Telefono;
            txtDireccion.Text = estudiantes.Direccion;
            txtIdEst.Text = Convert.ToString(estudiantes.IdEstudiante);
        }
    }
}
