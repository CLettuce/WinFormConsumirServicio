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
    public partial class frmRegistro : Form
    {
        public frmRegistro()
        {
            InitializeComponent();
        }

        private void frmRegistro_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }
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
        //OCULTAMOS EL FORMULARIO Y MOSTRAMOS EN frmPrincipal en forma de dialogo modal
        private void btnCerrar_Click(object sender, EventArgs e)
        {
           
            //frmPrincipal princ = new frmPrincipal();
           // princ.ShowDialog();
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

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            InicializarControles();
            
        }

    }
}
