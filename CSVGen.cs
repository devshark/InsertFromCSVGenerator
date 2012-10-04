using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace InsertFromCSVGenerator
{
    public partial class CSVGen : Form
    {
        public String Filename = null;
        public CSVGen()
        {
            InitializeComponent();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV|*.csv", Title = "Find CSV file", CheckFileExists = true };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.Filename = ofd.FileName;
            }
            else
            {
                this.Filename = null;
            }
        }

        private void generateSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Filename == null)
                    throw new Exception("No file specified.");
                String[] lines = File.ReadAllLines(this.Filename, Encoding.UTF8);
                String main = String.Format("insert into {0}({1}) values", Path.GetFileNameWithoutExtension(this.Filename), lines[0].Replace(Convert.ToChar('"'),Convert.ToChar(" ")).Replace(Convert.ToChar("'"),Convert.ToChar(" ")));
                for (int i = 1; i < lines.Length; i++)
                {
                    main += String.Format("({0}),\n", lines[i]);
                }
                main = main.Remove(main.Length - 1);
                txtSQL.Text = main;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void clipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Filename == null && this.txtSQL.Text.Trim().Length == 0)
                    throw new Exception("No file chosen and no SQL script available.");
                Clipboard.SetText(txtSQL.Text, TextDataFormat.UnicodeText);
                MessageBox.Show("Copied to clipboard.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Filename == null && txtSQL.Text.Trim().Length == 0)
                    throw new Exception("No file chosen and no SQL script available.");
                SaveFileDialog sfd = new SaveFileDialog() { OverwritePrompt = true, Filter = "Structured Query Language|*.sql", InitialDirectory = Path.GetDirectoryName(this.Filename),Title="Save SQL as",AddExtension=true };
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, txtSQL.Text, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

    }
}
