#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;//added this
using Microsoft.Xna.Framework.Audio;//added this
using System.Collections; //added this
#endregion

namespace Arbiter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        MenuDisplay DisplayBox;
        MenuLogic LogicBox;
        public static MouseState click; // the mouse state for the menus

        //controller attributes
        public static int preferredInput; //controls which input is used for the game
        GamePadState gamepad;
        GamePadState previousgamepadState;

        KeyboardState keyboard;
        KeyboardState previouskeyboardState;

        MouseState previousmouseState;

        //images for game pieces/board states
        public static Texture2D Heavy;
        public static Texture2D Light;
        public static Texture2D Jumper;
        public static Texture2D Standard;
        public static Texture2D Tower;
        public static Texture2D Normal;
        public static Texture2D Obstacle;
        public static Texture2D MenuBorder;

        //Player Turn Attributes
        Match testMatch;
        Unit selectedunit;
        List<Unit> movedUnits;
        int currentPlayer; //ID num of current player


        public enum States { MENU, SETUP, PLAYERTURN, ENDGAME } //Contains gamestates used in Update(). Update as needed!
        public static States gameState; //Controls the state of the game using the above enum.
        


        //varaiables for the UnitMove methods
        public bool PotentialMoves; //if true run DisplayUnitMove, if false do not
        public Unit PossibleMovesUnit; //used to store the unit that is put in Display UnitMove from UnitMove
        
        //aniamation variables
        public static bool Anim;
        
        public List<Unit> Animations;
        
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GameVariables.screenWidth;  //sets the window size. DO NOT SET THIS DIRECTLY, change in GameVariables class
            graphics.PreferredBackBufferHeight = GameVariables.screenHeight; 
            graphics.ApplyChanges();
            

            IsMouseVisible = true;
            
            //define menu objects
            LogicBox = new MenuLogic();  
            DisplayBox = new MenuDisplay();


            //turn logic initialization
            selectedunit = null;
            currentPlayer = 1;
            movedUnits = new List<Unit>();

            //animation logic
            Anim = false;
            
            Animations = new List<Unit>();

            
            
        }
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: Add your initialization logic here



            

            //make the mouse visible on screen
            this.IsMouseVisible = true;
           
            //start off the FSM
            gameState = States.MENU;
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
           
            
            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Font1");

            //load in the images for the pieces and game tiles
            Heavy = Content.Load<Texture2D>("Heavy");
            Light = Content.Load<Texture2D>("Light");
            Jumper = Content.Load<Texture2D>("Jumper");
            Standard = Content.Load<Texture2D>("Standard");
            Tower = Content.Load<Texture2D>("Tower");
            Normal = Content.Load<Texture2D>("NormalTile");
            Obstacle = Content.Load<Texture2D>("Obstacle");
            MenuBorder = Content.Load<Texture2D>("menu border4");


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //update the mouse
            click = Mouse.GetState();
            //Josh - gamepad stuff
            //GamePadThumbSticks sticks = gamepad.ThumbSticks;
            switch(preferredInput)
            { 
                case 0:
                #region mouse movement
            
            if (GameVariables.OnBoard(new Vector2((int)(click.Position.X - GameVariables.screenbufferHorizontal) / GameVariables.spaceDim, 
                (int)(click.Position.Y - GameVariables.screenbufferVertical) / GameVariables.spaceDim)))
             GameVariables.gamePadLocation = new Vector2(((int)click.Position.X - GameVariables.screenbufferHorizontal) / GameVariables.spaceDim,
                 ((int)click.Position.Y - GameVariables.screenbufferVertical) / GameVariables.spaceDim);
           
            #endregion
                break;
                case 1:
                #region gamepad movement
            switch (currentPlayer) //lets only the current player's gamepad control the cursor
            { 
                case 1: gamepad = GamePad.GetState(PlayerIndex.One);
                       break;
                case 2: gamepad = GamePad.GetState(PlayerIndex.Two);
                       break;
                case 3: gamepad = GamePad.GetState(PlayerIndex.Three);
                       break;
                case 4: gamepad = GamePad.GetState(PlayerIndex.Four);
                       break;
             }   
            if (gamepad.IsConnected)
            {
              if(gameState == States.PLAYERTURN)
              {
                  if(gamepad.IsButtonDown(Buttons.DPadDown)&&(!previousgamepadState.IsButtonDown(Buttons.DPadDown)))
                  {
                      GameVariables.gamePadLocation.Y++;
                      if (GameVariables.gamePadLocation.Y >= GameVariables.BoardSpaceDim)
                          GameVariables.gamePadLocation.Y = GameVariables.BoardSpaceDim - 1;
                  }
                  if (gamepad.IsButtonDown(Buttons.DPadUp) && (!previousgamepadState.IsButtonDown(Buttons.DPadUp)))
                  {
                      GameVariables.gamePadLocation.Y--;
                      if (GameVariables.gamePadLocation.Y < 0 )
                          GameVariables.gamePadLocation.Y = 0;
                  }
                  if (gamepad.IsButtonDown(Buttons.DPadRight) && (!previousgamepadState.IsButtonDown(Buttons.DPadRight)))
                  {
                      GameVariables.gamePadLocation.X++;
                      if (GameVariables.gamePadLocation.X >= GameVariables.BoardSpaceDim)
                          GameVariables.gamePadLocation.X = GameVariables.BoardSpaceDim - 1;
                  }
                  if (gamepad.IsButtonDown(Buttons.DPadLeft) && (!previousgamepadState.IsButtonDown(Buttons.DPadLeft)))
                  {
                      GameVariables.gamePadLocation.X--;
                      if (GameVariables.gamePadLocation.X < 0)
                          GameVariables.gamePadLocation.X = 0;
                  }
              }

            }
            #endregion
                break;
                case 2:
                #region keyboard movement
            keyboard = Keyboard.GetState();
            if (gameState == States.PLAYERTURN)
            {
                if (keyboard.IsKeyDown(Keys.Down)&&(!previouskeyboardState.IsKeyDown(Keys.Down)))
                {
                    GameVariables.gamePadLocation.Y++;
                    if (GameVariables.gamePadLocation.Y >= GameVariables.BoardSpaceDim)
                        GameVariables.gamePadLocation.Y = GameVariables.BoardSpaceDim - 1;
                }
                if (keyboard.IsKeyDown(Keys.Up) && (!previouskeyboardState.IsKeyDown(Keys.Up)))
                {
                    GameVariables.gamePadLocation.Y--;
                    if (GameVariables.gamePadLocation.Y < 0)
                        GameVariables.gamePadLocation.Y = 0;
                }
                if (keyboard.IsKeyDown(Keys.Left) && (!previouskeyboardState.IsKeyDown(Keys.Left)))
                {
                    GameVariables.gamePadLocation.X--;
                    if (GameVariables.gamePadLocation.X < 0)
                        GameVariables.gamePadLocation.X = 0;
                }
                if (keyboard.IsKeyDown(Keys.Right) && (!previouskeyboardState.IsKeyDown(Keys.Right)))
                {
                    GameVariables.gamePadLocation.X++;
                    if (GameVariables.gamePadLocation.X >= GameVariables.BoardSpaceDim)
                        GameVariables.gamePadLocation.X = GameVariables.BoardSpaceDim - 1;
                }

            }
            #endregion
                break;
            }
            #region Josh's code - doesn't work
            /*Vector2 left = sticks.Left;
            Vector2 right = sticks.Right;
            int state = 0; // number 0 to three. different states will be different locations for rectangles to be drawn each corresponding to one of the menu buttons
            
            //todo - add a range of values that will work for left and right. 
            
            else
            {
                if (left.X == 0 && left.Y == 32767)
                {
                    // move up calls
                    if (state > 0)
                    {
                        state--;
                    }
                }
                if (left.X == 32767 && left.Y == 0)
                {
                    // move right calls
                }
                if (left.X == 0 && left.Y == 32768)
                {
                    // move down calls
                    if (state < 3)
                    {
                        state++;
                    }
                }
                if (left.X == -32768 && left.Y == 0)
                {
                    // move left calls
                }
            }
            */
            #endregion
        
            //finite state machine - Travis
            switch(gameState)
            {
                case States.MENU:
                    //menu control
                    if (MenuVariables.MenuStates == MenuVariables.MENUS.MAIN)
                    {
                        LogicBox.MainMenuLogic(this, font);
                    }
                    if (MenuVariables.MenuStates == MenuVariables.MENUS.NEWGAME)
                    {
                        LogicBox.NewGameMenuLogic(this, font);
                        gameState = States.SETUP;
                    }
                    if (MenuVariables.MenuStates == MenuVariables.MENUS.OPTIONS)
                    {
                        LogicBox.OptionsMenuLogic(this, font);
                    }
                    if (MenuVariables.MenuStates == MenuVariables.MENUS.INPUT)
                    {
                        LogicBox.InputMenuLogic(this, font); //menu to choose mouse, keyboard or gamepad input
                    }
                    if (MenuVariables.MenuStates == MenuVariables.MENUS.LOADGAME)
                    {
                        LogicBox.LoadGameMenuLogic(this, font);
                    }
                    if (MenuVariables.MenuStates == MenuVariables.MENUS.PAUSE)
                    {
                        LogicBox.PauseMenuLogic(this, font);
                    }
                    if (MenuVariables.MenuStates == MenuVariables.MENUS.WINSCREEN)
                    {
                        LogicBox.WinMenuLogic(this, font);
                    }
                    if(MenuVariables.MenuStates == MenuVariables.MENUS.MAPMENU)
                    {
                        LogicBox.LoadMapMenuLogic(this, font);
                    }
                    if(MenuVariables.MenuStates == MenuVariables.MENUS.SAVEGAME)
                    {
                        LogicBox.SaveGameMenuLogic(this,font);
                    }
                    break;
                case States.SETUP:
                    testMatch = new Match(2);
                    testMatch.Draft();
                    gameState = States.PLAYERTURN;
                    break;

                case States.PLAYERTURN:
                    //code here to handle turn
                    #region turnlogic
                    
                    
                        if (keyboard.IsKeyDown(Keys.P) || click.RightButton == ButtonState.Pressed || gamepad.IsButtonDown(Buttons.Start))
                        {
                            MenuVariables.MenuStates = MenuVariables.MENUS.PAUSE;
                            gameState = States.MENU;
                        }

                        if ((gamepad.IsButtonDown(Buttons.A) || keyboard.IsKeyDown(Keys.Space)|| click.LeftButton == ButtonState.Pressed)
                            &&!((previousgamepadState.IsButtonDown(Buttons.A)|| previouskeyboardState.IsKeyDown(Keys.Space) || previousmouseState.LeftButton != ButtonState.Pressed ))
                            && movedUnits.Count != GameVariables.NumPiecesPerTurn)
                        {

                            if (GameVariables.board[(int)GameVariables.gamePadLocation.X, (int)GameVariables.gamePadLocation.Y] is Unit
                                && GameVariables.board[(int)GameVariables.gamePadLocation.X, (int)GameVariables.gamePadLocation.Y].owner.ID == currentPlayer
                                && !movedUnits.Contains((Unit)GameVariables.board[(int)GameVariables.gamePadLocation.X, (int)GameVariables.gamePadLocation.Y]))
                            {
                                selectedunit = (Unit)GameVariables.board[(int)GameVariables.gamePadLocation.X, (int)GameVariables.gamePadLocation.Y];
                                PotentialMoves = true; //runs unitmove with selected unti
                                
                                
                            }

                            else if (selectedunit != null)
                            {
                                if (selectedunit.PossibleMoves.Contains(GameVariables.gamePadLocation))
                                {
                                    
                                    selectedunit.Move(GameVariables.gamePadLocation);
                                    movedUnits.Add(selectedunit);
                                    Anim = true; //run animation
                                    PotentialMoves = false; //stop displaying spots where you can move
                                    selectedunit = null;
                                }

                            }
                        }
                        //run the animation of units that are moving
                        if (Anim == true)
                        {
                            Animate();
                        }


#endregion
                        if (movedUnits.Count == GameVariables.NumPiecesPerTurn && Anim == false) //signals end of turn
                        {
                            //reset things
                            
                            movedUnits.Clear();
                            //at end of turn
                            if (testMatch.TurnManager()) //if returns true end game
                                gameState = States.ENDGAME;
                            else
                            { 
                                if(GameVariables.players.Count -1 < currentPlayer + 1) //account for the filler player taking up the first element of the list
                                {
                                    currentPlayer = 1; //reset to first player
                                }
                                else
                                {
                                    currentPlayer++; //go to next player
                                }
                            } //else other players turn
                        }
                    break;
                

                case States.ENDGAME:
                    MenuVariables.MenuStates = MenuVariables.MENUS.WINSCREEN;
                    gameState = States.MENU;
                    break;
            }

            //music.Play();
            previousgamepadState = gamepad; //save this gamepad state for the next loop
            previouskeyboardState = keyboard;
            previousmouseState = click;
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            
            
            ///////////////////////////////// DRAW MENUS //////////////////////////////////////////

            //pandora.DisplayTestPopup("Tower Control Game of No-Names", "this is some test text", "USUP", spriteBatch, font, this);
            if (MenuVariables.MenuStates == MenuVariables.MENUS.MAIN)
            {
                DisplayBox.DisplayMainMenu(spriteBatch, font, this);
            }
            if (MenuVariables.MenuStates == MenuVariables.MENUS.NEWGAME)
            {
                //will only ever be true is there actually is a controller connected (MenuVariables.ControllerConnected == true)
                //testing in menulogic for this is currently commented out
                //DisplayBox.DisplayNewGameMenu(spriteBatch, font, this);
                DrawBoard(); 
            }
            if (MenuVariables.MenuStates == MenuVariables.MENUS.OPTIONS)
            {
                DisplayBox.DisplayOptionsMenu(spriteBatch, font, this);
                

            }
            if (MenuVariables.MenuStates == MenuVariables.MENUS.LOADGAME)
            {
                DisplayBox.DisplayLoadGameMenu(spriteBatch, font, this);


            }
            if (MenuVariables.MenuStates == MenuVariables.MENUS.PAUSE)
            {
                DisplayBox.DisplayPauseMenu(spriteBatch, font, this);


            }
            if (MenuVariables.MenuStates == MenuVariables.MENUS.WINSCREEN)
            {
                DisplayBox.DisplayWinMenu(spriteBatch, font, this);
            }
            if (MenuVariables.MenuStates == MenuVariables.MENUS.INPUT)
            {
                DisplayBox.DisplayInputMenu(spriteBatch, font, this);
            }
            if(MenuVariables.MenuStates == MenuVariables.MENUS.MAPMENU)
            {
                DisplayBox.DisplayMapMenu(spriteBatch, font, this);
            }
            if(MenuVariables.MenuStates == MenuVariables.MENUS.SAVEGAME)
            {
                DisplayBox.DisplaySaveMenu(spriteBatch, font, this);
            }

            if (gameState == States.MENU)
            {
                
                spriteBatch.DrawString(font, "Remember the Menus Only Take Mouse Input Right Now!", new Vector2(150, 500), Color.Black);
            }
            //////////////////////////////// END OF DRAW MENUS //////////////////////////////////
            
            if(gameState == States.PLAYERTURN)
            {
                DrawBoard();
                spriteBatch.Draw(Normal, new Rectangle((int)GameVariables.gamePadLocation.X*GameVariables.spaceDim+GameVariables.screenbufferHorizontal,(int)GameVariables.gamePadLocation.Y*GameVariables.spaceDim+GameVariables.screenbufferVertical,GameVariables.spaceDim,GameVariables.spaceDim), Color.Red * 0.5f);
            }


            //show possible moves a unit can take

            if (PotentialMoves == true)
            {
                DisplayUnitMove(selectedunit);
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        public void DrawBoard() //should be between spritebatch Begin and End
        {
            for (int i = 0; i < GameVariables.BoardSpaceDim; i++)
            {
                for (int j = 0; j < GameVariables.BoardSpaceDim; j++)
                {
                    spriteBatch.Draw(Normal, new Rectangle(i * GameVariables.spaceDim + GameVariables.screenbufferHorizontal, j * GameVariables.spaceDim + GameVariables.screenbufferVertical,
                        GameVariables.spaceDim, GameVariables.spaceDim), Color.White);   
                    //Not sure what this is??? - Nick
                    //we need a picture to represent a tile with nothing on it,
                    //it could theoretically just be a white square - Margaret
                    if (GameVariables.board[i, j] != null)
                    {
                        spriteBatch.Draw(GameVariables.board[i,j].icon, GameVariables.board[i, j].Region, GameVariables.board[i, j].color);

                    }
                    
                }
            }
            foreach(Tower tower in GameVariables.towers)
            {
                if(tower.isClaimed)
                {
                    spriteBatch.Draw(tower.icon, tower.Region, Color.White);
                    spriteBatch.Draw(tower.claimedBy.icon, tower.claimedBy.Region, tower.claimedBy.color);
                }
            }
            foreach(Unit unit in movedUnits) //makes sure that the units moving stay on top
            {
                spriteBatch.Draw(unit.icon, unit.Region, unit.color);
            }
        }

       //Nick
        public void UnitMove(Unit person)
        {
            //no longer requires a Unit due to existence of selectedUnit variable
            //if called makes sure DisplayUnitMove is Run untill a unit is actually selected
            //set possible choices equal to true
            PotentialMoves = true; //DisplayUnitMove will be run in draw as long as this is true
            PossibleMovesUnit = person; //this variable should then be plugged into DisplayUnitMove when its in the draw method

        }

        public void DisplayUnitMove(Unit person)
        {

            foreach (Vector2 guy in person.Select())
            {
                //for every possible place you could move the unit 
                //spriteBatch.Begin(); //no need for spritebatches, this is called within draw now
                Rectangle rect = new Rectangle((int)(guy.X * GameVariables.spaceDim + GameVariables.screenbufferHorizontal), (int)(guy.Y * GameVariables.spaceDim + GameVariables.screenbufferVertical), GameVariables.spaceDim, GameVariables.spaceDim);
                if (currentPlayer == 1)
                { spriteBatch.Draw(Normal, rect, Color.Maroon * 0.5f); } //highlight the area maroon for player 1

                if (currentPlayer == 2)
                {spriteBatch.Draw(Normal, rect, Color.MediumBlue * 0.5f);} //highlight the area blue for player 2
                //spriteBatch.End();
            }
        }

        //Animate the methods as they move

        public void Animate()
        {
            //CURRENTLY NOT FUNCTIONING
            //move the unit's rectangle incrementally untill it reaches its final position 
            // increments repeatedly (should be called many times)
            
            // if there are no more units to be animated, set anim to false and end method
            bool finished = true;
            
            //if there still are people to be animated, change their location accordingly
            foreach (Unit person in movedUnits)
            {
                float x = person.Location.X * GameVariables.spaceDim + GameVariables.screenbufferHorizontal;
                float y = person.Location.Y * GameVariables.spaceDim + GameVariables.screenbufferVertical;
                if (x != person.Region.X || y!= person.Region.Y)
                {
                    finished = false;
                    float differenceX = x - person.Region.X; //find the total differences between the current x position of the rectangle and its final pposition
                    float differenceY = y - person.Region.Y; //find the total differences between the current y position of the rectangle and its final position

                    if (differenceX > 0) //if the difference is positive increase its x value
                        person.RegionX = (person.Region.X + 1);

                    if (differenceX < 0) //if the difference is negative, decrease the x value
                        person.RegionX = (person.Region.X - 1);

                    if (differenceY > 0)  //if the difference is positive increase the y position
                        person.RegionY = (person.Region.Y + 1);

                    if (differenceY < 0)  //if the difference is negative decrease the y position
                        person.RegionY = (person.Region.Y - 1);

                    //draw the piece!
                    //spriteBatch.Draw(person.icon, person.icon.Bounds, Color.White); //not anymore!
                    //(int)location.X * GameVariables.spaceDim + GameVariables.screenbufferHorizontal;
                    //(int)location.Y * GameVariables.spaceDim + GameVariables.screenbufferVertical;
                }



                
            }

            if (finished && movedUnits.Count == GameVariables.NumPiecesPerTurn) //loop is finished
            {
                Anim = false;
                return;
            }
        }
    }
}
