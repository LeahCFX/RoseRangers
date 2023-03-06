using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TriColor;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Runtime.InteropServices;

namespace Project_Yeehaw
{

    #region Enums
    /// <summary>
    /// different game states
    /// </summary>
    public enum GameState
    {
        Menu,
        Load,
        Game,
        GameLose,
        GameWin
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
        Purple,
        Clear
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
#endregion

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState screenState;

        //general game textures
        private Texture2D title;
        private Texture2D bgimg;
        private Texture2D skyimg;
        private SpriteFont font;

        //buttons
        private Button play;
        private Texture2D playTexture;
        private Button quit;
        private Texture2D quitTexture;
        private Button tryagain;
        private Texture2D tryagainTexture;

        //level loading
        private StreamReader reader;
        private List<GameObject> level = new List<GameObject>();
        private List<Tile> tileObjects = new List<Tile>();
        private List<Collectible> collectibles = new List<Collectible>();
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

        //sprite sheets 
        private Texture2D blueDinoSpriteSheet;
        private Texture2D redDinoSpriteSheet;
        private Texture2D yellowDinoSpriteSheet;
        private Texture2D terrain;

        //Player object
        private Player player;
        private Texture2D playerTexture;

        // Sprite sheet data
        private int numPlayerSprites;
        private int widthOfPlayerSprite;

        // Animation data
        private int playerCurrentFrame;
        private double fps;
        private double secondsPerFrame;
        private double timeCounter;

        //win state stuff
        private List<InkColor> objectives;
        private List<Small> fullInventory;
        private List<Small> inventory;
        int objectiveCounter;
        private double timer;

        //music
        Song titleSong;
        Song gameSong;
        Song endSong;

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

            //set the window screen dimensions
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            //level loading
            levelFile = "";
            reader = null;

            //list initialization for win
            objectives = new List<InkColor>();
            objectives.Add(InkColor.Purple);
            fullInventory= new List<Small>();
            inventory = new List<Small>();
            timer = 10;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";

            // TODO: use this.Content to load your game content here

            //screen textures
            title = Content.Load<Texture2D>("title");
            bgimg = Content.Load<Texture2D>("bgimg");
            skyimg = Content.Load<Texture2D>("Sky");

            //button textures
            playTexture = Content.Load<Texture2D>("Load");
            quitTexture = Content.Load<Texture2D>("Quit");
            tryagainTexture = Content.Load<Texture2D>("Untitled_Artwork (2)");

            //button objects
            // play button
            play = new Button(
                playTexture, 
                new Rectangle(
                    300, 
                    500, 
                    playTexture.Width, 
                    playTexture.Height));
            
            // quit button
            quit = new Button(
                quitTexture, 
                new Rectangle(
                    600, 
                    500, 
                    quitTexture.Width, 
                    quitTexture.Height));
            
            // replay button
            tryagain = new Button(
                tryagainTexture, 
                new Rectangle(400, 400, 200, 100));

            //placeholder font
            font = Content.Load<SpriteFont>("File");

            //all sprites
            #region BigPotions
            // BLUE POTIONS ---------------------------------------------------
            bigB1 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB1");
            bigB2 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB2");
            bigB3 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB3");
            bigB4 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB4");
            bigB5 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB5");
            bigB6 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB6");
            bigB7 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB7");
            bigB8 = Content.Load<Texture2D>("Big Potion/Big potion blue/bigB8");

            // RED POTIONS ----------------------------------------------------
            bigR1 = Content.Load<Texture2D>("Big Potion/Red big/bigR1");
            bigR2 = Content.Load<Texture2D>("Big Potion/Red big/bigR2");
            bigR3 = Content.Load<Texture2D>("Big Potion/Red big/bigR3");
            bigR4 = Content.Load<Texture2D>("Big Potion/Red big/bigR4");
            bigR5 = Content.Load<Texture2D>("Big Potion/Red big/bigR5");
            bigR6 = Content.Load<Texture2D>("Big Potion/Red big/bigR6");
            bigR7 = Content.Load<Texture2D>("Big Potion/Red big/bigR7");
            bigR8 = Content.Load<Texture2D>("Big Potion/Red big/bigR8");

            // YELLOW POTIONS -------------------------------------------------
            bigY1 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY1");
            bigY2 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY2");
            bigY3 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY3");
            bigY4 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY4");
            bigY5 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY5");
            bigY6 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY6");
            bigY7 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY7");
            bigY8 = Content.Load<Texture2D>("Big Potion/Yellowbig/bigY8");
            #endregion

            #region SmallVials
            //EMPTY VIALS ------------------------------------------------------------
            smallEmpty1 = Content.Load<Texture2D>("Small vial/Small half/Emptysmall/smallEmpty01");
            smallEmpty2 = Content.Load<Texture2D>("Small vial/Small half/Emptysmall/smallEmpty02");
            smallEmpty3 = Content.Load<Texture2D>("Small vial/Small half/Emptysmall/smallEmpty03");
            smallEmpty4 = Content.Load<Texture2D>("Small vial/Small half/Emptysmall/smallEmpty04");
            smallEmpty5 = Content.Load<Texture2D>("Small vial/Small half/Emptysmall/smallEmpty05");
            smallEmpty6 = Content.Load<Texture2D>("Small vial/Small half/Emptysmall/smallEmpty06");
            smallEmpty7 = Content.Load<Texture2D>("Small vial/Small half/Emptysmall/smallEmpty07");

            //BLUE VIALS -------------------------------------------------------------
            //full
            smallFullB0 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULB0");
            smallFullB1 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULLB1");
            smallFullB2 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULLB2");
            smallFullB3 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULLB3");
            smallFullB4 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULLB4");
            smallFullB5 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULLB5");
            smallFullB6 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULLB6");
            smallFullB7 = Content.Load<Texture2D>("Small vial/Small full/Blue small/smallFULLB7");

            //half
            smallHalfB1 = Content.Load<Texture2D>("Small vial/Small half/Blue half/smallHALFB1");
            smallHalfB2 = Content.Load<Texture2D>("Small vial/Small half/Blue half/smallHALFB2");
            smallHalfB3 = Content.Load<Texture2D>("Small vial/Small half/Blue half/smallHALFB3");
            smallHalfB4 = Content.Load<Texture2D>("Small vial/Small half/Blue half/smallHALFB4");
            smallHalfB5 = Content.Load<Texture2D>("Small vial/Small half/Blue half/smallHALFB5");
            smallHalfB6 = Content.Load<Texture2D>("Small vial/Small half/Blue half/smallHALFB6");
            smallHalfB7 = Content.Load<Texture2D>("Small vial/Small half/Blue half/smallHALFB7");

            //RED VIALS ---------------------------------------------------------------
            //full
            smallFullR1 = Content.Load<Texture2D>("Small vial/Small full/Red small/smallFULLR1");
            smallFullR2 = Content.Load<Texture2D>("Small vial/Small full/Red small/smallFULLR2");
            smallFullR3 = Content.Load<Texture2D>("Small vial/Small full/Red small/smallFULLR3");
            smallFullR4 = Content.Load<Texture2D>("Small vial/Small full/Red small/smallFULLR4");
            smallFullR5 = Content.Load<Texture2D>("Small vial/Small full/Red small/smallFULLR5");
            smallFullR6 = Content.Load<Texture2D>("Small vial/Small full/Red small/smallFULLR6");
            smallFullR7 = Content.Load<Texture2D>("Small vial/Small full/Red small/smallFULLR7");

            //half
            smallHalfR1 = Content.Load<Texture2D>("Small vial/Small half/Red half/smallHALFR");
            smallHalfR2 = Content.Load<Texture2D>("Small vial/Small half/Red half/smallHALFR2");
            smallHalfR3 = Content.Load<Texture2D>("Small vial/Small half/Red half/smallHALFR3");
            smallHalfR4 = Content.Load<Texture2D>("Small vial/Small half/Red half/smallHALFR4");
            smallHalfR5 = Content.Load<Texture2D>("Small vial/Small half/Red half/smallHALFR5");
            smallHalfR6 = Content.Load<Texture2D>("Small vial/Small half/Red half/smallHALFR6");
            smallHalfR7 = Content.Load<Texture2D>("Small vial/Small half/Red half/smallHALFR7");

            //YELLOW VIALS -------------------------------------------------------------
            //full
            smallFullY1 = Content.Load<Texture2D>("Small vial/Small full/Yellow small/smallFULLY1");
            smallFullY2 = Content.Load<Texture2D>("Small vial/Small full/Yellow small/smallFULLY2");
            smallFullY3 = Content.Load<Texture2D>("Small vial/Small full/Yellow small/smallFULLY3");
            smallFullY4 = Content.Load<Texture2D>("Small vial/Small full/Yellow small/smallFULLY4");
            smallFullY5 = Content.Load<Texture2D>("Small vial/Small full/Yellow small/smallFULLY5");
            smallFullY6 = Content.Load<Texture2D>("Small vial/Small full/Yellow small/smallFULLY6");
            smallFullY7 = Content.Load<Texture2D>("Small vial/Small full/Yellow small/smallFULLY7");

            //half
            smallHalfY1 = Content.Load<Texture2D>("Small vial/Small half/Yellow half/smallHALFY");
            smallHalfY2 = Content.Load<Texture2D>("Small vial/Small half/Yellow half/smallHALFY2");
            smallHalfY3 = Content.Load<Texture2D>("Small vial/Small half/Yellow half/smallHALFY3");
            smallHalfY4 = Content.Load<Texture2D>("Small vial/Small half/Yellow half/smallHALFY4");
            smallHalfY5 = Content.Load<Texture2D>("Small vial/Small half/Yellow half/smallHALFY5");
            smallHalfY6 = Content.Load<Texture2D>("Small vial/Small half/Yellow half/smallHALFY6");
            smallHalfY7 = Content.Load<Texture2D>("Small vial/Small half/Yellow half/smallHALFY7");

            //GREEN VIALS -------------------------------------------------------------
            smallFullG0 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG0");
            smallFullG1 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG1");
            smallFullG2 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG2");
            smallFullG3 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG3");
            smallFullG4 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG4");
            smallFullG5 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG5");
            smallFullG6 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG6");
            smallFullG7 = Content.Load<Texture2D>("Small vial/Small full/Green small/smallFULLG7");

            //ORANGE VIALS -------------------------------------------------------------
            smallFullO0 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO0");
            smallFullO1 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO1");
            smallFullO2 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO2");
            smallFullO3 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO3");
            smallFullO4 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO4");
            smallFullO5 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO5");
            smallFullO6 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO6");
            smallFullO7 = Content.Load<Texture2D>("Small vial/Small full/Orange small/smallFULLO7");

            //PURPLE VIALS -------------------------------------------------------------
            smallFullP0 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP0");
            smallFullP1 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP1");
            smallFullP2 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP2");
            smallFullP3 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP3");
            smallFullP4 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP4");
            smallFullP5 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP5");
            smallFullP6 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP6");
            smallFullP7 = Content.Load<Texture2D>("Small vial/Small full/Purple small/smallFULLP7");
            #endregion
            
            // add an empty vial into the inventory
            inventory.Add(new Small(
                Capacity.Empty, 
                InkColor.Clear, 
                smallEmpty6, 
                new Vector2(0, 0)));

            #region Dinos
            blueDinoSpriteSheet = Content.Load<Texture2D>("Dinos/DinoSpriteBlue");
            redDinoSpriteSheet = Content.Load<Texture2D>("Dinos/DinoSpriteRed");
            yellowDinoSpriteSheet = Content.Load<Texture2D>("Dinos/DinoSpriteYellow");
            #endregion

            terrain = Content.Load<Texture2D>("Terrain");

            //player
            playerTexture = yellowDinoSpriteSheet;
            numPlayerSprites = 24;
            widthOfPlayerSprite = playerTexture.Width / numPlayerSprites;
            player =
                new Player(
                    playerTexture,
                    (new Vector2(0, 0)),
                    (new Rectangle(playerCurrentFrame * widthOfPlayerSprite + 2 * widthOfPlayerSprite,   // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfPlayerSprite,                        // - Width 
                    yellowDinoSpriteSheet.Height)));

            // Set up animation data:
            fps = 8.0;                      // Animation frames to cycle through per second
            secondsPerFrame = 1.0 / fps;    // How long each animation frame lasts
            timeCounter = 0;                // Time passed since animation
            playerCurrentFrame = 1;         // Sprite sheet's first animation frame is 1 (not 0)

            LoadLevel("tutorial.level");

            //song loading
            gameSong = Content.Load<Song>("Sounds/game normal");

            MediaPlayer.Play(gameSong);
            MediaPlayer.IsRepeating = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //make the window in focus
            this.IsMouseVisible = true;

            //Update FSM yeet
            switch (screenState)
            {
                // MENU SCREEN ----------------------------------------------------------
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
                
                // GAME SCREEN ----------------------------------------------------------
                case GameState.Game:
                    
                    //decrease the timer
                    timer-=gameTime.ElapsedGameTime.TotalSeconds;
                    
                    player.Update(gameTime);
                    ResolveCollisions();

                    //check if collectibles are collected
                    for (int i = 0; i < collectibles.Count; i++)
                    {
                        //if collectible is hit, remove from list
                        if (collectibles[i].CheckCollision(player))
                        {
                            // determine if it is a BIG vial
                            if (collectibles[i] is Big)
                            {
                                Big b = (Big)collectibles[i];
                                MixingLogic(b);
                            }
                            collectibles.RemoveAt(i);
                            i--;
                        }
                    }

                    //if timer runs out --> lose
                    if (timer <= 0)
                    {
                        screenState=GameState.GameLose;
                    }

                    //if the player meets win conditions win
                    if (CheckWin())
                    {
                        screenState = GameState.GameWin;
                    }

                    break;
                
                // LOADING SCREEN -------------------------------------------------------
                case GameState.Load: 
                    //leah code here
                    screenState = GameState.Game;

                    break;
                
                // GAME LOSE SCREEN -----------------------------------------------------
                case GameState.GameLose:
                    if (quit.MouseClick() && quit.MousePosition())
                    {
                        Environment.Exit(0);
                    }
                    if (tryagain.MouseClick() && tryagain.MousePosition())
                    {
                        Reset();
                        screenState = GameState.Menu;
                    }
                    break;
                
                // GAME WIN SCREEN ------------------------------------------------------
                case GameState.GameWin:
                    
                    // if player wants to quit the game
                    if (quit.MouseClick() && quit.MousePosition())
                    {
                        // exit the program
                        Environment.Exit(0);
                    }

                    //if player wants to play again
                    if (tryagain.MouseClick() && tryagain.MousePosition())
                    {
                        // reset the game
                        Reset();   
                        
                        //go to game menu
                        screenState = GameState.Menu;
                    }
                    break;
            }

            UpdateAnimation(gameTime);

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
                // GAME MENU ------------------------------------------------------------
                case GameState.Menu:

                    //background image
                    _spriteBatch.Draw(
                        bgimg, 
                        new Rectangle(0,0, 1024, 768), 
                        Color.White);
                    
                    //draw buttons
                    play.Draw(_spriteBatch);
                    quit.Draw(_spriteBatch);

                    //title logo
                    _spriteBatch.Draw(
                        title, 
                        new Rectangle(614/2, 268/2, 400, 200), 
                        Color.White);
                    break;
                
                // GAME PLAY SCREEN -----------------------------------------------------
                case GameState.Game:

                    // background image
                    _spriteBatch.Draw(
                        skyimg, 
                        new Rectangle(0, 0, 1024, 768), 
                        Color.White);

                    //draw each tile
                    foreach (Tile t in tileObjects)
                    {
                        t.Draw(_spriteBatch);
                    }

                    //draw collectibles
                    foreach (Collectible c in collectibles)
                    {
                        c.Draw(_spriteBatch);
                    }

                    //determine what state to draw the player in
                    switch (player.PlayerState)
                    {
                        case PlayerState.StandLeft:
                            DrawPlayerStanding(SpriteEffects.FlipHorizontally);
                            break;
                        case PlayerState.StandRight:
                            DrawPlayerStanding(SpriteEffects.None);
                            break;
                        case PlayerState.JumpLeft:
                            DrawPlayerWalking(SpriteEffects.FlipHorizontally);
                            break;
                        case PlayerState.JumpRight:
                            DrawPlayerWalking(SpriteEffects.None);
                            break;
                        case PlayerState.WalkLeft:
                            DrawPlayerWalking(SpriteEffects.FlipHorizontally);
                            break;
                        case PlayerState.WalkRight:
                            DrawPlayerWalking(SpriteEffects.None);
                            break;
                    }

                    //timer
                    _spriteBatch.DrawString(
                        font, 
                        String.Format("{0:0.00}", timer), 
                        (new Vector2(50, 50)), 
                        Color.Brown);

                    //objective
                    _spriteBatch.DrawString(
                        font, 
                        String.Format("OBJECTIVE:"), 
                        (new Vector2(50, 150)), 
                        Color.Brown);
                    _spriteBatch.DrawString(
                        font, 
                        String.Format("Get RED and BLUE to make PURPLE"), 
                        (new Vector2(50, 200)), 
                        Color.Brown);

                    //inventory
                    int count = 1;
                    foreach (Small vial in inventory)
                    {
                        _spriteBatch.DrawString(font, 
                            String.Format(
                                "vial {0}: {1} {2}", 
                                count, vial.Capacity, vial.Color), 
                            (new Vector2 (50, 100 * count)), 
                            Color.Brown);
                        count++;
                    }

                    //full inventory
                    foreach (Small vial in fullInventory)
                    {
                        _spriteBatch.DrawString(font, 
                            String.Format(
                                "vial {0}: {1} {2}", 
                                count, vial.Capacity, vial.Color),
                            (new Vector2(50, 100 * count)), 
                            Color.White);
                        count++;
                    }
                    break;
                
                // LOADING SCREEN -------------------------------------------------------
                case GameState.Load:
                    _spriteBatch.DrawString(
                        font, 
                        "Load", 
                        new Vector2(0, 0), 
                        Color.Brown);
                    break;
                
                // GAME LOSE SCREEN -----------------------------------------------------
                case GameState.GameLose:
                    
                    // background image
                    _spriteBatch.Draw(
                        skyimg, 
                        new Rectangle(0, 0, 1024, 768), 
                        Color.White);
                    
                    // loss text
                    _spriteBatch.DrawString(
                        font, 
                        "You lost!", 
                        new Vector2(450, 350), 
                        Color.Brown);
                    
                    // replay button
                    tryagain.Draw(_spriteBatch);
                    break;
                
                // GAME WIN SCREEN ------------------------------------------------------
                case GameState.GameWin:
                    
                    // background image
                    _spriteBatch.Draw(
                        skyimg, 
                        new Rectangle(0, 0, 1024, 768), 
                        Color.White);

                    // win text
                    _spriteBatch.DrawString(
                        font, 
                        "You won!", 
                        new Vector2(450, 350), Color.Brown);

                    // quit button
                    quit.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadLevel(string levelFile)
        {
            //read in the file
            reader = new StreamReader("Content/Levels/" + levelFile);

            //read each line of data in the file
            string lineOfData = "";

            while ((lineOfData = reader.ReadLine()) != null)
            {
                //store the split data into an array
                string[] objectData = lineOfData.Split(",");

                //determine what kind of game object it is
                if (objectData[2] == "belowGrass")
                {
                    Tile obj = new Tile(terrain,
                        new Vector2(
                            int.Parse(objectData[1]) * 32,
                                    int.Parse(objectData[0]) * 32),
                        new Rectangle(
                            32,
                            32,
                            32,
                            32));

                    //place each object into game object list
                    tileObjects.Add(obj);
                }
                else if (objectData[2] == "grass")
                {
                    Tile obj = new Tile(terrain,
                        new Vector2(
                            int.Parse(objectData[1]) * 32,
                                    int.Parse(objectData[0]) * 32),
                        new Rectangle(
                            32,
                            0,
                            32,
                            32));

                    //place each object into game object list
                    tileObjects.Add(obj);
                }
                else if (objectData[2] == "stoneRight")
                {
                    Tile obj = new Tile(terrain,
                        new Vector2(int.Parse(objectData[1]) * 32, int.Parse(objectData[0]) * 32),
                        new Rectangle(
                            64,
                            32,
                            32,
                            32));

                    //place each object into game object list
                    tileObjects.Add(obj);
                }
                else if (objectData[2] == "stoneLeft")
                {
                    Tile obj = new Tile(terrain,
                        new Vector2(int.Parse(objectData[1]) * 32, int.Parse(objectData[0]) * 32),
                        new Rectangle(
                            0,
                            32,
                            32,
                            32));

                    //place each object into game object list
                    tileObjects.Add(obj);
                }
                else if (objectData[2] == "platform")
                {
                    Tile obj = new Tile(terrain,
                        new Vector2(int.Parse(objectData[1]) * 32, int.Parse(objectData[0]) * 32),
                        new Rectangle(
                            0,
                            128,
                            96,
                            32));

                    //place each object into game object list
                    tileObjects.Add(obj);
                }
                else if (objectData[2] == "pink")
                {
                    player.X = int.Parse(objectData[1]) * 32;
                    player.Y = int.Parse(objectData[0]) * 32;
                }
                else if (objectData[2] == "blue")
                {
                    Big obj =
                            new Big(
                                InkColor.Blue,
                                bigB1,
                                new Vector2(int.Parse(objectData[1]) * 32, int.Parse(objectData[0]) * 32));

                    //place each object into game object list
                    collectibles.Add(obj);
                }
                else if (objectData[2] == "red")
                {
                    Big obj =
                            new Big(
                                InkColor.Red,
                                bigR1,
                                new Vector2(int.Parse(objectData[1]) * 32, int.Parse(objectData[0]) * 32));

                    //place each object into game object list
                    collectibles.Add(obj);
                }
                else if (objectData[2] == "yellow")
                {
                    Big obj =
                            new Big(
                                InkColor.Yellow,
                                bigY1,
                                new Vector2(int.Parse(objectData[1]) * 32, int.Parse(objectData[0]) * 32));

                    //place each object into game object list
                    collectibles.Add(obj);
                }
                else if (objectData[2] == "white")
                {
                    Small obj =
                            new Small(Capacity.Empty,
                                InkColor.Clear,
                                smallEmpty1,
                                new Vector2(int.Parse(objectData[1]) * 32, int.Parse(objectData[0]) * 32));

                    //place each object into game object list
                    collectibles.Add(obj);
                } 
            }
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            // ElapsedGameTime is the duration of the last GAME frame
            timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

            // Has enough time passed to flip to the next frame?
            if (timeCounter >= secondsPerFrame)
            {
                // Change which frame is active, ensuring the frame is reset back to the first 
                playerCurrentFrame++;
                if (playerCurrentFrame >= 7)
                {
                    playerCurrentFrame = 1;
                }

                // Reset the time counter
                timeCounter -= secondsPerFrame;
            }
        }

        private void DrawPlayerWalking(SpriteEffects flip)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            // Mario is animated with this method.
            // He is drawn starting at the second animation frame in the sprite sheet 
            //   and cycles through animation frames 1, 2, and 3.
            //   (i.e. the second through fourth images in the sheet)
            _spriteBatch.Draw(
                player.PlayerTexture,                                   // Whole sprite sheet
                player.Position,                                  // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    playerCurrentFrame * widthOfPlayerSprite + 2*widthOfPlayerSprite,   // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfPlayerSprite,                        // - Width 
                    yellowDinoSpriteSheet.Height),              // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        private void DrawPlayerStanding(SpriteEffects flip)
        {
            // This version of draw can flip (mirror) the image horizontally or vertically,
            // depending on the method's SpriteEffects parameter.

            // Mario is animated with this method.
            // He is drawn starting at the second animation frame in the sprite sheet 
            //   and cycles through animation frames 1, 2, and 3.
            //   (i.e. the second through fourth images in the sheet)
            _spriteBatch.Draw(
                player.PlayerTexture,                                   // Whole sprite sheet
                player.Position,                                  // Position of the Mario sprite
                new Rectangle(                                  // Which portion of the sheet is drawn:
                    (playerCurrentFrame%3) * widthOfPlayerSprite,   // - Left edge
                    0,                                          // - Top of sprite sheet
                    widthOfPlayerSprite,                        // - Width 
                    yellowDinoSpriteSheet.Height),              // - Height
                Color.White,                                    // No change in color
                0.0f,                                           // No rotation
                Vector2.Zero,                                   // Start origin at (0, 0) of sprite sheet 
                1.0f,                                           // Scale
                flip,                                           // Flip it horizontally or vertically?    
                0.0f);                                          // Layer depth
        }

        /// <summary>
        /// checking the win condition for the COLOR ONLY
        /// </summary>
        /// <returns></returns>
        public bool CheckWin()
        {
            //if the collectible matches the objective return true
            foreach (Small s in inventory)
            {
                foreach (InkColor o in objectives)
                {
                    if (s.Color == o)
                    {
                        objectiveCounter++;
                        return true;
                    }
                }
            }
            return false;
        }

        public void MixingLogic(Big collect)
        {
                if (inventory[0].Capacity == Capacity.Half)
                {
                    switch (inventory[0].Color)
                    {
                        case InkColor.Red:
                            if (collect.Color == InkColor.Red)
                            {
                                inventory[0].Color = InkColor.Red;
                            }
                            else if (collect.Color == InkColor.Blue)
                            {
                                inventory[0].Color = InkColor.Purple;
                            }
                            else if (collect.Color == InkColor.Yellow)
                            {
                                inventory[0].Color = InkColor.Orange;
                            }
                            break;
                        case InkColor.Yellow:
                            if (collect.Color == InkColor.Red)
                            {
                                inventory[0].Color = InkColor.Orange;
                            }
                            else if (collect.Color == InkColor.Blue)
                            {
                                inventory[0].Color = InkColor.Green;
                            }
                            else if (collect.Color == InkColor.Yellow)
                            {
                                inventory[0].Color = InkColor.Yellow;
                            }
                            break;
                        case InkColor.Blue:
                            if (collect.Color == InkColor.Red)
                            {
                                inventory[0].Color = InkColor.Purple;
                            }
                            else if (collect.Color == InkColor.Blue)
                            {
                                inventory[0].Color = InkColor.Blue;
                            }
                            else if (collect.Color == InkColor.Yellow)
                            {
                                inventory[0].Color = InkColor.Green;
                            }
                            break;
                    }
                    inventory[0].Capacity = Capacity.Full;
                }
                else if (inventory[0].Capacity == Capacity.Empty)
                {
                    switch (collect.Color)
                    {
                        case InkColor.Red:
                            inventory[0].Color = InkColor.Red;
                            break;
                        case InkColor.Yellow:
                            inventory[0].Color = InkColor.Yellow;
                            break;
                        case InkColor.Blue:
                            inventory[0].Color = InkColor.Blue;
                            break;
                    }
                    inventory[0].Capacity = Capacity.Half;
                }

               
        }

        private void ResolveCollisions()
        {
            // PRACTICE EXERCISE: Finish this method!

            //get player rectangle to check collisions
            Rectangle playerRect = player.GetObjectRect();

            //check for intersections
            List<Tile> intersectionsList = new List<Tile>();

            foreach (Tile t in tileObjects)
            {
                if (t.GetObjectRect().Intersects(playerRect))
                {
                    intersectionsList.Add(t);
                }
            }

            foreach (Tile t in intersectionsList)
            {
                //get how the rectangles are overlapped
                Rectangle overlap = Rectangle.Intersect(playerRect, t.GetObjectRect());

                //if it's taller than it is wide - Horizontal
                if (overlap.Height >= overlap.Width)
                {
                    //if player is left
                    if (t.GetObjectRect().X > playerRect.X)
                    {
                        //move player left
                        playerRect.X -= overlap.Width;
                    }
                    //if player is right
                    else
                    {
                        //move player right
                        playerRect.X += overlap.Width;
                    }
                }

                //if it's wider than it is tall - Vertical
                if (overlap.Height < overlap.Width)
                {
                    //if player is up
                    if (t.GetObjectRect().Y > playerRect.Y)
                    {
                        //move player up
                        playerRect.Y -= overlap.Height;
                    }
                    //if player is down
                    else
                    {
                        //move player down
                        playerRect.Y += overlap.Height;
                    }

                    //player's y-velocity is set to 0
                    player.PlayerVelocity = new Vector2(player.PlayerVelocity.X, 0);
                }

                //update players actual position, copy data over
                player.X = playerRect.X;
                player.Y = playerRect.Y;
            }
        }

        public void Reset()
        {
            timer = 10;

            inventory.Clear();
            fullInventory.Clear();

            inventory.Add(new Small(Capacity.Empty, InkColor.Clear, smallEmpty6, new Vector2(0, 0)));

            LoadLevel("tutorial.level");

            player.X= 0;
            player.Y= 0;
        }
    }
}