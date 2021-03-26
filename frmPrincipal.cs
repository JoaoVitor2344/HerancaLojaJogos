using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HerançaLojajogos
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void jogosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 jogo = new Form1();
            jogo.Show();
        }

        private void plataformasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 plataforma = new Form2();
            plataforma.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
