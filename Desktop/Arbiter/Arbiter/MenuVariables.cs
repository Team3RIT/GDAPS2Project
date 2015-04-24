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

        public enum MENUS { MAIN, PAUSE, LOADGAME, SAVEGAME, OPTIONS, INPUT, NEWGAME, LOADINGSCREEN, WINSCREEN, MAPMENU,} //Contains all of the different menus
        public static MENUS MenuStates = MENUS.MAIN; //Controls the state of the menus using the above enum.
        
        ////////////////////////////////////////////////COLOR VARIABLES///////////////////////////////////////////////////////
        //change the color of different boxes for different menus?
        public static Color BoxColor = Color.Black; //thecolor for generic title menu boxes - either equal to normal color, or to hovercolor
        public static Color TypeColor = Color.Maroon;//color for a box that has text you typed into
        public static Color HoverColor = Color.MediumBlue; //color for when mouse is hovering over a rectangle (not main rectangle)
        public static Color BackgroundColor = Color.Teal; //Color for the mainmenuboxes - used in the background - changes with each update version

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
        public static Color LoadTextTitleColor = BoxColor;
        public static Color LoadTextBoxColor = BoxColor;
        public static Color LoadSubmitColor = BoxColor;
        public static Color LoadClearColor = BoxColor;

        //options specific colors
        public static Color OptionsMenuBoxColor = BoxColor;
        public static Color OptionsTitleColor = BoxColor;
        public static Color OptionsMainMenuReturnColor = BoxColor;
        public static Color OptionsInputColor = BoxColor;

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

        //input menu colors
        public static Color InputMenuBoxColor = BoxColor;
        public static Color InputTitleColor = BoxColor;
        public static Color InputMouseColor = BoxColor;
        public static Color InputGamePadColor = BoxColor;
        public static Color InputKeyBoardColor = BoxColor;
        public static Color InputOptionsReturnColor = BoxColor;
        public static Color InputMessageColor = BoxColor;


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
        public static Rectangle LoadTextTitle; //doesn't change color
        public static Rectangle LoadTextBox; //changes to a different color
        public static Rectangle LoadSubmit;
        public static Rectangle LoadClear;
        
        //options rectangles
        public static Rectangle OptionsMenuBox;
        public static Rectangle OptionsTitle;
        public static Rectangle OptionsMainMenuReturn;
        public static Rectangle OptionsInput;

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

        //input menu variables
        public static Rectangle InputMenuBox;
        public static Rectangle InputTitle;
        public static Rectangle InputMouse;
        public static Rectangle InputGamePad;
        public static Rectangle InputKeyBoard;
        public static Rectangle InputOptionsReturn;
        public static Rectangle InputMessage;

        //game screen variables - Margaret
        public static Texture2D playerIndicatorContainer;
        public static Texture2D playerIndicatorColorBox;



        /////////////////////////////// EXTRANEOUS INTERFACE MENU VARIABLES ////////////////////////////////////////// 

        public static bool ControllerConnected;  //defaulted to not having a gamepad connected
        public static bool CanType = false;
        public static string filename = "";

        ///////////////////PROPERTIES//////////////////////////////////
        /*public static bool Main
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

        public static bool Input
        {
            get { return input; }
            set { input = value; }
        }
        */

        //Reads keyboard input, puts appends it to MenuVariables.filename within the method, yay. -Margaret
        public static void GetKey(KeyboardState keyState, ref KeyboardState oldKeyboardState)
        {
            Keys[] keys = keyState.GetPressedKeys();
            string previousString = "";
            
            foreach(Keys key in keys)
            {
                
                if (oldKeyboardState.IsKeyUp(key))
                {
                    //backspace
                    if (key == Keys.Back && MenuVariables.filename.Length > 0)
                    {
                        MenuVariables.filename = MenuVariables.filename.Remove(MenuVariables.filename.Length - 1, 1);
                    }
                    //space
                    else if (key == Keys.Space)
                    {
                        MenuVariables.filename = MenuVariables.filename.Insert(MenuVariables.filename.Length, " ");
                    }
                    //enter
                    else if (key == Keys.Enter)
                    {
                        //nothing happens, we can add code here to have it hit submit if we want
                    }
                    else 
                        //number keys
                        switch(key)
                        {
                            case Keys.D0:
                            case Keys.NumPad0:
                                {
                                    MenuVariables.filename += '0';
                                    break;
                                }
                            case Keys.D1:
                            case Keys.NumPad1:
                                {
                                    MenuVariables.filename += '1';
                                    break;
                                }
                            case Keys.D2:
                            case Keys.NumPad2:
                                {
                                    MenuVariables.filename += '2';
                                    break;
                                }
                            case Keys.D3:
                            case Keys.NumPad3:
                                {
                                    MenuVariables.filename += '3';
                                    break;
                                }
                            case Keys.D4:
                            case Keys.NumPad4:
                                {
                                    MenuVariables.filename += '4';
                                    break;
                                }
                            case Keys.D5:
                            case Keys.NumPad5:
                                {
                                    MenuVariables.filename += '5';
                                    break;
                                }
                            case Keys.D6:
                            case Keys.NumPad6:
                                {
                                    MenuVariables.filename += '6';
                                    break;
                                }
                            case Keys.D7:
                            case Keys.NumPad7:
                                {
                                    MenuVariables.filename += '7';
                                    break;
                                }
                            case Keys.D8:
                            case Keys.NumPad8:
                                {
                                    MenuVariables.filename += '8';
                                    break;
                                }
                            case Keys.D9:
                            case Keys.NumPad9:
                                {
                                    MenuVariables.filename += '9';
                                    break;
                                }
                            //all other keys
                            default:
                            {
                                string keyString = key.ToString();
                                bool isUpperCase = (keyState.IsKeyDown(Keys.RightShift) || keyState.IsKeyDown(Keys.LeftShift));



                                if (keyString.Length == 1 && previousString != keyString)
                                {
                                 MenuVariables.filename += isUpperCase ? keyString.ToUpper() : keyString.ToLower();
                                }
                                previousString = keyString;
                                break;
                            }
                }
                }

            }
            oldKeyboardState = keyState;
        }
            
        }



    }

