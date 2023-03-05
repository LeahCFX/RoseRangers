using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Project_Yeehaw
{
    /// <summary>
    /// different game states
    /// </summary>
    public enum GameState
    {
        Menu,
        Load,
        Game,
        GameOver
    }

    /// <summary>
    /// different collectible colors
    /// </summary>
    public enum InkColor
    {
        Red,
        Yellow,
        Blue,
        Orange,
        Green,
        Purple
    }

    /// <summary>
    /// different player animation states
    /// </summary>
    public enum PlayerState
    {
        WalkLeft,
        WalkRight,
        StandLeft,
        StandRight,
        JumpLeft,
        JumpRight
    }

    /// <summary>
    /// state of the mixer vile
    /// </summary>
    public enum Capacity
    {
        Empty,
        Half,
        Full
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState screenState;

        private Texture2D buttonTexture;
        private Texture2D title;
        private Texture2D bgimg;
        private SpriteFont font;

        //buttons
        private Button play;
        private Button back;
        private Button quit;
        private Button tryagain;

        //level loading
        StreamReader reader;
        List<GameObject> level = new List<GameObject>();
        string levelFile;

        #region BigPotions

        #endregion

        #region SmallVials

        #endregion

        //dino sprite sheets 
        private Texture2D blueDinoSpriteSheet;
        private Texture2D redDinoSpriteSheet;
        private Texture2D yellowDinoSpriteSheet;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //default to menu
            screenState = GameState.Menu;

            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            //level loading
            levelFile = "";
            reader = null;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            title = Content.Load<Texture2D>("title");
            bgimg = Content.Load<Texture2D>("bgimg");
            //buttons
            buttonTexture = Content.Load<Texture2D>("rectangle");
            play = new Button(buttonTexture, new Rectangle(0, 0, 100, 100));
            back = new Button(buttonTexture, new Rectangle(0, 0, 100, 100));
            quit = new Button(buttonTexture, new Rectangle(0, 200, 100, 100));
            tryagain = new Button(buttonTexture, new Rectangle(0, 0, 100, 100));


            //placeholderfont
            font = Content.Load<SpriteFont>("File");

            //
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Update FSM yeet
            switch (screenState)
            {
                case GameState.Menu:
                    if (play.MouseClick() && play.MousePosition())
                    {
                        screenState = GameState.Load;
                    }
                    if (quit.MouseClick() && quit.MousePosition())
                    {
                        Environment.Exit(0);
                    }

                    break;
                case GameState.Game:
                    //if timer runs out lose

                    break;
                case GameState.Load: 
                    //leah code here
                    screenState = GameState.Game;

                    break;
                case GameState.GameOver:
                    if (quit.MouseClick() && quit.MousePosition())
                    {
                        Environment.Exit(0);
                    }
                    if (tryagain.MouseClick() && tryagain.MousePosition())
                    {
                        screenState = GameState.Menu;
                    }
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSeaGreen);

            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            //Draw FSM yeet
            switch (screenState)
            {
                case GameState.Menu:
                    _spriteBatch.Draw(bgimg, new Rectangle(0,0, 1024, 768), Color.White);
                    //draw buttons
                    play.Draw(_spriteBatch);
                    quit.Draw(_spriteBatch);
                    //placeholder words
                    _spriteBatch.DrawString(font, "menu", new Vector2(100, 0), Color.Black);
                    _spriteBatch.Draw(title, new Rectangle(614/2, 268/2, 400, 200), Color.White);
                    break;
                case GameState.Game:
                    _spriteBatch.DrawString(font, "game", new Vector2(0,0), Color.White);
                    break;
                case GameState.Load:
                    _spriteBatch.DrawString(font, "load", new Vector2(0, 0), Color.White);
                    break;
                case GameState.GameOver:
                    _spriteBatch.DrawString(font, "gameover", new Vector2(0, 0), Color.White);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadLevel(string level)
        {
            //read in the file
            reader = new StreamReader("Content/Levels/" + level);

            //read each line of data in the file
            string lineOfData = "";

            while ((lineOfData = reader.ReadLine()) != null)
            {
                //store the split data into an array
                string[] objectData = lineOfData.Split(",");

                //determine what kind of game object it is
                switch (objectData[2])
                {
                    case "belowGrass":
                        break;
                    case "grass":
                        break;
                    case "stoneRight":
                        break;
                    case "stoneLeft":
                        break;
                    case "platform":
                        break;
                    case "pink":
                        break;
                    case "blue":
                        break;
                    case "red":
                        break;
                    case "yellow":
                        break;
                    case "white":
                        break;
                }
            }
        }
    }
}