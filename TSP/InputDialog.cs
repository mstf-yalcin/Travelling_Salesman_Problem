using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSP
{
    public partial class InputDialog : Form
    {
        public  string distance { get; set; }
        public InputDialog()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void okClick(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                distance = textBox1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter distance!");
            }
        }
 
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
 
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
