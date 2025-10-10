using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAIGrupoG.Playero
{
    public partial class PlayeroForm : Form
    {
        public PlayeroModelo modelo = new();
        public PlayeroForm()
        {
            InitializeComponent();
        }
    }
}
