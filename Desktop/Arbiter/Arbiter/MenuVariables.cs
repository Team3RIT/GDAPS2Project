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
        
        public static Color HoverColor = Color.MediumBlue; //color for when mouse is hovering over a rectangle (not main rectangle)
        public static Color BackgroundColor = Color.Lime; //Color for the mainmenuboxes - used in the background - changes with each update version

        // main menu box colors
        public static Color MainMenuBoxColor = BoxColor;
        public static Color MainTitleColor = BoxColor;
        public static Color MainNewGameColor = BoxColor;
        public static Color MainLoadGameColor = BoxColor;
        public static Color MainOptionsColor = BoxColor;
        public static Color MainExitColor = BoxColor;

        //NewGame specifc colors
        public static Color NewGameMenuBoxColor = BoxColor;
        public static Color NewGameTitleColor = BoxColor;
        public static Color NewGameMainMenuReturnColor = BoxColor;
        public static Color NewGameTwoPlayerColor = BoxColor; 

        //LoadGame specific colors
        public static Color LoadMenuBoxColor = BoxColor;
        public static Color LoadTitleColor = BoxColor;
        public static Color LoadMainMenuReturnColor = BoxColor;
        public static Color LoadTextBoxColor = BoxColor; //this functionality currently doesn't exist!

        //options specific colors
        public static Color OptionsMenuBoxColor = BoxColor;
        public static Color OptionsTitleColor = BoxColor;
        public static Color OptionsMainMenuReturnColor = BoxColor;

        //pause menu specific colors
        public static Color PauseMenuBoxColor = BoxColor;
        public static Color PauseTitleColor = BoxColor;
        public static Color PauseMainMenuReturnColor = BoxColor;
        public static Color PauseSaveGameColor = BoxColor;
        public static Color PauseReturnToGameColor = BoxColor;

        //victory menu specific colors
        public static Color WinMenuBoxColor = BoxColor;
        public static Color WinTitleColor = BoxColor;
        public static Color WinMainMenuReturnColor = BoxColor;
        public static Color WinExitColor = BoxColor;
        
        /*UPDATES:
        1.0 Cornflowerblue
        1.1 MediumSpringGreen
        2.0 CadetBlue;
        3.0 Lime - boxes change to MediumBlue
         
        */
        /////////////////////////////////////////////// RECTANGLE VARIABLES /////////////////////////////////////////////////////
        
        // main menu rectangles
        public static Rectangle MainMenuBox;
        public static Rectangle MainTitle;
        public static Rectangle MainNewGame;
        public static Rectangle MainLoadGame;
        public static Rectangle MainOptions;
        public static Rectangle MainExit;

        //NewGame rectangles
        public static Rectangle NewGameMenuBox;
        public static Rectangle NewGameTitle;
        public static Rectangle NewGameMainMenuReturn;
        public static Rectangle NewGameTwoPlayer;  //start a two player game
        public static Rectangle NewGameControllerConnected; //display if there is no controller connected

        //LoadGame rectangle
        public static Rectangle LoadMenuBox;
        public static Rectangle LoadTitle;
        public static Rectangle LoadMainMenuReturn;
        public static Rectangle LoadTextBox; //this functionality currently doesn't exist!
        
        //options rectangles
        public static Rectangle OptionsMenuBox;
        public static Rectangle OptionsTitle;
        public static Rectangle OptionsMainMenuReturn;

        //pause rectangles
        public static Rectangle PauseMenuBox;
        public static Rectangle PauseTitle;
        public static Rectangle PauseMainMenuReturn;
        public static Rectangle PauseSaveGame;
        public static Rectangle PauseReturnToGame;

        //victory rectangles
        public static Rectangle WinMenuBox;
        public static Rectangle WinTitle;
        public static Rectangle WinMainMenuReturn;
        public static Rectangle WinExit;
        /////////////////////////////// EXTRANEOUS INTERFACE MENU VARIABLES ////////////////////////////////////////// 

        public static bool ControllerConnected;  //defaulted to not having a gamepad connected
        

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
