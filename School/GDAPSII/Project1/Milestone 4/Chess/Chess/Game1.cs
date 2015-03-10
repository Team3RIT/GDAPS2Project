#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Chess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        static GraphicsDeviceManager graphics;
        static SpriteBatch spriteBatch;

        #region Piece list attributes
        public static List<Pawn> whitePawns; //all static so other classes can use them
        public static List<Pawn> blackPawns;
        public static List<Knight> whiteKnights;
        public static List<Knight> blackKnights;
        public static List<Bishop> whiteBishops;
        public static List<Bishop> blackBishops;
        public static List<Rook> whiteRooks;
        public static List<Rook> blackRooks;
        public static List<Queen> whiteQueen;
        public static List<Queen> blackQueen;
        public static King whiteKing;
        public static King blackKing;
        #endregion
        static Texture2D boardBG;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            boardBG = Content.Load<Texture2D>("chessboard");

            
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
            #region Piece Initialization
            //PAWNS
            whitePawns = new List<Pawn>();
            //add the white pawns from left to right
            for (int i = 0; i < GameVariables.boardSpaceDim; i++)
            {
                //white is bottom, so y should be boardSpaceDim-1, white = true
                whitePawns.Add(new Pawn(i, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("whitePawn"), true));
            }
            blackPawns = new List<Pawn>();
            //add the black pawns from left to right
            for (int i = 0; i < GameVariables.boardSpaceDim; i++)
            {
                //black is bottom, so y should be 0, black = false
                whitePawns.Add(new Pawn(i, 0, Content.Load<Texture2D>("blackPawn"), false));
            }

            //KNIGHTS
            whiteKnights = new List<Knight>() { new Knight(1, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("whiteKnight"), true), new Knight(6, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("whiteKnight"), true) };
            blackKnights = new List<Knight>() { new Knight(1, 0, Content.Load<Texture2D>("blackKnight"), false), new Knight(6, 0, Content.Load<Texture2D>("blackKnight"), false) };
            //BISHOPS
            whiteBishops = new List<Bishop>() { new Bishop(2, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("whiteBishop"), true), new Bishop(5, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("blackBishop"), true) };
            blackBishops = new List<Bishop>() { new Bishop(2, 0, Content.Load<Texture2D>("blackBishop"), false), new Bishop(5, 0, Content.Load<Texture2D>("blackBishop"), false) };
            //ROOKS
            whiteRooks = new List<Rook>() { new Rook(0, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("whiteRook"), true), new Rook(7, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("whiteKnight"), true) };
            blackRooks = new List<Rook>() { new Rook(0, 0, Content.Load<Texture2D>("blackRook"), false), new Rook(7, 0, Content.Load<Texture2D>("blackKnight"), false) };
            //QUEENS
            whiteQueen = new List<Queen>() { new Queen(3, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("whiteQueen"), true) };
            blackQueen = new List<Queen>(){new Queen(3, 0, Content.Load<Texture2D>("blackQueen"), false)};
            //KINGS 
            whiteKing = new King(4, GameVariables.boardSpaceDim - 1, Content.Load<Texture2D>("blackKing"), true);
            blackKing = new King(4, 0, Content.Load<Texture2D>("blackKing"), false);
            #endregion

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
           
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(boardBG, new Rectangle(GameVariables.screenbufferHorizontal, GameVariables.screenbufferVertical, GameVariables.boardDim, GameVariables.boardDim), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
            
        }

        public static void AddPiece()
        {
            spriteBatch.Begin();
            //add stuff here later, need some kind of selection thing to choose a new piece to add.
            //remember to set the hasMoved attribute of any piece added to true
            spriteBatch.End();
        }
    }
}
