using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapTool
{
    public partial class InfoForm : Form
    {
        public int x;
        public int y;
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
                textBoxY.Clear();
                return;
            }
            success = Int32.TryParse(this.textBoxY.Text, out y);
            if (!success || y < 1 || y > 20)
            {
                textBoxX.Clear();
                textBoxY.Clear();
                return;
            }

            owner.MapBuilder(x, y);
            this.Close();

        }
    }
}
