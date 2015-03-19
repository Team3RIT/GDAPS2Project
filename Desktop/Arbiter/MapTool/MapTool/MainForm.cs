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
    public partial class MainForm : Form
    {
        public InfoForm form;
        public PictureBox[,] tiles;
        public int[,] nums;
        public MainForm()
        {
            InitializeComponent();
            form = new InfoForm(this);
            form.Show();
            
        }

        public void MapBuilder(int x, int y)
        {
            tiles = new PictureBox[x,y];
            nums = new int[x, y];
            this.Width = 30 * x + 50;
            this.Height = 30 * y + 300;
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    tiles[i,j] = new PictureBox();
                    nums[i, j] = 0;

                    // Set the location and size of the PictureBox control. 
                    this.tiles[i, j].Location = new System.Drawing.Point(30*i+25, 30*j+50);
                    this.tiles[i, j].Size = new System.Drawing.Size(30, 30);
                    this.tiles[i, j].TabStop = false;

                    // Set the SizeMode property to the StretchImage value.  This 
                    // will shrink or enlarge the image as needed to fit into 
                    // the PictureBox. 
                    this.tiles[i, j].SizeMode = PictureBoxSizeMode.StretchImage;

                    // Set the border style to a three-dimensional border. 
                    this.tiles[i, j].BorderStyle = BorderStyle.Fixed3D;

                    // Add the PictureBox to the form. 
                    this.Controls.Add(this.tiles[i, j]);
                    

                }
            }

            Button submitButton = new Button();
            submitButton.Location = new System.Drawing.Point(30, 30 * y + 80);
            submitButton.Text = "Submit";
            this.Controls.Add(submitButton);
        }
    }
}
