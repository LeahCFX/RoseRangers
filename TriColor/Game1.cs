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
        private StreamReader reader;
        private List<GameObject> level = new List<GameObject>();
        private string levelFile;

        #region BigPotions
        // BLUE POTIONS -------------------------------------------------------
        private Texture2D bigB1;
        private Texture2D bigB2;
        private Texture2D bigB3;
        private Texture2D bigB4;
        private Texture2D bigB5;
        private Texture2D bigB6;
        private Texture2D bigB7;
        private Texture2D bigB8;

        // RED POTIONS --------------------------------------------------------
        private Texture2D bigR1;
        private Texture2D bigR2;
        private Texture2D bigR3;
        private Texture2D bigR4;
        private Texture2D bigR5;
        private Texture2D bigR6;
        private Texture2D bigR7;
        private Texture2D bigR8;

        // YELLOW POTION ------------------------------------------------------
        private Texture2D bigY1;
        private Texture2D bigY2;
        private Texture2D bigY3;
        private Texture2D bigY4;
        private Texture2D bigY5;
        private Texture2D bigY6;
        private Texture2D bigY7;
        private Texture2D bigY8;

        #endregion

        #region SmallVials
        //EMPTY VIALS ------------------------------------------------------------
        private Texture2D smallEmpty1;
        private Texture2D smallEmpty2;
        private Texture2D smallEmpty3;
        private Texture2D smallEmpty4;
        private Texture2D smallEmpty5;
        private Texture2D smallEmpty6;
        private Texture2D smallEmpty7;

        //BLUE VIALS -------------------------------------------------------------
        //full
        private Texture2D smallFullB0;
        private Texture2D smallFullB1;
        private Texture2D smallFullB2;
        private Texture2D smallFullB3;
        private Texture2D smallFullB4;
        private Texture2D smallFullB5;
        private Texture2D smallFullB6;
        private Texture2D smallFullB7;

        //half
        private Texture2D smallHalfB1;
        private Texture2D smallHalfB2;
        private Texture2D smallHalfB3;
        private Texture2D smallHalfB4;
        private Texture2D smallHalfB5;
        private Texture2D smallHalfB6;
        private Texture2D smallHalfB7;

        //RED VIALS ---------------------------------------------------------------
        //full
        private Texture2D smallFullR1;
        private Texture2D smallFullR2;
        private Texture2D smallFullR3;
        private Texture2D smallFullR4;
        private Texture2D smallFullR5;
        private Texture2D smallFullR6;
        private Texture2D smallFullR7;

        //half
        private Texture2D smallHalfR1;
        private Texture2D smallHalfR2;
        private Texture2D smallHalfR3;
        private Texture2D smallHalfR4;
        private Texture2D smallHalfR5;
        private Texture2D smallHalfR6;
        private Texture2D smallHalfR7;

        //YELLOW VIALS -------------------------------------------------------------
        //full
        private Texture2D smallFullY1;
        private Texture2D smallFullY2;
        private Texture2D smallFullY3;
        private Texture2D smallFullY4;
        private Texture2D smallFullY5;
        private Texture2D smallFullY6;
        private Texture2D smallFullY7;

        //half
        private Texture2D smallHalfY1;
        private Texture2D smallHalfY2;
        private Texture2D smallHalfY3;
        private Texture2D smallHalfY4;
        private Texture2D smallHalfY5;
        private Texture2D smallHalfY6;
        private Texture2D smallHalfY7;

        //GREEN VIALS -------------------------------------------------------------
        private Texture2D smallFullG0;
        private Texture2D smallFullG1;
        private Texture2D smallFullG2;
        private Texture2D smallFullG3;
        private Texture2D smallFullG4;
        private Texture2D smallFullG5;
        private Texture2D smallFullG6;
        private Texture2D smallFullG7;

        //ORANGE VIALS -------------------------------------------------------------
        private Texture2D smallFullO0;
        private Texture2D smallFullO1;
        private Texture2D smallFullO2;
        private Texture2D smallFullO3;
        private Texture2D smallFullO4;
        private Texture2D smallFullO5;
        private Texture2D smallFullO6;
        private Texture2D smallFullO7;

        //PURPLE VIALS -------------------------------------------------------------
        private Texture2D smallFullP0;
        private Texture2D smallFullP1;
        private Texture2D smallFullP2;
        private Texture2D smallFullP3;
        private Texture2D smallFullP4;
        private Texture2D smallFullP5;
        private Texture2D smallFullP6;
        private Texture2D smallFullP7;
        #endregion

        //dino sprite sheets 
        private Texture2D blueDinoSpriteSheet;
        private Texture2D redDinoSpriteSheet;
        private Texture2D yellowDinoSpriteSheet;

        //terrain sheet
        private Texture2D terrain;

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

            //terrain
            terrain = Content.Load<Texture2D>("Terrain (32x32)");

            #region BigPotions
            // BLUE POTIONS ---------------------------------------------------
            bigB1 = Content.Load<Texture2D>("bigB1");
            bigB2 = Content.Load<Texture2D>("bigB2");
            bigB3 = Content.Load<Texture2D>("bigB3");
            bigB4 = Content.Load<Texture2D>("bigB4");
            bigB5 = Content.Load<Texture2D>("bigB5");
            bigB6 = Content.Load<Texture2D>("bigB6");
            bigB7 = Content.Load<Texture2D>("bigB7");
            bigB8 = Content.Load<Texture2D>("bigB8");

            // RED POTIONS ----------------------------------------------------
            bigR1 = Content.Load<Texture2D>("bigR1");
            bigR2 = Content.Load<Texture2D>("bigR2");
            bigR3 = Content.Load<Texture2D>("bigR3");
            bigR4 = Content.Load<Texture2D>("bigR4");
            bigR5 = Content.Load<Texture2D>("bigR5");
            bigR6 = Content.Load<Texture2D>("bigR6");
            bigR7 = Content.Load<Texture2D>("bigR7");
            bigR8 = Content.Load<Texture2D>("bigR8");

            // YELLOW POTIONS -------------------------------------------------
            bigY1 = Content.Load<Texture2D>("bigY1");
            bigY2 = Content.Load<Texture2D>("bigY2");
            bigY3 = Content.Load<Texture2D>("bigY3");
            bigY4 = Content.Load<Texture2D>("bigY4");
            bigY5 = Content.Load<Texture2D>("bigY5");
            bigY6 = Content.Load<Texture2D>("bigY6");
            bigY7 = Content.Load<Texture2D>("bigY7");
            bigY8 = Content.Load<Texture2D>("bigY8");

            #endregion
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