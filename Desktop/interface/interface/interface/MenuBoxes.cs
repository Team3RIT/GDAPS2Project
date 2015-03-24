using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections; //added this
using Microsoft.Xna.Framework; //added
using Microsoft.Xna.Framework.Content; //added
using Microsoft.Xna.Framework.Graphics; //added
using Microsoft.Xna.Framework.Input; //added
using Microsoft.Xna.Framework.Storage; //added
using Microsoft.Xna.Framework.GamerServices; //added
namespace @interface
{
    //holds all of the methods for displaying popup boxes and menus
    public class MenuBoxes
    {
        /// <summary>
        /// takes text, seperates it by its ',' into individual lines and returns them as an array
        /// </summary>
        /// <param name="text"></param>
        /// <param name="Length"></param>
        /// <param name="font"></param>
        /// <returns>an array of strings object</returns>
        private object[] WrapText(string text, float Length, SpriteFont font)
        {
            string[] words = text.Split(' '); //splits text by ',' and stores in array
            ArrayList Lines = new ArrayList(); //new arraylist to store all of the lines of text
            float linewidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;
            int CurLine = 0;
            Lines.Add(string.Empty);
            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);
                if (linewidth + size.X < Length)
                {
                    Lines[CurLine] += word + " ";
                    linewidth += size.X + spaceWidth;
                }
                else
                {
                    Lines.Add(word + " ");
                    linewidth = size.X + spaceWidth;
                    CurLine++;
                }
            }
            return Lines.ToArray();
        }
        /// <summary>
        /// displays a series of popup boxes in order to make a menu
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="text"></param>
        /// <param name="AssetPicturePath"></param>
        /// <param name="batch"></param>
        /// <param name="font"></param>
        /// <param name="chess"></param>
        /// <returns>a rectangle object</returns>

        public Rectangle CreateMainBox(Game1 chess, int width, int height)
        {
            //creates a rectangle to represent the entire menu screen and return it
            Rectangle Main = new Rectangle(); 
            //set the rectangle main box properties
            Main.Width = width; //700
            Main.Height = height; //400
            //rectangle drawn in center of the screen
            Main.X = ((chess.Window.ClientBounds.Width / 2) - (Main.Width / 2));
            Main.Y = ((chess.Window.ClientBounds.Height / 2) - (Main.Height / 2));
            return Main; //return the box
        }
        /// <summary>
        /// create a box for a single line of text, like a title
        /// </summary>
        /// <param name="TitleText"></param>
        /// <param name="width"></param>
        /// <param name="font"></param>
        /// <param name="MainBox"></param>
        /// <returns>a rectangle object</returns>
        public Rectangle CreateTitleBox(string TitleText, int width, int Y, SpriteFont font, Rectangle MainBox)
        {
            Rectangle TitleBox = new Rectangle(); //new titlebox rectangle
            //set attributes for titlebox rectangle
            TitleBox.Width = width; //650
            TitleBox.Height = (int)font.MeasureString(TitleText).Y;
            double Padding = MainBox.Width / 2 - TitleBox.Width / 2;  //get the padding variable
            //need to cast padding from double to int to get an x and y value for the title box
            TitleBox.X = (int)Padding + MainBox.X;
            TitleBox.Y = Y; /*(int)Padding + MainBox.Y;*/ //using multiple titleboxes mean that they must have independant y's (still centered in the x direction from padding)

            return TitleBox; //returns the titlebox
        }
        /// <summary>
        /// creates a rectangle box with a line it
        /// </summary>
        /// <param name="mainbox"></param>
        /// <param name="titlebox"></param>
        /// <param name="thickness"></param>
        /// <returns>a rectangle which looks like a line</returns>
        public Rectangle CreateLine(Rectangle mainbox, Rectangle titlebox, int thickness)
        {
            //define a line that goes between the title and other text
            Rectangle TextLine = new Rectangle();
            double Padding = mainbox.Width / 2 - titlebox.Width / 2;  //get the padding variable for room around the line
            //define rectangle parameters
            TextLine.Width = mainbox.Width - (int)Padding * 2;
            TextLine.Height = thickness;
            TextLine.X = mainbox.X + (int)Padding;
            TextLine.Y = titlebox.Y + (int)(Padding * 1.2);

            return TextLine;
        }

        /// <summary>
        /// create a box to hold a picture
        /// </summary>
        /// <param name="PictureFile"></param>
        /// <param name="MainBox"></param>
        /// <param name="TitleBox"></param>
        /// <returns>a rectangle to hold the picture</returns>
        public Rectangle CreatePictureBox(string PictureFile, Rectangle MainBox, Rectangle TitleBox)
        {
            double Padding = MainBox.Width / 2 - TitleBox.Width / 2;  //get the padding variable
            //define PictureBox
            Rectangle PictureBox;
            if (PictureFile != string.Empty)
                PictureBox.Width = 200;
            else
                PictureBox.Width = 0;
            PictureBox.Height = 250;
            PictureBox.X = MainBox.X + (int)Padding;
            PictureBox.Y = MainBox.Y + TitleBox.Height + (int)Padding * 2;

            return PictureBox;
        }

        //creates the main menu
        public void DisplayMainMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {
            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });
            //define all of the necessary rectangles
            Rectangle MainMenuBox = CreateMainBox(checkers, 700, 800);
            Rectangle Title = CreateTitleBox("This Is Our Game's Title!", 500, 50, font, MainMenuBox);  //give mainmenubox a title
            Rectangle Line1 = CreateLine(MainMenuBox, Title, 2);
            Rectangle NewGame = CreateTitleBox("New Game", 500, 100, font, MainMenuBox);  //give mainmenubox a title
            Rectangle Line2 = CreateLine(MainMenuBox, NewGame, 2);
            Rectangle LoadGame = CreateTitleBox("Load Game", 500, 150, font, MainMenuBox);  //give mainmenubox a title
            Rectangle Line3 = CreateLine(MainMenuBox, LoadGame, 2);
            Rectangle Options = CreateTitleBox("Options", 500, 200, font,  MainMenuBox);  //give mainmenubox a title
            Rectangle Line4 = CreateLine(MainMenuBox, Options, 2);
            Rectangle Exit = CreateTitleBox("Exit", 500, 250, font,  MainMenuBox);  //give mainmenubox a title

            //   ^
            //   |
            //right now no lines are being drawn, deal with later

            //draw all of the rectangles

            //draw MainBox
            batch.Draw(FillText, MainMenuBox, Color.CornflowerBlue); //cornflowerblue
            //Draw TitleBox
            batch.Draw(FillText, Title, Color.Black); // draws the box
            batch.DrawString(font, "This is Our Game's Title!", new Vector2(Title.X, Title.Y), Color.Crimson); //draws the text within the box
            
            //Draw TitleBox
            batch.Draw(FillText, NewGame, Color.Black); // draws the box
            batch.DrawString(font, "NewGame", new Vector2(NewGame.X, NewGame.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, LoadGame, Color.Black); // draws the box
            batch.DrawString(font, "LoadGame", new Vector2(LoadGame.X, LoadGame.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, Options, Color.Black); // draws the box
            batch.DrawString(font, "Options", new Vector2(Options.X, Options.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, Exit, Color.Black); // draws the box
            batch.DrawString(font, "Exit", new Vector2(Exit.X, Exit.Y), Color.Crimson); //draws the text within the box

        }

        public void DisplayOptionsMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {



            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });
            
            //define rectangles boxes thingys
            Rectangle MainMenuBox = CreateMainBox(checkers, 700, 800);
            Rectangle Title = CreateTitleBox("Options Menu", 500, 50, font, MainMenuBox);  //give options a title

            Rectangle MainMenu = CreateTitleBox("Return to Main Menu", 500, 100, font, MainMenuBox);  //return to main menu

            //draw MainBox
            batch.Draw(FillText, MainMenuBox, Color.CornflowerBlue); //cornflowerblue

            //Draw TitleBox
            batch.Draw(FillText, Title, Color.Black); // draws the box
            batch.DrawString(font, "Options", new Vector2(Title.X, Title.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MainMenu, Color.Black); // draws the box
            batch.DrawString(font, "return to main menu", new Vector2(MainMenu.X, MainMenu.Y), Color.Crimson); //draws the text within the box


        }

        public void DisplayLoadGameMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {
            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //define rectangles boxes thingys
            Rectangle MainMenuBox = CreateMainBox(checkers, 700, 800); //create the box to hold the entire menu
            Rectangle Title = CreateTitleBox("Load Game Menu", 500, 50, font, MainMenuBox);  //give LoadGame a title
            
            Rectangle MainMenu = CreateTitleBox("Return to Main Menu", 500, 100, font, MainMenuBox);  //Return to main menu
            Rectangle Textbox = CreateTitleBox("this functionality currently doesn't exist!", 500, 150, font, MainMenuBox);  //give LoadGame a textbox

            //draw MainBox
            batch.Draw(FillText, MainMenuBox, Color.CornflowerBlue); //cornflowerblue
            
            //Draw TitleBox
            batch.Draw(FillText, Title, Color.Black); // draws the box
            batch.DrawString(font, "Load Game Menu", new Vector2(Title.X, Title.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MainMenu, Color.Black); // draws the box
            batch.DrawString(font, "Return to Main menu", new Vector2(MainMenu.X, MainMenu.Y), Color.Crimson); //draws the text within the box

            //draw the textbox
            batch.Draw(FillText, Textbox, Color.Black); // draws the box
            batch.DrawString(font, "This functionality currently doesn't exist!", new Vector2(Textbox.X, Textbox.Y), Color.Crimson); //draws the text within the textbox
        }








        //displays a test version of a menubox, one which uses all the different types of boxes (including a text box which does not have an independant method
        public void DisplayTestPopup(string Title, string text, string AssetPicturePath, SpriteBatch batch, SpriteFont font, Game1 chess)
        {
            //PLEASE NOT THAT THIS WILL DISPLAY A POPUP BOX CORRECTLY, IT WILL HOWEVER BE MANGLED AND STRANGE AND HAVE UNNECCESSARY PARTS, AND REALLY IS A FRANKENSTEIN METHOD
            
            //define all of the rectangles
            Texture2D FillText = new Texture2D(chess.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });
            
            //draw rectangle for main menu - default to the center of the screen
            Rectangle MainMenuBox = CreateMainBox(chess, 700, 400);
            Rectangle TitleBox = CreateTitleBox(Title, 650, 50, font, MainMenuBox);  //give mainmenubox a title
            Rectangle TextLine = CreateLine(MainMenuBox, TitleBox, 1); //give MainMenuBox a line below the title and above the text
            Rectangle PictureBox = CreatePictureBox(AssetPicturePath, MainMenuBox, TitleBox); //create a picturebox

            double Padding = MainMenuBox.Width / 2 - TitleBox.Width / 2;  //get the padding variable

            MainMenuBox.Height = PictureBox.Y - MainMenuBox.Y + PictureBox.Height + (int)Padding;  //change the mainmenubox height 
            //no longer defining rectangles///////////////////////////////////////
            //draw the textbody

           
            //define TextBody
            Rectangle TextBody;
            if (AssetPicturePath == string.Empty)
                TextBody.Width = MainMenuBox.Width - ((int)Padding * 2);
            else
                TextBody.Width = MainMenuBox.Width - ((int)Padding * 3) - PictureBox.Width;
            TextBody.Height = MainMenuBox.Height - ((int)Padding * 3) - TitleBox.Height;
            if (AssetPicturePath == string.Empty)
                TextBody.X = PictureBox.X;
            else
                TextBody.X = PictureBox.X + PictureBox.Width + (int)Padding;
            TextBody.Y = TitleBox.Y + TitleBox.Height + (int)Padding;


            //draw MainBox
            batch.Draw(FillText, MainMenuBox, Color.Wheat);
            //Draw PictureBox
            batch.Draw(FillText, PictureBox, Color.Green);
            if (AssetPicturePath != string.Empty) //if there actually is text for a picture asset, draw the asset, not just the box
                batch.Draw(chess.Content.Load<Texture2D>(/*AssetPath + */ AssetPicturePath.TrimStart(new char[] { '/' })), PictureBox, Color.White);
            //Draw TitleBox
            batch.Draw(FillText, TitleBox, Color.BlueViolet);
            batch.DrawString(font, Title, new Vector2(TitleBox.X, TitleBox.Y), Color.Blue);
            //Draw Line Between Title And TextBody
            batch.Draw(FillText, TextLine, Color.Gray);
            //Draw TextBody
            batch.Draw(FillText, TextBody, Color.Indigo);
            int LineNumber = 0;
            foreach (string Line in WrapText("Text", TextBody.Width, font))
            {
                batch.DrawString(font, Line, new Vector2(TextBody.X, TextBody.Y + (LineNumber * font.MeasureString(Line).Y)), Color.Black);
                LineNumber++;
            }

        }










    }//end of class
}
