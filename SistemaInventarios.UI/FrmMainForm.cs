using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaInventarios.ApplicationSettings.NHibernateSettings;
using SistemaInventarios.ApplicationSettings.WindsorSettings;

namespace SistemaInventarios.UI
{
    public partial class FrmMainForm : Form
    {
        private FrmClienteForm frmClienteForm;
        private readonly WindsorSetup windsorSetup;

        public FrmMainForm()
        {
            InitializeComponent();
            windsorSetup = new WindsorSetup();
            windsorSetup.InitializeInterfaces();
        }

        private void FrmMainForm_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmClienteForm == null || frmClienteForm.IsDisposed)
                frmClienteForm = new FrmClienteForm();

            frmClienteForm.MdiParent = this;
            frmClienteForm.Show();
            frmClienteForm.BringToFront();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Setup()
        {
            var configSetup = new NHibernateConfigSetup();
            if (!configSetup.VerifyNHConfigFileExist())
            {
                if (MessageBox.Show("El archivo de conexión a la base de datos no existe\n" +
                                    "para poder hacer uso del sistema por favor importe el archivo.\n\n",
                                    "Inventarios",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var ofdBuscarArchivo = new OpenFileDialog
                                               {
                                                   InitialDirectory = "c:\\",
                                                   Filter = "Data files (*.config)|*.config",
                                                   FilterIndex = 2,
                                                   RestoreDirectory = false
                                               };
                    if (ofdBuscarArchivo.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(ofdBuscarArchivo.FileName, "C:\\NHibernateSettings\\" + ofdBuscarArchivo.SafeFileName);
                    }
                    if (ofdBuscarArchivo.ShowDialog() == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                }
                else
                    Application.Exit();
            }
            else
                configSetup.InitializeNHibernate();
        }
    }
}
