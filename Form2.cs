using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFGui
{
    public partial class Form2 : Form
    {
        Form1 f1;
        public Form2(Form1 parent)
        {
            InitializeComponent();
            f1 = parent;
            textBox1.Text = f1.pathYtdlp;
            textBox2.Text = f1.pathFfmpeg;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            f1.pathYtdlp = textBox1.Text;
            f1.pathFfmpeg = textBox2.Text;
            f1.regP.SetValue("PathYtdlp", f1.pathYtdlp);
            f1.regP.SetValue("PathFfmpeg", f1.pathFfmpeg);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK) {
                textBox2.Text = openFileDialog2.FileName;
            }
        }
    }
}
