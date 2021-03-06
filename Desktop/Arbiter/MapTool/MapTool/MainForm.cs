﻿using System;
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
    public partial class MainForm : Form
    {
        public InfoForm form; //gets dimension from user
        public PictureBox[,] tiles; //interface board
        public int[,] nums; //back end board
        public int dim; //dimension of board
        BinaryWriter writer; //writes to save file
        TextBox textBox; //for filename input
        Button submitButton; //tries to save file
        Label label; //for error output
        Button randomButton; //Calls MapGenerator
        Label instructionLabel; //gives instructions for the MapBuilder
        public MainForm()
        {
            InitializeComponent();
            form = new InfoForm(this);
            form.Show();


        }

        public void MapBuilder(int x)
        {
            dim = x;
            tiles = new PictureBox[x, x];
            nums = new int[x, x];
            this.AutoSize = true; // form autosizes based on contents
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    tiles[i, j] = new PictureBox();
                    nums[i, j] = 0;

                    // Set the location and size of the PictureBox control. 
                    this.tiles[i, j].Location = new System.Drawing.Point(30 * i + 25, 30 * j + 50);
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
                    tiles[i, j].Click += new EventHandler(pictureBox_Click); //when it's clicked, it'll call the function


                }
            }

            submitButton = new Button(); //for submitting custom map
            submitButton.Location = new System.Drawing.Point(30, 30 * x + 100);
            submitButton.Text = "Submit";
            submitButton.Click += new EventHandler(submitButton_Click);
            this.Controls.Add(submitButton);

            randomButton = new Button(); //for randomizing the map
            randomButton.Location = new System.Drawing.Point(210, 30 * x + 100);
            randomButton.Text = "Randomize";
            randomButton.Click += new EventHandler(randomButton_Click);
            this.Controls.Add(randomButton);


            textBox = new TextBox(); //for file name input
            textBox.Location = new System.Drawing.Point(30, 30*x + 80);
            this.Controls.Add(textBox);

            instructionLabel = new Label();
            instructionLabel.Location = new System.Drawing.Point(5, 5);
            instructionLabel.AutoSize = true; //allows label to stretch to fit text
            instructionLabel.Text = "Click on a tile to change it. Enter a file name and click Submit when finished.";
            this.Controls.Add(instructionLabel);
        }

        private void pictureBox_Click(object sender, System.EventArgs e)
        {

            PictureBox box = (PictureBox)sender;

            int x = (box.Location.X - 25) / 30;
            int y = (box.Location.Y - 50) / 30;

            switch (nums[x, y])
            {
                case 0: //empty
                    {
                        nums[x, y] = 1;
                        box.Image = Image.FromFile("../Images/structure.jpg");
                        return;

                    }
                case 1: //structure
                    {
                        nums[x, y] = 2;
                        box.Image = Image.FromFile("../Images/tower.png");
                        return;
                    }
                case 2: //tower
                    {
                        nums[x, y] = 0;
                        box.Image = null; //turns it back to nothing
                        return;
                    }

            }
        } //for when they click a picturebox

        private void submitButton_Click(object sender, System.EventArgs e)
        {
            bool success = true; //tracks if the map was written successfully

            label = new Label(); //label for output to user
            label.Location = new System.Drawing.Point(30, 30 * dim + 130);
            label.AutoSize = true;
            this.Controls.Add(label);

            if (textBox.Text.Length != 0) //if there is a filename
                success = MapWrite(textBox.Text); //will return true if the layout is acceptable
            else
                label.Text = "Please enter a file name.";

            if(!success) //towers were not good
            {
                
                label.Text = "Must include between 3 and 11 towers.";
                
            }
            else
            {
                MapClear(); //clears board so they can make a new map
                label.Text = "File Written Successfully. File can be found in Arbiter/bin/maps.";
            }
        }

        private bool MapWrite(string path) //could have just taken the path directly from textBox, I suppose, but parameters are the better practice
        {
            List<Point> towers = new List<Point>();
            List<Point> structures = new List<Point>();

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    switch (nums[i, j])
                    {
                        case 0: //blank
                            {
                                break;
                            }
                        case 1: //structure
                            {
                                structures.Add(new Point(i, j));
                                break;
                            }
                        case 2: //tower
                            {
                                towers.Add(new Point(i, j));
                                break;
                            }
                    }
                }
            }

            if (towers.Count < 3 || towers.Count > 11) //good number of towers
            {
                label.Text = "File path broken.";
                Environment.Exit(0);
            }

            try
            {
                writer = new BinaryWriter(File.Open("..\\..\\..\\..\\Arbiter\\bin\\maps\\" + path + ".dat", FileMode.Create));
            }
            catch (Exception ex)
            {
                return false;
            }
            writer.Write(dim); //first thing in the map file is dimension of board in spaces

            foreach(Point point in structures) //structures come first
            {
                writer.Write(point.X);
                writer.Write(point.Y);
            }
            writer.Write(-1); //signifies switch from structures to towers
            foreach (Point point in towers) //towers come second
            {
                writer.Write(point.X);
                writer.Write(point.Y);
            }

            writer.Close();
            return true;
        }

        private void randomButton_Click(object sender, System.EventArgs e)
        {
            MapGenerator.GenerateMap(dim, this);
        }

        public void MapClear()
        {
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    tiles[i, j].Image = null; //reset the tiles to blank
                    nums[i, j] = 0;           //reset the board array to default
                }
            }
        }

    }
}
