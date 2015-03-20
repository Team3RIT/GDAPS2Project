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
    //Development Coding Color
    //1.0 CornFlowerblue
    //1.1 MediumSpringGreen
    //2.0 Cadetblue
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
            batch.Draw(FillText, MenuVariables.MainTitle, MenuVariables.BoxColor); // draws the box
            batch.DrawString(font, "This is Our Game's Title!", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("This is Our Game's Title!").X / 2, MenuVariables.MainTitle.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.MainNewGame, MenuVariables.BoxColor); // draws the box
            batch.DrawString(font, "NewGame", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("NewGame").X / 2, MenuVariables.MainNewGame.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.MainLoadGame, MenuVariables.BoxColor); // draws the box
            batch.DrawString(font, "LoadGame", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("LoadGame").X / 2, MenuVariables.MainLoadGame.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.MainOptions, MenuVariables.BoxColor); // draws the box
            batch.DrawString(font, "Options", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("Options").X / 2, MenuVariables.MainOptions.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBoLength
            batch.Draw(FillText, MenuVariables.MainExit, MenuVariables.BoxColor); // draws the box
            batch.DrawString(font, "Exit", new Vector2(MenuVariables.MainMenuBox.Width / 2 - font.MeasureString("Exit").X / 2, MenuVariables.MainExit.Y), Color.Crimson); //draws the text within the box

        }

        public void DisplayOptionsMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {

            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });
            
            //define rectangles boxes thingys
            

            //draw MainBox
            batch.Draw(FillText, MenuVariables.MainMenuBox, Color.MediumSpringGreen); //cornflowerblue, mediumspringgreen

            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.OptionsTitle, Color.Black); // draws the box
            batch.DrawString(font, "Options", new Vector2(MenuVariables.OptionsMenuBox.Width / 2 - font.MeasureString("Options").X / 2, MenuVariables.OptionsTitle.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.OptionsMainMenuReturn, Color.Black); // draws the box
            batch.DrawString(font, "Return to Main Menu", new Vector2(MenuVariables.OptionsMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.OptionsMainMenuReturn.Y), Color.Crimson); //draws the text within the box


        }

        public void DisplayLoadGameMenu(SpriteBatch batch, SpriteFont font, Game1 checkers)
        {
            //define all of the rectangles
            Texture2D FillText = new Texture2D(checkers.GraphicsDevice, 1, 1);
            FillText.SetData(new Color[] { Color.White });

            //draw MainBox
            batch.Draw(FillText, MenuVariables.OptionsMenuBox, Color.MediumSpringGreen); //cornflowerblue, mediumspringgreen
            
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadTitle, Color.Black); // draws the box
            batch.DrawString(font, "Load Game Menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Load Game Menu").X / 2, MenuVariables.LoadTitle.Y), Color.Crimson); //draws the text within the box
            //Draw TitleBox
            batch.Draw(FillText, MenuVariables.LoadMainMenuReturn, Color.Black); // draws the box
            batch.DrawString(font, "Return to Main menu", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("Return to Main Menu").X / 2, MenuVariables.LoadMainMenuReturn.Y), Color.Crimson); //draws the text within the box

            //draw the textbox
            batch.Draw(FillText, MenuVariables.LoadTextBox, Color.Black); // draws the box
            batch.DrawString(font, "This Functionality Currently Doesn't Exist!", new Vector2(MenuVariables.LoadMenuBox.Width / 2 - font.MeasureString("This functionality currently Doesn't Exist").X / 2, MenuVariables.LoadTextBox.Y), Color.Crimson); //draws the text within the textbox
        }



/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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



    }//end of class
}
