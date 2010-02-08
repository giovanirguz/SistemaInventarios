using System;
using System.Windows.Forms;
using SharpArch.Data.NHibernate;
using SistemaInventarios.ApplicationServices;
using SistemaInventarios.ApplicationServices.Impl;
using SistemaInventarios.Model;
using SistemaInventarios.UI.Mappers;
using SistemaInventarios.UI.Mappers.Impl;
using SistemaInventarios.UI.Models;
using SistemaInventarios.UI.ValidationHelper;

namespace SistemaInventarios.UI
{
    public partial class FrmClienteForm : Form
    {
        readonly IClienteService clienteService;
        readonly IClienteMapper clienteMapper;

        public FrmClienteForm()
        {
            InitializeComponent();
            clienteService = new ClienteService(new Repository<Cliente>());
            clienteMapper = new ClienteMapper(new Repository<Cliente>());
        }

        private void FrmClienteForm_Load(object sender, EventArgs e)
        {
            GenerateNoControl();
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            var clienteForm = new ClienteForm
                              {
                                  Id = int.Parse(lblHiddenId.Text),
                                  NoControl = int.Parse(txtNoControl.Text),
                                  Nombre = txtNombre.Text,
                                  ApellidoPaterno = txtApellidoPaterno.Text,
                                  ApellidoMaterno = txtApellidoMaterno.Text,
                                  Direccion = txtDireccion.Text,
                                  Rfc = txtRFC.Text,
                                  Telefono = txtTelefono.Text
                              };

            var cliente = clienteMapper.Map(clienteForm);

            if (!cliente.IsValid())
            {
                MessageBox.Show(ModelStateValidation.ValidationResults(cliente.ValidationResults()));
            }
            else
            {
                clienteService.SaveCliente(cliente);

                MessageBox.Show(cliente.Nombre + " fue grabado exitosamente");
                LimpiarControles();
                txtNombre.Focus();
                lblHiddenId.Text = "0";
            }
        }

        private void txtNombreBusqueda_TextChanged(object sender, EventArgs e)
        {
            lsvClientes.Items.Clear();

            if (txtNombreBusqueda.Text == "") { return; }

            var clientes = clienteService.GetClientesByName(txtNombreBusqueda.Text);

            foreach (var cliente in clientes)
            {
                lsvClientes.Items.Add(new ListViewItem(new[] { cliente.NoControl.ToString(), cliente.Nombre, cliente.Rfc }));
            }
        }

        private void lsvClientes_Click(object sender, EventArgs e)
        {
            gpbAlta.Visible = true;
            gpbBusqueda.Visible = false;
            tsbGuardar.Enabled = true;
            var clienteForm = clienteMapper.Map(clienteService.GetClienteByNoControl(int.Parse(lsvClientes.Items[lsvClientes.FocusedItem.Index].SubItems[0].Text)));

            txtNoControl.Text = clienteForm.NoControl.ToString();
            txtNombre.Text = clienteForm.Nombre;
            txtApellidoPaterno.Text = clienteForm.ApellidoPaterno;
            txtApellidoMaterno.Text = clienteForm.ApellidoMaterno;
            txtRFC.Text = clienteForm.Rfc;
            txtDireccion.Text = clienteForm.Direccion;
            txtTelefono.Text = clienteForm.Telefono;
            lblHiddenId.Text = clienteForm.Id.ToString();
        }

        #region Form Event Handlers
        private void tsbSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            MostrargpbAltaClientes();
            LimpiarControles();
        }

        private void tsbBusqueda_Click(object sender, EventArgs e)
        {
            MostrargpbBusquedaClientes();
            LimpiarControles();
        }

        private void MostrargpbAltaClientes()
        {
            gpbAlta.Visible = true;
            gpbBusqueda.Visible = false;
            txtNombre.Focus();
            tsbGuardar.Enabled = true;
            lblHiddenId.Text = "0";
            GenerateNoControl();
        }

        private void MostrargpbBusquedaClientes()
        {
            gpbAlta.Visible = false;
            gpbBusqueda.Visible = true;
            txtNombreBusqueda.Focus();
            tsbGuardar.Enabled = false;
        }

        private void LimpiarControles()
        {
            foreach (Control control in Controls)
            {
                if (!(control is GroupBox)) continue;
                for (var i = 0; i < control.Controls.Count; i++)
                {
                    if (!(control.Controls[i] is TextBox)) continue;
                    control.Controls[i].Text = "";
                }
            }

            GenerateNoControl();
        }

        private void GenerateNoControl()
        {
            var noControl = new Random();

            txtNoControl.Text = Convert.ToString(noControl.Next());
        }
        #endregion Form Event Handlers
    }
}
