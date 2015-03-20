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
namespace Arbiter
{
    public static class MenuVariables
    {
        //Nick Serrao

        //boolean values which reporesent if the menu is showing or not
        //defaulted to false, except for main which is set to true, in order to show when game starts


        
        /// /////////////////////////////////////// BOOLEAN VARIABLES - ONE FOR EVERY MENU SCREEN ///////////////////////////////////////////
        //USE TO SET WHETHER MENU SCREENS ARE DRAWN IN THE DRAW MWETHOD, CHANGE THEIR VALUES IN THE METHODS CORRESPONDING WITH EACH MENU

        public static bool main = true;
        public static bool pause = false;
        public static bool loadGame = false;
        public static bool saveGame = false;
        public static bool options = false;
        public static bool newGame = false;
        public static bool loadingScreen = false;
        public static bool winScreen = false;

        ////////////////////////////////////////////////COLOR VARIABLES///////////////////////////////////////////////////////
        //change the color of different boxes for different menus?
        public static Color BoxColor = Color.Black; //thecolor for generic title menu boxes - either equal to normal color, or to hovercolor
        public static Color NormalColor = Color.Black; //a single default color for now, use whenever a mouse is not hovering over a rectangle
        public static Color HoverColor = Color.Orange; //color for when mouse is hovering over a rectangle (not main rectangle)
        public static Color BackgroundColor = Color.CadetBlue; //Color for the mainmenuboxes - used in the background - changes with each update version
        /*UPDATES:
        1.0 Cornflowerblue
        1.1 MediumSpringGreen
        2.0 CadetBlue;
        */
        /////////////////////////////////////////////// RECTANGLE VARIABLES /////////////////////////////////////////////////////
        
        // main menu rectangles
        public static Rectangle MainMenuBox;
        public static Rectangle MainTitle;
        public static Rectangle MainNewGame;
        public static Rectangle MainLoadGame;
        public static Rectangle MainOptions;
        public static Rectangle MainExit;
        
        //LoadGame rectangle
        public static Rectangle LoadMenuBox;
        public static Rectangle LoadTitle;
        public static Rectangle LoadMainMenuReturn;
        public static Rectangle LoadTextBox; //this functionality currently doesn't exist!
        
        //options rectangles
        public static Rectangle OptionsMenuBox;
        public static Rectangle OptionsTitle;
        public static Rectangle OptionsMainMenuReturn;




        ///////////////////PROPERTIES//////////////////////////////////
        public static bool Main
        {
            get { return main; }
            set { main = value; }
        }
        public static bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }
        public static bool LoadGame
        {
            get { return loadGame; }
            set { loadGame = value; }
        }
        public static bool SaveGame
        {
            get { return saveGame; }
            set { saveGame = value; }
        }
        public static bool Options
        {
            get { return options; }
            set { options = value; }
        }
        public static bool NewGame
        {
            get { return newGame; }
            set { newGame = value; }
        }
        public static bool LoadingScreen
        {
            get { return loadingScreen; }
            set { LoadingScreen = value; }
        }
        public static bool WinScreen
        {
            get { return winScreen; }
            set { winScreen = value; }
        }









    }
}
