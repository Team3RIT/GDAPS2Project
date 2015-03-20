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
        MenuDisplay pandora;
        MenuLogic shrodingers;
        

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
            shrodingers = new MenuLogic();  //in the future, please come up with self identifying variable names  - Margaret
            pandora = new MenuDisplay();
        }
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
              
           
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Font1");
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

            //menu control
            shrodingers.MainMenuLogic(this, font);

            //music.Play();
            
            
            
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
            //pandora.DisplayTestPopup("Tower Control Game of No-Names", "this is some test text", "USUP", spriteBatch, font, this);
            if (MenuVariables.main == true)
            {
                pandora.DisplayMainMenu(spriteBatch, font, this);
            }
            //pandora.DisplayOptionsMenu(spriteBatch, font, this);
            //pandora.DisplayLoadGameMenu(spriteBatch, font, this);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        public void DrawBoard() //should be between spritebatch Begin and End
        {
            for (int i = 0; i < GameVariables.boardSpaceDim; i++)
            {
                for (int j = 0; j < GameVariables.boardSpaceDim; j++)
                {
                    //spriteBatch.Draw(blank space picture)
                    if(GameVariables.board[i,j] == null)
                    {
                        //spriteBatch.Draw(ground texture)

                    }
                    else if(GameVariables.board[i,j] is Tower)
                    {
                        //spriteBatch.Draw(tower)

                    }
                    else if(GameVariables.board[i,j] is Structure)
                    {
                        //spriteBatch.Draw(structure)
                    }
                    else if(GameVariables.board[i,j] is StandardUnit)
                    {
                        //spriteBatch.Draw(standardUnit)
                    }
                    else if(GameVariables.board[i,j] is LightUnit)
                    {
                        //spriteBatch.Draw(LightUnit)
                    }
                    else if(GameVariables.board[i,j] is HeavyUnit)
                    {
                        //spriteBatch.Draw(HeavyUnit)
                    }
                    else if(GameVariables.board[i,j] is JumperUnit)
                    {
                        //spriteBatch.Draw(HeavyUnit)
                    }
                }
            }
        }
    
    }
}
