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
                var solictud = new RestRequest($"api/EEstudiantes/{Convert.ToInt32(textBox1.Text.Trim())}");
                var respuesta = cliente.Get(solictud);
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
        }
    }
}
