﻿#region Using Statements
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
        public static MouseState poppy; // the mouse state for the menus

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
            
            //make the mouse visible on screen
            this.IsMouseVisible = true;
           
            
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

            //update the mouse
            poppy = Mouse.GetState();
            //menu control
            if (MenuVariables.main == true)
            {
                shrodingers.MainMenuLogic(this, font);
            }
            if (MenuVariables.newGame == true)
            {
                shrodingers.NewGameMenuLogic(this, font);
            }
            if (MenuVariables.options == true)
            {
                shrodingers.OptionsMenuLogic(this, font);
            }
            if (MenuVariables.loadGame == true)
            {
                shrodingers.LoadGameMenuLogic(this, font);
            }

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

            ///////////////////////////////// DRAW MENUS //////////////////////////////////////////

            //pandora.DisplayTestPopup("Tower Control Game of No-Names", "this is some test text", "USUP", spriteBatch, font, this);
            if (MenuVariables.main == true)
            {
                pandora.DisplayMainMenu(spriteBatch, font, this);
            }
            if(MenuVariables.newGame == true)
            {
                pandora.DisplayNewGameMenu(spriteBatch, font, this);
            }
            if (MenuVariables.options == true)
            {
                pandora.DisplayOptionsMenu(spriteBatch, font, this);
            }
            if (MenuVariables.loadGame == true)
            {
                pandora.DisplayLoadGameMenu(spriteBatch, font, this);
            }
            
            //////////////////////////////// END OF DRAW MENUS //////////////////////////////////

            spriteBatch.End();
            base.Draw(gameTime);
        }
        
    
    }
}
