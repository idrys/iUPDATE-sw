using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace iUPDATE_sw
{
    public partial class Form1 : Form
    {
        string pathToCad;
        
        public Form1()
        {
            pathToCad = String.Empty;
            InitializeComponent();
          
        }

        public string PathToCAD
        {
            get { return pathToCad; }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            pathToCad = this.openFileDialog1.FileName;
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            this.openFileDialog1.FileName = "iUPDATE.exe";
            this.openFileDialog1.ShowDialog();
            this.textBox1.Text = this.openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
