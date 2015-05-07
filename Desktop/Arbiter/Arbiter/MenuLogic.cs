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
    //4.0 Teal
    //5.0 black/dark red
    class MenuLogic
        
    {
        //contains the code for all of the logic behind the menu interfaces
        ////////////variables////////////////

        KeyboardState keyboard;
        string previouslyPressed;
        KeyboardState previousKeyboard;
        
        ///////////////////////////////---- HELPER METHODS ----/////////////////////////////////////////////////////
        #region HELPER METHODS

        /// <summary>
        /// method which checks if mouse is within the given rectangle
        /// </summary>
        /// <param name="roy"></param>
        /// <returns>true if within rectangles, false if not</returns>
        public bool ThinkInsideTheBox(Rectangle roy)
        {
            
            //if mouse is within the rectangle, then return true
            if (Game1.click.X >= roy.X && Game1.click.X <= (roy.X + roy.Width) && Game1.click.Y  >= roy.Y && Game1.click.Y <= (roy.Y + roy.Height))
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
            if (Game1.click.X < roy.X || Game1.click.X > (roy.X + roy.Width) || Game1.click.Y < roy.Y || Game1.click.Y > (roy.Y + roy.Height))
            {
                return true;
            }
            return false; //if within box return false
        }

#endregion
        /////////////////////////////// ---- CREATE MENU BOXES HELPER METHODS ---- //////////////////////////////////////
        #region CREATE MENU BOXES HELPER METHODS
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

        #endregion


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////// ---- MENU LOGIC METHODS ---- ////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region MainMenuLogic
        public void MainMenuLogic(Game1 checkers,SpriteFont font)
        {
            //create the main menu
            //define all of the necessary rectangles - variables inherited from MenuVariables
            MenuVariables.MainMenuBox = CreateMainBox(checkers, 800, 680);
            MenuVariables.MainTitle = CreateTitleBox("Arbiter", 500, 200, font, MenuVariables.MainMenuBox);
            MenuVariables.MainNewGame = CreateTitleBox("New Game", 500, 250, font, MenuVariables.MainMenuBox);
            MenuVariables.MainLoadGame = CreateTitleBox("Load Game", 500, 300, font, MenuVariables.MainMenuBox);
            MenuVariables.MainOptions = CreateTitleBox("Options", 500, 350, font, MenuVariables.MainMenuBox);
            MenuVariables.MainExit = CreateTitleBox("Exit", 500, 400, font, MenuVariables.MainMenuBox);  

            

            //handles all the event based logic for the main menu
            if (ThinkInsideTheBox(MenuVariables.MainNewGame) == true)
            {
                MenuVariables.MainNewGameColor = MenuVariables.HoverColor;
                if(Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //if (MenuVariables.ControllerConnected == true) //only works if there is a controller connected
                    {
                        //pull up new game, get rid of main menu
                        MenuVariables.MenuStates = MenuVariables.MENUS.MAPMENU;
                    }
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
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up new game menu, get rid of main menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.LOADGAME;
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
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up options menu, get rid of main menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.OPTIONS;
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
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up new game menu, get rid of main menu
                    
                    ////////////////END THE GAME!!!!!!!!!! ///////////////////////////////
                    Environment.Exit(0);
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.MainExit) == true)
            {
                MenuVariables.MainExitColor = MenuVariables.BoxColor;
            }


        #endregion
        }////////////////////////// end of main menu logic //////////////////////////

        #region OptionsMenuLogic
        //      ---- OPTIONS MENU ---
        public void OptionsMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the options menu
            MenuVariables.OptionsMenuBox = CreateMainBox(checkers, 800, 680);
            MenuVariables.OptionsTitle = CreateTitleBox("Options Menu", 600, 200, font, MenuVariables.OptionsMenuBox);  //give options a title
            MenuVariables.OptionsInput = CreateTitleBox("Select Preferred Input", 500, 250, font, MenuVariables.OptionsMenuBox);  //return to main menu
            MenuVariables.OptionsMainMenuReturn = CreateTitleBox("Return to Main Menu", 600, 450, font, MenuVariables.OptionsMenuBox);  //return to main menu


            //handles all the event based logic for the options menu
            if (ThinkInsideTheBox(MenuVariables.OptionsMainMenuReturn) == true)
            {
                MenuVariables.OptionsMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up main menu, get rid of options menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.MAIN;
                    
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.OptionsMainMenuReturn) == true)
            {
                MenuVariables.OptionsMainMenuReturnColor = MenuVariables.BoxColor;
            }

            //input
            if (ThinkInsideTheBox(MenuVariables.OptionsInput) == true)
            {
                MenuVariables.OptionsInputColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up input menu, get rid of options
                    MenuVariables.MenuStates = MenuVariables.MENUS.INPUT;
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.OptionsInput) == true)
            {
                MenuVariables.OptionsInputColor = MenuVariables.BoxColor;
            }

        }
        #endregion

        public void InputMenuLogic(Game1 checkers, SpriteFont font)
        {

            //create all of the boxes
            MenuVariables.InputMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.InputTitle = CreateTitleBox("Select Your Input Method", 600, 200, font, MenuVariables.InputMenuBox);
            MenuVariables.InputMouse = CreateTitleBox("Mouse", 500, 250, font, MenuVariables.InputMenuBox);
            MenuVariables.InputGamePad = CreateTitleBox("GamePad", 500, 300, font, MenuVariables.InputMenuBox);
            MenuVariables.InputKeyBoard = CreateTitleBox("KeyBoard", 500, 350, font, MenuVariables.InputMenuBox);
            MenuVariables.InputOptionsReturn = CreateTitleBox("Return to OptionsMenu", 600, 500, font, MenuVariables.InputMenuBox);
            MenuVariables.InputMessage = CreateTitleBox("This Will Change Depending on the Input", 500, 400, font, MenuVariables.InputMenuBox); //changes in MenuDisplay

            if (ThinkInsideTheBox(MenuVariables.InputMouse) == true)
            {MenuVariables.InputMouseColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                   Game1.preferredInput = 0; //change input to the mouse
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.InputMouse) == true)
            { MenuVariables.InputMouseColor= MenuVariables.BoxColor;}

            ///////
            if (ThinkInsideTheBox(MenuVariables.InputGamePad) == true)
            {MenuVariables.InputGamePadColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                   Game1.preferredInput = 1; //change input to the gamepad
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.InputGamePad) == true)
            { MenuVariables.InputGamePadColor = MenuVariables.BoxColor;}

            ///////
            if (ThinkInsideTheBox(MenuVariables.InputKeyBoard) == true)
            {MenuVariables.InputKeyBoardColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                   Game1.preferredInput = 2; //change input to the keyboard
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.InputKeyBoard) == true)
            { MenuVariables.InputKeyBoardColor = MenuVariables.BoxColor;}

            /////return to options
            if (ThinkInsideTheBox(MenuVariables.InputOptionsReturn) == true)
            {
                MenuVariables.InputOptionsReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //leave input menu and go to the options menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.OPTIONS;
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.InputOptionsReturn) == true)
            { MenuVariables.InputOptionsReturnColor = MenuVariables.BoxColor; }

        }



        #region LoadGameMenuLogic
        public void LoadGameMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the load game menu
            

            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.LoadMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.LoadTitle = CreateTitleBox("Load Game Menu", 500, 150, font, MenuVariables.LoadMenuBox);  //give LoadGame a title
            MenuVariables.LoadTextTitle = CreateTitleBox("Click the Box Below and Type the Name of the File to Open:", 600, 200, font, MenuVariables.LoadMenuBox);
            MenuVariables.LoadTextBox = CreateTitleBox("random extraneous text", 500, 250, font, MenuVariables.LoadMenuBox);  //give LoadGame a textbox
            MenuVariables.LoadMainMenuReturn = CreateTitleBox("Return to Main Menu", 500, 500, font, MenuVariables.LoadMenuBox);  //Return to main menu
            MenuVariables.LoadSubmit = CreateTitleBox("Submit", 200, 350, font, MenuVariables.LoadMenuBox);  //give LoadGame a textbox
            MenuVariables.LoadClear = CreateTitleBox("Clear", 200, 400, font, MenuVariables.LoadMenuBox);  //Return to main menu


            /////////////////////type in your file name
             //Keys[] pressed;
             
             keyboard = Keyboard.GetState();
             
            if (ThinkInsideTheBox(MenuVariables.LoadTextBox) == true)
            {
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    MenuVariables.CanType = true;
                }
            }
            if (MenuVariables.CanType == true)
            {
                
                MenuVariables.LoadTextBoxColor = MenuVariables.TypeColor; //change color once its clicked on
                MenuVariables.GetKey(keyboard, ref previousKeyboard);
                
            }

            //stop adding text and reset the filename
            if (ThinkInsideTheBox(MenuVariables.LoadClear) == true && MenuVariables.CanType == true)
            {
                MenuVariables.LoadClearColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    MenuVariables.filename = "";
                    MenuVariables.CanType = false;
                    MenuVariables.LoadTextBoxColor = MenuVariables.BoxColor; //change color back
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadClear) == true)
            { MenuVariables.LoadClearColor = MenuVariables.BoxColor; }
            /////submit file name

            if (ThinkInsideTheBox(MenuVariables.LoadSubmit) == true && MenuVariables.CanType == true)
            {
                MenuVariables.LoadSubmitColor = MenuVariables.TypeColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //LOAD THE GAME HERE!!!!!!!!!!!!!
                    FileIO.LoadGame(MenuVariables.filename);
                    MenuVariables.filename = "";
                    MenuVariables.MenuStates = MenuVariables.MENUS.NEWGAME;

                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadSubmit) == true)
            { MenuVariables.LoadSubmitColor = MenuVariables.BoxColor; }
            /////


  
            
            if (ThinkInsideTheBox(MenuVariables.LoadMainMenuReturn) == true)
            {   MenuVariables.LoadMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up main menu, get rid of loadGame menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.MAIN;
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadMainMenuReturn) == true)
            {MenuVariables.LoadMainMenuReturnColor = MenuVariables.BoxColor;}
        }
        #endregion

        #region LoadMapMenuLogic
        public void LoadMapMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the load game menu


            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.LoadMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.LoadTitle = CreateTitleBox("Map Menu", 500, 150, font, MenuVariables.LoadMenuBox);  //give LoadGame a title
            MenuVariables.LoadTextTitle = CreateTitleBox("Click the Box Below and Type the Name of the File to Open:", 600, 200, font, MenuVariables.LoadMenuBox);
            MenuVariables.LoadTextBox = CreateTitleBox("random extraneous text", 500, 300, font, MenuVariables.LoadMenuBox);  //give LoadGame a textbox
            MenuVariables.LoadMainMenuReturn = CreateTitleBox("Return to Main Menu", 500, 500, font, MenuVariables.LoadMenuBox);  //Return to main menu
            MenuVariables.LoadSubmit = CreateTitleBox("Submit", 200, 350, font, MenuVariables.LoadMenuBox);  //give LoadGame a textbox
            MenuVariables.LoadClear = CreateTitleBox("Clear", 200, 400, font, MenuVariables.LoadMenuBox);  //Return to main menu


            /////////////////////type in your file name

            keyboard = Keyboard.GetState();

            if (ThinkInsideTheBox(MenuVariables.LoadTextBox) == true)
            {
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    MenuVariables.CanType = true;
                }
            }
            if (MenuVariables.CanType == true)
            {

                MenuVariables.LoadTextBoxColor = MenuVariables.TypeColor; //change color once its clicked on
                MenuVariables.GetKey(keyboard, ref previousKeyboard);

            }

            //stop adding text and reset the filename
            if (ThinkInsideTheBox(MenuVariables.LoadClear) == true && MenuVariables.CanType == true)
            {
                MenuVariables.LoadClearColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    MenuVariables.filename = "";
                    MenuVariables.CanType = false;
                    MenuVariables.LoadTextBoxColor = MenuVariables.BoxColor; //change color back
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadClear) == true)
            { MenuVariables.LoadClearColor = MenuVariables.BoxColor; }
            /////submit file name

            if (ThinkInsideTheBox(MenuVariables.LoadSubmit) == true && MenuVariables.CanType == true)
            {
                MenuVariables.LoadSubmitColor = MenuVariables.TypeColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //LOAD THE GAME HERE!!!!!!!!!!!!!
                    FileIO.ReadMapFile(MenuVariables.filename);
                    MenuVariables.filename = "";
                    MenuVariables.MenuStates = MenuVariables.MENUS.NEWGAME;
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadSubmit) == true)
            { MenuVariables.LoadSubmitColor = MenuVariables.BoxColor; }
            /////




            if (ThinkInsideTheBox(MenuVariables.LoadMainMenuReturn) == true)
            {
                MenuVariables.LoadMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up main menu, get rid of loadGame menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.MAIN;
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadMainMenuReturn) == true)
            { MenuVariables.LoadMainMenuReturnColor = MenuVariables.BoxColor; }
        }
#endregion

        #region Save Game Menu
        public void SaveGameMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the load game menu


            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.LoadMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.LoadTitle = CreateTitleBox("Map Menu", 600, 150, font, MenuVariables.LoadMenuBox);  //give LoadGame a title
            MenuVariables.LoadTextTitle = CreateTitleBox("Click the Box Below and Type the Name of the File to Open:", 600, 200, font, MenuVariables.LoadMenuBox);
            MenuVariables.LoadTextBox = CreateTitleBox("random extraneous text", 500, 250, font, MenuVariables.LoadMenuBox);  //give LoadGame a textbox
            MenuVariables.LoadMainMenuReturn = CreateTitleBox("Return to Main Menu", 500, 500, font, MenuVariables.LoadMenuBox);  //Return to main menu
            MenuVariables.LoadSubmit = CreateTitleBox("Submit", 200, 350, font, MenuVariables.LoadMenuBox);  //give LoadGame a textbox
            MenuVariables.LoadClear = CreateTitleBox("Clear", 200, 400, font, MenuVariables.LoadMenuBox);  //Return to main menu


            /////////////////////type in your file name

            keyboard = Keyboard.GetState();

            if (ThinkInsideTheBox(MenuVariables.LoadTextBox) == true)
            {
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    MenuVariables.CanType = true;
                }
            }
            if (MenuVariables.CanType == true)
            {

                MenuVariables.LoadTextBoxColor = MenuVariables.TypeColor; //change color once its clicked on
                MenuVariables.GetKey(keyboard, ref previousKeyboard);

            }

            //stop adding text and reset the filename
            if (ThinkInsideTheBox(MenuVariables.LoadClear) == true && MenuVariables.CanType == true)
            {
                MenuVariables.LoadClearColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    MenuVariables.filename = "";
                    MenuVariables.CanType = false;
                    MenuVariables.LoadTextBoxColor = MenuVariables.BoxColor; //change color back
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadClear) == true)
            { MenuVariables.LoadClearColor = MenuVariables.BoxColor; }
            /////submit file name

            if (ThinkInsideTheBox(MenuVariables.LoadSubmit) == true && MenuVariables.CanType == true)
            {
                MenuVariables.LoadSubmitColor = MenuVariables.TypeColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //SAVE THE GAME HERE!!!!!!!!!!!!!
                    FileIO.SaveGame(MenuVariables.filename);
                    MenuVariables.filename = "";
                    MenuVariables.MenuStates = MenuVariables.MENUS.PAUSE;
                    
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadSubmit) == true)
            { MenuVariables.LoadSubmitColor = MenuVariables.BoxColor; }
            /////




            if (ThinkInsideTheBox(MenuVariables.LoadMainMenuReturn) == true)
            {
                MenuVariables.LoadMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up main menu, get rid of loadGame menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.MAIN;
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.LoadMainMenuReturn) == true)
            { MenuVariables.LoadMainMenuReturnColor = MenuVariables.BoxColor; }
        }

        #endregion
        /// /////////////// NEW GAME MENU CURRENTLY NOT IN SERVICE - STILL FUNCTIONAL IF NEEDS TO BE CALLED ////////////////////////////////////
        
        #region NEW GAME MENU
        public void NewGameMenuLogic(Game1 checkers, SpriteFont font)
        {
            //////////////////////////////// THIS MENU IS CURRENTLY NOT IN SERVICE ////////////////////////////////////
            //create the new game menu

            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.NewGameMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.NewGameTitle = CreateTitleBox("New Game Menu", 600, 50, font, MenuVariables.NewGameMenuBox);  //give NewGame a title
            MenuVariables.NewGameTwoPlayer = CreateTitleBox("Start Two Player Game", 600, 100, font, MenuVariables.NewGameMenuBox);  //start a two player game!
            MenuVariables.NewGameControllerConnected = CreateTitleBox("I don't quite remeber why I needed text for this", 600, 250, font, MenuVariables.NewGameMenuBox);  //start a two player game!
            MenuVariables.NewGameMainMenuReturn = CreateTitleBox("Return to Main Menu", 500, 400, font, MenuVariables.NewGameMenuBox);  //Return to main menu
            


            //handles all the event based logic for the new game menu
            if (ThinkInsideTheBox(MenuVariables.NewGameMainMenuReturn) == true)
            {
                MenuVariables.NewGameMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //pull up main menu, get rid of NewGame menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.MAIN;
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.NewGameMainMenuReturn) == true)
            {
                MenuVariables.NewGameMainMenuReturnColor = MenuVariables.BoxColor;
            }


            ///lets start a two player game!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! - code below 
            if (ThinkInsideTheBox(MenuVariables.NewGameTwoPlayer) == true)
            {
                MenuVariables.NewGameTwoPlayerColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //won't start game if there is no controller connected
                    if (MenuVariables.ControllerConnected == true)
                    {
                        //code to start a two player game goes here


                    }
                }

            }
            if (ThinkOutsideTheBox(MenuVariables.NewGameTwoPlayer) == true)
            {
                MenuVariables.NewGameTwoPlayerColor = MenuVariables.BoxColor;
            }

        }
        #endregion

        /// ///////////////////////////////////////////////////////////////////////////////////////
        #region PauseMenu
        public void PauseMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the new game menu

            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.PauseMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.PauseTitle = CreateTitleBox("GAME PAUSED", 600, 200, font, MenuVariables.PauseMenuBox);  //pause menu title
            MenuVariables.PauseSaveGame = CreateTitleBox("Save Game", 600, 300, font, MenuVariables.PauseMenuBox);  //save your game
            
            MenuVariables.PauseReturnToGame = CreateTitleBox("Return to Game", 600, 350, font, MenuVariables.PauseMenuBox);  //Return to the active game
            MenuVariables.PauseMainMenuReturn = CreateTitleBox("Main Menu", 600, 500, font, MenuVariables.PauseMenuBox);  //return to the main menu
            //handles all the event based logic for the pause menu
            if (ThinkInsideTheBox(MenuVariables.PauseSaveGame) == true)
            {
                MenuVariables.PauseSaveGameColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    MenuVariables.MenuStates = MenuVariables.MENUS.SAVEGAME;


                }
            }
            if (ThinkOutsideTheBox(MenuVariables.PauseSaveGame) == true)
            {
                MenuVariables.PauseSaveGameColor = MenuVariables.BoxColor;
            }
           //return to main menu code
            if (ThinkInsideTheBox(MenuVariables.PauseMainMenuReturn) == true)
            {
                MenuVariables.PauseMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //go to main menu, perhaps we want a message asking if they were sure?

                    MenuVariables.MenuStates = MenuVariables.MENUS.MAIN;

                }
            }
            if (ThinkOutsideTheBox(MenuVariables.PauseMainMenuReturn) == true)
            {
                MenuVariables.PauseMainMenuReturnColor = MenuVariables.BoxColor;
            }

            //return to the game code
            if (ThinkInsideTheBox(MenuVariables.PauseReturnToGame) == true)
            {
                MenuVariables.PauseReturnToGameColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    
                    Game1.gameState = Game1.States.PLAYERTURN;
                    //return to the game somehow

                }
            }
            if (ThinkOutsideTheBox(MenuVariables.PauseReturnToGame) == true)
            {
                MenuVariables.PauseReturnToGameColor = MenuVariables.BoxColor;
            }

        }

        #endregion

        #region WinMenuLogic
        public void WinMenuLogic(Game1 checkers, SpriteFont font)
        {
            //create the new game menu

            //define all of the necessary rectangles - variables inherited from MenuVariables
            //define rectangles boxes thingys
            MenuVariables.WinMenuBox = CreateMainBox(checkers, 800, 680); //create the box to hold the entire menu
            MenuVariables.WinTitle = CreateTitleBox("Congratulations! Someone has won!", 600, 200, font, MenuVariables.WinMenuBox);  //win menu title
            MenuVariables.WinExit = CreateTitleBox("Exit Game", 600, 350, font, MenuVariables.WinMenuBox);  //Return to the active game
            MenuVariables.WinMainMenuReturn = CreateTitleBox("Return to Main Menu", 600, 500, font, MenuVariables.WinMenuBox);  //return to the main menu
            //handles all the event based logic for the pause menu
            if (ThinkInsideTheBox(MenuVariables.WinMainMenuReturn) == true)
            {
                MenuVariables.WinMainMenuReturnColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //go to main menu
                    MenuVariables.MenuStates = MenuVariables.MENUS.MAIN;
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.WinMainMenuReturn) == true)
            {
                MenuVariables.WinMainMenuReturnColor = MenuVariables.BoxColor;
            }
            ///exit the game form the victory screen
            if (ThinkInsideTheBox(MenuVariables.WinExit) == true)
            {
                MenuVariables.WinExitColor = MenuVariables.HoverColor;
                if (Game1.click.LeftButton == ButtonState.Pressed)
                {
                    //leave the game
                    Environment.Exit(0);
                }
            }
            if (ThinkOutsideTheBox(MenuVariables.WinExit) == true)
            {
                MenuVariables.WinExitColor = MenuVariables.BoxColor;
            }

        }

        #endregion



        /////////////////////////////////// ---PROPERTIES--- /////////////////////////////////////////////////////////////////
 
  

    }
}
