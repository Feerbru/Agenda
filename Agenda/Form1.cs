namespace Agenda
{
    public partial class Form1 : Form
    {
        List<Contacto> contactos = new List<Contacto>();
        int indice = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Contacto persona = new Contacto();

            persona.Nombre = tbNombre.Text;
            persona.Apellido = tbApellido.Text;
            persona.FechaNacimiento = dtpFecha.Value.ToString();
            persona.Genero = cbGenero.Text;
            persona.Telefono = tbTelefono.Text;
            persona.Direccion = tbDireccion.Text;
            persona.Email = tbEmail.Text;

            if (indice > -1)
            {
                contactos[indice] = persona;
                indice = -1;
            }
            else
            {
                contactos.Add(persona);
            }
            Limpiar();
            ActualizarGrid();
        }

        private void Limpiar()
        {
            tbNombre.Text = null;
            tbApellido.Text = null;
            tbDireccion.Text = null;
            cbGenero.Text = null;
            dtpFecha.Value = DateTime.Now;
            tbTelefono.Text = null;
            tbEmail.Text = null;

        }

        private void ActualizarGrid()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = contactos;
            dgvDatos.ClearSelection();
        }

        private void dgvDatos_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow reglon = dgvDatos.SelectedRows[0];
            indice = dgvDatos.Rows.IndexOf(reglon);
            Contacto per = contactos[indice];

            tbNombre.Text = per.Nombre;
            tbApellido.Text = per.Apellido;
            dtpFecha.Value = Convert.ToDateTime(per.FechaNacimiento);
            cbGenero.Text = per.Genero;
            tbTelefono.Text = per.Telefono;
            tbDireccion.Text = per.Direccion;
            tbEmail.Text = per.Email;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (indice > -1)
            {
                contactos.RemoveAt(indice);
                Limpiar();
                ActualizarGrid();
            }
            else
            {
                MessageBox.Show("Debe seleccionar contacto para eliminar");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("Datos.txt", true))
            {
                foreach (Contacto persona in contactos)
                {
                    sw.WriteLine(persona.Nombre + "|" +
                    persona.Apellido + "|" +
                    persona.FechaNacimiento + "|" +
                    persona.Genero + "|" +
                    persona.Direccion + "|" +
                    persona.Telefono + "|" +
                    persona.Email
                        );
                }
                sw.Close();
            }
            Limpiar();
            ActualizarGrid();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("Datos.txt"))
            {
                string linea;

                while ((linea = sr.ReadLine()) != null)
                {
                    int posicion;
                    Contacto contacto = new Contacto();
                    posicion = linea.IndexOf("|");

                    contacto.Nombre = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    contacto.Apellido = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    contacto.FechaNacimiento = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    contacto.Genero = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    contacto.Telefono = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    contacto.Direccion = linea.Substring(0, posicion);
                    //linea = linea.Substring(posicion + 1);
                    //posicion = linea.IndexOf("|");

                    contacto.Email = linea.Substring(0, posicion);

                    contactos.Add(contacto);
                }

                sr.Close();

            }

            ActualizarGrid();
        }
        
    }
}