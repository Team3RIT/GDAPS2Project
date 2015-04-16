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
namespace Arbiter
{
    //Nick Serrao
   
    //holds all of the methods for displaying popup boxes and menus - used to be known as MenuBoxes
    //responsible for the methods thrown into the Draw method in the Game1 class
    //whats drawn can be changed by the methods from MenuLogic, by influencing MenuVariables
    //Development Coding Color
    //1.0 CornFlowerblue
    //1.1 MediumSpringGreen
    //2.0 Cadetblue
    //3.0 lime
    //4.0 teal
    public class MenuDisplay
    {
 


        //creates the main menu
        public void DisplayMainMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {
            //create the regular text standards
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1); //the two ints at the end change the boxes fading, both left and up
            FillText.SetData(new Color[] { Color.White });
            
            //draw all of the rectangles
            
            //double trouble = MainMenuBox.Width/2 + font.MeasureString("This is Our Game's Title!").X/2; // - the code to find the center point for the text of a box
            
            //draw MainBox
            batch.Draw(FillText, MenuVariables.MainMenuBox, MenuVariables.BackgroundColor); //cornflowerblue, mediumspringgreen, cadetblue
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.MainTitle, MenuVariables.MainTitleColor); // draws the box
            batch.DrawString(font, "This is Our Game's Title!", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("This is Our Game's Title!").X / 2, MenuVariables.MainTitle.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.MainNewGame, MenuVariables.MainNewGameColor); // draws the box
            batch.DrawString(font, "NewGame", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("NewGame").X / 2, MenuVariables.MainNewGame.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.MainLoadGame, MenuVariables.MainLoadGameColor); // draws the box
            batch.DrawString(font, "LoadGame", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("LoadGame").X / 2, MenuVariables.MainLoadGame.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.MainOptions, MenuVariables.MainOptionsColor); // draws the box
            batch.DrawString(font, "Options", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("Options").X / 2, MenuVariables.MainOptions.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBoLength
            batch.Draw(FillText, MenuVariables.MainExit, MenuVariables.MainExitColor); // draws the box
            batch.DrawString(font, "Exit", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("Exit").X / 2, MenuVariables.MainExit.Y), Color.Crimson); //draws the text within the box

        }


        public void DisplayNewGameMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {

            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //define rectangles boxes thingys


            //draw MainBox
            batch.Draw(FillText, MenuVariables.MainMenuBox, MenuVariables.BackgroundColor); 

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.NewGameTitle, MenuVariables.NewGameTitleColor); // draws the box
            batch.DrawString(font, "New Game", new Vector2(MenuVariables.NewGameMenuBox.Width / 2 - font.MeasureString("New Game").X / 2, MenuVariables.NewGameTitle.Y), Color.Crimson); //draws the text within the box

            batch.Draw(FillText, MenuVariables.NewGameTwoPlayer, MenuVariables.NewGameTwoPlayerColor); // draws the box
            batch.DrawString(font, "Start a Two Player Game", new Vector2(MenuVariables.NewGameMenuBox.Width / 2 - font.MeasureString("Start a Two Player Game").X / 2, MenuVariables.NewGameTwoPlayer.Y), Color.Crimson); //draws the text within the box

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.NewGameMainMenuReturn, MenuVariables.NewGameMainMenuReturnColor); // draws the box
            batch.DrawString(font, "Return to Main Menu", new Vector2(MenuVariables.NewGameMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.NewGameMainMenuReturn.Y), Color.Crimson); //draws the text within the box

            if (MenuVariables.ControllerConnected == false)
            {
                //show text if no gamepad is connected
                batch.Draw(FillText, MenuVariables.NewGameControllerConnected, Color.Black); // draws the box around the no gamepad text
                batch.DrawString(font, "YOU FOOL(S)! CONNECT A GAMEPAD TO PLAY THE GAME", new Vector2(MenuVariables.NewGameMenuBox.Width / 2 - font.MeasureString("YOU FOOL(S)! CONNECT A GAMEPAD TO PLAY THE GAME").X / 2, 250), Color.Lime);

                
            }

        }

        public void DisplayOptionsMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {

            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });
            
            //define rectangles boxes thingys
            

            //draw MainBox
            batch.Draw(FillText, MenuVariables.OptionsMenuBox, MenuVariables.BackgroundColor); 

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.OptionsTitle, MenuVariables.OptionsTitleColor); // draws the box
            batch.DrawString(font, "Options", new Vector2(MenuVariables.OptionsMenuBox.Width / 2 - font.MeasureString("Options").X / 2, MenuVariables.OptionsTitle.Y), Color.Crimson); //draws the text within the box

            batch.Draw(FillText, MenuVariables.OptionsInput, MenuVariables.OptionsInputColor); // draws the box
            batch.DrawString(font, "Select Preferred Input", new Vector2(MenuVariables.OptionsMenuBox.Width / 2 - font.MeasureString("Select Preferred Input").X / 2, MenuVariables.OptionsInput.Y), Color.Crimson); //draws the text within the box
            
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.OptionsMainMenuReturn, MenuVariables.OptionsMainMenuReturnColor); // draws the box
            batch.DrawString(font, "Return to Main Menu", new Vector2(MenuVariables.OptionsMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.OptionsMainMenuReturn.Y), Color.Crimson); //draws the text within the box


        }

        public void DisplayLoadGameMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {
            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //draw MainBox
            batch.Draw(FillText, MenuVariables.LoadMenuBox, MenuVariables.BackgroundColor); 
            
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadTitle, MenuVariables.LoadTitleColor); // draws the box
            batch.DrawString(font, "Load Game Menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Load Game Menu").X / 2, MenuVariables.LoadTitle.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadMainMenuReturn, MenuVariables.LoadMainMenuReturnColor); // draws the box
            batch.DrawString(font, "Return to Main menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.LoadMainMenuReturn.Y), Color.Crimson); //draws the text within the box

            //draw the textbox
            batch.Draw(FillText, MenuVariables.LoadTextTitle, MenuVariables.LoadTextTitleColor); // draws the box
            batch.DrawString(font, "Click the Box Below and Type the Name of the File to Open:", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Click the Box Below and Type the Name of the File to Open:").X / 2, MenuVariables.LoadTextTitle.Y), Color.Crimson); //draws the text within the textbox

            batch.Draw(FillText, MenuVariables.LoadTextBox, MenuVariables.LoadTextBoxColor); // draws the box
            batch.DrawString(font, MenuVariables.filename, new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString(MenuVariables.filename).X / 2, MenuVariables.LoadTextBox.Y), Color.Crimson);
            ///////
            batch.Draw(FillText, MenuVariables.LoadClear, MenuVariables.LoadClearColor); // draws the box
            batch.DrawString(font, "Clear", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Clear").X/2, MenuVariables.LoadClear.Y), Color.Crimson); //draws the text within the textbox

            batch.Draw(FillText, MenuVariables.LoadSubmit, MenuVariables.LoadSubmitColor); // draws the box
            batch.DrawString(font, "Submit", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Submit").X/2, MenuVariables.LoadSubmit.Y), Color.Crimson);
        }

        public void DisplayMapMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {
            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //draw MainBox
            batch.Draw(FillText, MenuVariables.LoadMenuBox, MenuVariables.BackgroundColor);

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadTitle, MenuVariables.LoadTitleColor); // draws the box
            batch.DrawString(font, "Map Menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Load Game Menu").X / 2, MenuVariables.LoadTitle.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadMainMenuReturn, MenuVariables.LoadMainMenuReturnColor); // draws the box
            batch.DrawString(font, "Return to Main Menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.LoadMainMenuReturn.Y), Color.Crimson); //draws the text within the box

            //draw the textbox
            batch.Draw(FillText, MenuVariables.LoadTextTitle, MenuVariables.LoadTextTitleColor); // draws the box
            batch.DrawString(font, "Click the Box Below and Type the Name of the Map File to Open:", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Click the Box Below and Type the Name of the File to Open:").X / 2, MenuVariables.LoadTextTitle.Y), Color.Crimson); //draws the text within the textbox

            batch.Draw(FillText, MenuVariables.LoadTextBox, MenuVariables.LoadTextBoxColor); // draws the box
            batch.DrawString(font, MenuVariables.filename, new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString(MenuVariables.filename).X / 2, MenuVariables.LoadTextBox.Y), Color.Crimson);
            ///////
            batch.Draw(FillText, MenuVariables.LoadClear, MenuVariables.LoadClearColor); // draws the box
            batch.DrawString(font, "Clear", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Clear").X / 2, MenuVariables.LoadClear.Y), Color.Crimson); //draws the text within the textbox

            batch.Draw(FillText, MenuVariables.LoadSubmit, MenuVariables.LoadSubmitColor); // draws the box
            batch.DrawString(font, "Submit", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Submit").X / 2, MenuVariables.LoadSubmit.Y), Color.Crimson);
        }

        public void DisplaySaveMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {
            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //draw MainBox
            batch.Draw(FillText, MenuVariables.LoadMenuBox, MenuVariables.BackgroundColor);

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadTitle, MenuVariables.LoadTitleColor); // draws the box
            batch.DrawString(font, "Save Menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Load Game Menu").X / 2, MenuVariables.LoadTitle.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadMainMenuReturn, MenuVariables.LoadMainMenuReturnColor); // draws the box
            batch.DrawString(font, "Return to Main Menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.LoadMainMenuReturn.Y), Color.Crimson); //draws the text within the box

            //draw the textbox
            batch.Draw(FillText, MenuVariables.LoadTextTitle, MenuVariables.LoadTextTitleColor); // draws the box
            batch.DrawString(font, "Click the Box Below and Type the Name of the Map File to Open:", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Click the Box Below and Type the Name of the File to Open:").X / 2, MenuVariables.LoadTextTitle.Y), Color.Crimson); //draws the text within the textbox

            batch.Draw(FillText, MenuVariables.LoadTextBox, MenuVariables.LoadTextBoxColor); // draws the box
            batch.DrawString(font, MenuVariables.filename, new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString(MenuVariables.filename).X / 2, MenuVariables.LoadTextBox.Y), Color.Crimson);
            ///////
            batch.Draw(FillText, MenuVariables.LoadClear, MenuVariables.LoadClearColor); // draws the box
            batch.DrawString(font, "Clear", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Clear").X / 2, MenuVariables.LoadClear.Y), Color.Crimson); //draws the text within the textbox

            batch.Draw(FillText, MenuVariables.LoadSubmit, MenuVariables.LoadSubmitColor); // draws the box
            batch.DrawString(font, "Submit", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Submit").X / 2, MenuVariables.LoadSubmit.Y), Color.Crimson);
        }
        /////////////////in game menus ////////////////////////////////////////////////////////////

        public void DisplayPauseMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {

            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //define rectangles boxes thingys


            //draw MainBox
            batch.Draw(FillText, MenuVariables.PauseMenuBox, MenuVariables.BackgroundColor);

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.PauseTitle, MenuVariables.PauseTitleColor); // draws the box
            batch.DrawString(font, "GAME PAUSED", new Vector2(MenuVariables.PauseMenuBox.Width / 2 - font.MeasureString("GAME PAUSED").X / 2, MenuVariables.PauseTitle.Y), Color.Crimson);  //by now i hope you realize these just draw the text in the boxes
            //draw save game
            batch.Draw(FillText, MenuVariables.PauseSaveGame, MenuVariables.PauseSaveGameColor); // draws the box
            batch.DrawString(font, "Save Game", new Vector2(MenuVariables.PauseMenuBox.Width / 2 - font.MeasureString("Save Game").X / 2, MenuVariables.PauseSaveGame.Y), Color.Crimson); 

            //draw return to game box
            batch.Draw(FillText, MenuVariables.PauseReturnToGame, MenuVariables.PauseReturnToGameColor); // draws the box
            batch.DrawString(font, "Return to Game", new Vector2(MenuVariables.PauseMenuBox.Width / 2 - font.MeasureString("Return to Game").X / 2, MenuVariables.PauseReturnToGame.Y), Color.Crimson); 
            
            
            //Draw mainmenu return box
            batch.Draw(FillText, MenuVariables.PauseMainMenuReturn, MenuVariables.PauseMainMenuReturnColor); // draws the box
            batch.DrawString(font, "Return to Main Menu", new Vector2(MenuVariables.PauseMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.PauseMainMenuReturn.Y), Color.Crimson); 


        }

        public void DisplayWinMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {

            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //define rectangles boxes thingys


            //draw MainBox
            batch.Draw(FillText, MenuVariables.WinMenuBox, MenuVariables.BackgroundColor); 

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.WinTitle, MenuVariables.WinTitleColor); // draws the box for the victory screen title
            batch.DrawString(font, "Congratulations, Someone has won!", new Vector2(MenuVariables.WinMenuBox.Width / 2 - font.MeasureString("Congratualtions, Someone has won!").X / 2, MenuVariables.WinTitle.Y), Color.Crimson);  //by now i hope you realize these just draw the text in the boxes

            //draw return to game box
            batch.Draw(FillText, MenuVariables.WinMainMenuReturn, MenuVariables.WinMainMenuReturnColor); // draws the box for returning to main menu
            batch.DrawString(font, "Return to Main Menu", new Vector2(MenuVariables.WinMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.WinMainMenuReturn.Y), Color.Crimson);


            //Draw mainmenu return box
            batch.Draw(FillText, MenuVariables.WinExit, MenuVariables.WinExitColor); // draws the box for exiting the game from the win screen
            batch.DrawString(font, "Exit Game", new Vector2(MenuVariables.WinMenuBox.Width / 2 - font.MeasureString("Exit Game").X / 2, MenuVariables.WinExit.Y), Color.Crimson);


        }


        public void DisplayInputMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {//draw out the menu

            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //define rectangles boxes thingys
            //draw MainBox
            batch.Draw(FillText, MenuVariables.InputMenuBox, MenuVariables.BackgroundColor); // draws the menu box
            ///main title
            batch.Draw(FillText, MenuVariables.InputTitle, MenuVariables.InputTitleColor);
            batch.DrawString(font, "Select Your Input Mehod", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("Select Your Input Method").X / 2, MenuVariables.InputTitle.Y), Color.Crimson); //draws the text within the box
            //mouse
            batch.Draw(FillText, MenuVariables.InputMouse, MenuVariables.InputMouseColor); 
            batch.DrawString(font, "Mouse", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("Mouse").X / 2, MenuVariables.InputMouse.Y), Color.Crimson); 
            //gamepad
            batch.Draw(FillText, MenuVariables.InputGamePad, MenuVariables.InputGamePadColor); 
            batch.DrawString(font, "GamePad", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("GamePad").X / 2, MenuVariables.InputGamePad.Y), Color.Crimson); 
            //keyboard
            batch.Draw(FillText, MenuVariables.InputKeyBoard, MenuVariables.InputKeyBoardColor);
            batch.DrawString(font, "KeyBoard", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("KeyBoard").X / 2, MenuVariables.InputKeyBoard.Y), Color.Crimson); 
            //return to options menu
            batch.Draw(FillText, MenuVariables.InputOptionsReturn, MenuVariables.InputOptionsReturnColor);
            batch.DrawString(font, "Return to Options Menu", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("Return to Option Menu").X / 2, MenuVariables.InputOptionsReturn.Y), Color.Crimson); 
            
            //display a different message for the input depending on what input method is selected
           switch(Game1.preferredInput)
            {
                case 0:
                        batch.Draw(FillText, MenuVariables.InputMessage, MenuVariables.InputMessageColor);
                        batch.DrawString(font, "Your Current Input Method is the Mouse", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("Your Current Input Method is the Mouse").X / 2, MenuVariables.InputMessage.Y), Color.Gold); 
            
                        break;
                case 1:
                        batch.Draw(FillText, MenuVariables.InputMessage, MenuVariables.InputMessageColor);
                        batch.DrawString(font, "Your Current Input Method is the GamePad", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("Your Current Input Method is the GamePad").X / 2, MenuVariables.InputMessage.Y), Color.DarkOrange); 
                        break;
                case 2:
                        batch.Draw(FillText, MenuVariables.InputMessage, MenuVariables.InputMessageColor);
                        batch.DrawString(font, "Your Current Input Method is the KeyBoard", new Vector2(MenuVariables.InputMenuBox.Width / 2 - font.MeasureString("Your Current Input Method is the KeyBoard").X / 2, MenuVariables.InputMessage.Y), Color.Yellow); 
                        break;
            }


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region PopUpBox TestCase
        //displays a test version of a menubox, one which uses all the different types of boxes (including a text box which does not have an independant method)
        public void DisplayTestPopup(string Title, string text, string AssetPicturePath, SpriteBatch batch, SpriteFont font, Game1 chess)
        {
            //PLEASE NOTE THAT THIS WILL DISPLAY A POPUP BOX CORRECTLY, IT WILL HOWEVER BE MANGLED AND STRANGE AND HAVE UNNECCESSARY PARTS, AND REALLY IS A FRANKENSTEIN METHOD
            
            //define all of the rectangles
            Texture2D FillText = new Texture2D(chess.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            MenuLogic mlogic = new MenuLogic(); //allows access to the methods in mlogic
 
            //draw rectangle for main menu - default to the center of the screen
            Rectangle MainMenuBox = mlogic.CreateMainBox(chess, 700, 400);
            Rectangle TitleBox = mlogic.CreateTitleBox(Title, 650, 50, font, MainMenuBox);  //give mainmenubox a title
            Rectangle TextLine = mlogic.CreateLine(MainMenuBox, TitleBox, 1); //give MainMenuBox a line below the title and above the text
            Rectangle PictureBox = mlogic.CreatePictureBox(AssetPicturePath, MainMenuBox, TitleBox); //create a picturebox

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
            foreach (string Line in mlogic.WrapText("Text", TextBody.Width, font))
            {
                batch.DrawString(font, Line, new Vector2(TextBody.X, TextBody.Y + (LineNumber * font.MeasureString(Line).Y)), Color.Black);
                LineNumber++;
            }

        }

        #endregion

    }//end of class
}
