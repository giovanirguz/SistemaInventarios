using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpArch.Data.NHibernate;
using SistemaInventarios.ApplicationServices;
using SistemaInventarios.ApplicationServices.Impl;
using SistemaInventarios.Model;
using SistemaInventarios.UI.ValidationHelper;

namespace SistemaInventarios.UI
{
    public partial class FrmClienteForm : Form
    {
        readonly IClienteService clienteService;

        public FrmClienteForm()
        {
            InitializeComponent();
            clienteService = new ClienteService(new Repository<Cliente>());
        }

        private void FrmClienteForm_Load(object sender, EventArgs e)
        {
            GenerateNoControl();
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente
                              {
                                  NoControl = int.Parse(txtNoControl.Text),
                                  Nombre = txtNombre.Text,
                                  ApellidoPaterno = txtApellidoPaterno.Text,
                                  ApellidoMaterno = txtApellidoMaterno.Text,
                                  Direccion = txtDireccion.Text,
                                  Rfc = txtRFC.Text,
                                  Telefono = txtTelefono.Text
                              };

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
            }
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
