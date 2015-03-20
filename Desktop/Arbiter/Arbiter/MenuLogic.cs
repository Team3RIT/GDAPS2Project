using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //added
using Microsoft.Xna.Framework.Content; //added
using Microsoft.Xna.Framework.Graphics; //added
using Microsoft.Xna.Framework.Input; //added
using Microsoft.Xna.Framework.Storage; //added
using Microsoft.Xna.Framework.GamerServices; //added
using System.Collections; //added this

namespace Arbiter
{

    //Nick Serrao

    //Development Coding Color
    //1.0 CornFlowerblue
    //1.1 MediumSpringGreen
    //2.0 Cadetblue
    //3.0 Lime - boxes change to MediumBlue
    class MenuLogic
        
    {
        //contains the code for all of the logic behind the menu interfaces

        //////////////////////////////// VARIABLES ///////////////////////////////////////

         //new mouseState object - poppy from Poppy!!!!







        ///////////////////////////////---- HELPER METHODS ----/////////////////////////////////////////////////////


        /// <summary>
        /// method which checks if mouse is within the given rectangle
        /// </summary>
        /// <param name="roy"></param>
        /// <returns>true if within rectangles, false if not</returns>
        public bool ThinkInsideTheBox(Rectangle roy)
        {
            
            //if mouse is within the rectangle, then return true
            if (Game1.poppy.X >= roy.X && Game1.poppy.X <= (roy.X + roy.Width) && Game1.poppy.Y  >= roy.Y && Game1.poppy.Y <= (roy.Y + roy.Height))
            {
                
                return true;
            }
            return false; //if not within box return false
        }


        /// <summary>
        /// method which checks if mouse is outside given rectangle box
        /// </summary>
        /// <param name="roy"></param>
        /// <returns>false if inside rectangle, true if not</returns>
        public bool ThinkOutsideTheBox(Rectangle roy)
        {
            //if mouse is not within the given box, then return true
            if (Game1.poppy.X < roy.X || Game1.poppy.X > (roy.X + roy.Width) || Game1.poppy.Y < roy.Y || Game1.poppy.Y > (roy.Y + roy.Height))
            {
                return true;
            }
            return false; //if within box return false
        }
        /////////////////////////////// ---- CREATE MENU BOXES HELPER METHODS ---- //////////////////////////////////////

        /// <summary>
        /// takes text, seperates it by its ',' into individual lines and returns them as an array
        /// </summary>
        /// <param name="text"></param>
        /// <param name="Length"></param>
        /// <param name="font"></param>
        /// <returns>an array of strings object</returns>
        public object[] WrapText(string text, float Length, SpriteFont font) //please note, this also used to private
        { //PLEASE NOTE THIS IS FROM THE ORIGINAL CODE MARGARET GAVE ME AND IS NOT CURRENTLY NECESSARY OUTSIDE OF THE TESTDISPLAYPOPUPBOX IN MENUDISPLAY
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




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////// ---- MENU LOGIC METHODS ---- ////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       
        public void MainMenuLogic(Game1 checkers,SpriteFont font)
        {
            //create the main menu
            //define all of the necessary rectangles - variables inherited from MenuVariables
            MenuVariables.MainMenuBox = CreateMainBox(checkers, 800, 680);
            MenuVariables.MainTitle = CreateTitleBox("Arbiter", 500, 50, font, MenuVariables.MainMenuBox);
            MenuVariables.MainNewGame = CreateTitleBox("New Game", 500, 100, font, MenuVariables.MainMenuBox);
            MenuVariables.MainLoadGame = CreateTitleBox("Load Game", 500, 150, font, MenuVariables.MainMenuBox);
            MenuVariables.MainOptions = CreateTitleBox("Options", 500, 200, font, MenuVariables.MainMenuBox);
            MenuVariables.MainExit = CreateTitleBox("Exit", 500, 250, font, MenuVariables.MainMenuBox);  

            

            //handles all the event based logic for the main menu
            if (ThinkInsideTheBox(MenuVariables.MainNewGame) == true)
            {
                MenuVariables.MainNewGameColor = MenuVariables.HoverColor;
                if(Game1.poppy.LeftButton == ButtonState.Pressed)
                {
                    //pull up new game menu, get rid of main menu
                    MenuVariables.newGame = true;
                    MenuVariables.main = false;
                }
                
            }
            if (ThinkOutsideTheBox(MenuVariables.MainNewGame) == true)
            {
                MenuVariables.MainNewGameColor = MenuVariables.BoxColor;
            }

            //load game
            if (ThinkInsideTheBox(MenuVariables.MainLoadGame) == true)
            {
                MenuVariables.MainLoadGameColor = MenuVariables.HoverColor;
                if (Game1.poppy.LeftButton == ButtonState.Pressed)
                {
                    //pull up new game menu, get rid of main menu
                    MenuVariables.loadGame = true;
                    MenuVariables.main = false;
                }

            }
            
            if (ThinkOutsideTheBox(MenuVariables.MainLoadGame) == true)
            {
                MenuVariables.MainLoadGameColor = MenuVariables.BoxColor;
            }

            //options
            if (ThinkInsideTheBox(MenuVariables.MainOptions) == true)
            {
                MenuVariables.MainOptionsColor = MenuVariables.HoverColor;
                if (Game1.poppy.LeftButton == ButtonState.Pressed)
                {
                    //pull up options menu, get rid of main menu
                    MenuVariables.options = true;
                    MenuVariables.main = false;
                }
            }
            
            if (ThinkOutsideTheBox(MenuVariables.MainOptions) == true)
            {
                MenuVariables.MainOptionsColor = MenuVariables.BoxColor;
            }
            ///exit
            if (ThinkInsideTheBox(MenuVariables.MainExit) == true)
            {
                MenuVariables.MainExitColor = MenuVariables.HoverColor;
                if (Game1.poppy.LeftButton == ButtonState.Pressed)
                {
                    //pull up new game menu, get rid of main menu
                    
                    ////////////////END THE GAME SOMEHOW!!!!!!!!!! ///////////////////////////////
                    MenuVariables.main = false;
                    Environment.Exit(0);
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.MainExit) == true)
            {
                MenuVariables.MainExitColor = MenuVariables.BoxColor;
            }
            
        
            
        }////////////////////////// end of main menu logic //////////////////////////


        //      ---- OPTIONS MENU ---
        public void OptionsMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the options menu
            MenuVariables.OptionsMenuBox = CreateMainBox(checkers, 800, 680);
            MenuVariables.OptionsTitle = CreateTitleBox("Options Menu", 600, 50, font, MenuVariables.OptionsMenuBox);  //give options a title

            MenuVariables.OptionsMainMenuReturn = CreateTitleBox("Return to Main Menu", 500, 100, font, MenuVariables.OptionsMenuBox);  //return to main menu


            //handles all the event based logic for the options menu
            if (ThinkInsideTheBox(MenuVariables.OptionsMainMenuReturn) == true)
            {
                MenuVariables.OptionsMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.poppy.LeftButton == ButtonState.Pressed)
                {
                    //pull up new game menu, get rid of main menu
                    MenuVariables.main = true;
                    MenuVariables.options = false;
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.OptionsMainMenuReturn) == true)
            {
                MenuVariables.OptionsMainMenuReturnColor = MenuVariables.BoxColor;
            }


        }
        public void LoadGameMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the load game menu

            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.LoadMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.LoadTitle = CreateTitleBox("Load Game Menu", 600, 50, font, MenuVariables.LoadMenuBox);  //give LoadGame a title
            MenuVariables.LoadMainMenuReturn = CreateTitleBox("Return to Main Menu", 500, 100, font, MenuVariables.LoadMenuBox);  //Return to main menu
            MenuVariables.LoadTextBox = CreateTitleBox("this functionality currently doesn't exist!", 500, 150, font, MenuVariables.LoadMenuBox);  //give LoadGame a textbox


            //handles all the event based logic for the load game menu
            if (ThinkInsideTheBox(MenuVariables.LoadMainMenuReturn) == true)
            {
                MenuVariables.LoadMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.poppy.LeftButton == ButtonState.Pressed)
                {
                    //pull up main menu, get rid of loadGame menu
                    MenuVariables.main = true;
                    MenuVariables.loadGame = false;
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.OptionsMainMenuReturn) == true)
            {
                MenuVariables.LoadMainMenuReturnColor = MenuVariables.BoxColor;
            }
        }

        public void NewGameMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the load game menu

            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.NewGameMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.NewGameTitle = CreateTitleBox("New Game Menu", 600, 50, font, MenuVariables.NewGameMenuBox);  //give LoadGame a title
            MenuVariables.NewGameMainMenuReturn = CreateTitleBox("Return to Main Menu", 500, 100, font, MenuVariables.NewGameMenuBox);  //Return to main menu
            


            //handles all the event based logic for the load game menu
            if (ThinkInsideTheBox(MenuVariables.NewGameMainMenuReturn) == true)
            {
                MenuVariables.NewGameMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.poppy.LeftButton == ButtonState.Pressed)
                {
                    //pull up main menu, get rid of loadGame menu
                    MenuVariables.main = true;
                    MenuVariables.newGame = false;
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.NewGameMainMenuReturn) == true)
            {
                MenuVariables.NewGameMainMenuReturnColor = MenuVariables.BoxColor;
            }
        }

       /////////////////////////////////// ---PROPERTIES--- /////////////////////////////////////////////////////////////////
 
  

    }
}
