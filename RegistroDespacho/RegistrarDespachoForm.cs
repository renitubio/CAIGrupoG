using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.RegistroDespacho
{
    public partial class RegistrarDespachoForm : Form
    {
        public RegistrarDespachoForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Add("Paquete A");
            checkedListBox1.Items.Add("Paquete B");
            checkedListBox1.Items.Add("Paquete C");
            checkedListBox1.Items.Add("Paquete D");

            this.Controls.Add(checkedListBox1);
        }

        private void RegistrarDespachoForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
