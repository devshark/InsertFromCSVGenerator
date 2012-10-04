using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace InsertFromCSVGenerator
{
    public partial class Form1 : Form
    {
        public String Filename = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV|*.csv", Title = "Find CSV file",CheckFileExists=true };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.Filename = ofd.FileName;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (Filename == null)
                MessageBox.Show("No file specified.");
            String[] lines = File.ReadAllLines(this.Filename, Encoding.UTF8);
            String main = String.Format("insert into {0}({1}) values", Path.GetFileNameWithoutExtension(this.Filename),lines[0]);
            for (int i = 1; i < lines.Length; i++)
            {
                main += String.Format("({0}),", lines[i]);
            }
            main = main.Remove(main.Length - 1);
            txtSQL.Text = main;
        }

    }
}
