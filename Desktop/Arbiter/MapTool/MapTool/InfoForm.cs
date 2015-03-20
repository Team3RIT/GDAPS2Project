using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MapTool
{
    public partial class InfoForm : Form
    {
        public int x;
        
        MainForm owner;
        public InfoForm(MainForm own)
        {
            InitializeComponent();
            owner = own;
            this.TopMost = true;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            bool success;
            success = Int32.TryParse(this.textBoxX.Text,out x);
            if (!success || x < 1 || x > 20)
            {
                textBoxX.Clear();
                return;
            }
            

            owner.MapBuilder(x);
            this.Close();

        }

       
    }
}
