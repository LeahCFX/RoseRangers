using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Yeehaw
{
    internal class Player : GameObject
    {
        //fields
        private float playerSpeedX;
        private Vector2 playerVelocity;
        private Vector2 jumpVelocity;
        private Vector2 gravity;
        private Rectangle frame;
        private KeyboardState currKB;
        private KeyboardState prevKB;
        private PlayerState playerState;

        #region properties
        public Vector2 PlayerVelocity
        {
            get
            {
                return playerVelocity;
            }
            set
            {
                playerVelocity = value;
            }
        }
        public PlayerState PlayerState
        {
            get
            {
                return playerState;
            }
        }

        /// <summary>
        /// gets the portion of sprite sheet needed for sprite
        /// </summary>
        public Rectangle Frame
        {
            get
            {
                return frame;
            }
        }

        /// <summary>
        /// x position of frame on sprite sheet
        /// </summary>
        public int FrameX
        {
            get
            {
                return frame.X;
            }
            set
            {
                frame.X = value;
            }
        }

        /// <summary>
        /// y position of of frame on sprite sheet
        /// </summary>
        public int FrameY
        {
            get
            {
                return frame.Y;
            }
            set
            {
                frame.Y = value;
            }
        }

        /// <summary>
        /// width of animation frame
        /// </summary>
        public int FrameWidth
        {
            get
            {
                return frame.Width;
            }
        }

        /// <summary>
        /// height of animation frame
        /// </summary>
        public int FrameHeight
        {
            get
            {
                return frame.Height;
            }
        }

        public Texture2D PlayerTexture
        {
            get
            {
                return texture;
            }
        }

        #endregion

        //constructor
        /// <summary>
        /// creates an instance of player
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        public Player(Texture2D texture, Vector2 position, Rectangle frame) :
            base(texture, position)
        {
            playerVelocity = Vector2.Zero;
            jumpVelocity = new Vector2(0, -15.0f);
            gravity = new Vector2(0, 0.5f);
            this.frame = frame;
            playerState = PlayerState.StandRight;

            playerSpeedX = 5.0f;
        }

        #region methods
        /// <summary>
		/// Determines if a key was initially pressed this frame
		/// </summary>
		/// <param name="key">The key to check</param>
		/// <returns>True if this is the first frame the key is pressed, false otherwise</returns>
		private bool SingleKeyPress(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && prevKB.IsKeyUp(key);
        }

        /// <summary>
        /// Handles movement for sidescrolling game with gravity
        /// </summary>
        private void ProcessInput()
        {
            //get current kbState
            currKB = Keyboard.GetState();

            //---------------------------------------------------------------
            switch (playerState)
            {
                //===================================================================
                case PlayerState.StandLeft:
                    //if A is down ----------------------------------
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                        position.X -= playerSpeedX;
                    }
                    //if D is down ----------------------------------
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                        position.X += playerSpeedX;
                    }
                    if (SingleKeyPress(Keys.W) && playerVelocity.Y == 0)
                    {
                        playerState = PlayerState.JumpLeft;
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    break;
                //===================================================================
                case PlayerState.WalkLeft:
                    //if A is down ----------------------------------
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                        position.X -= playerSpeedX;
                    }
                    //if A is up ------------------------------------
                    else if (Keyboard.GetState().IsKeyUp(Keys.A))
                    {
                        playerState = PlayerState.StandLeft;
                    }
                    if (SingleKeyPress(Keys.W) && playerVelocity.Y == 0)
                    {
                        playerState = PlayerState.JumpLeft;
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    break;
                case PlayerState.WalkRight:
                    //if A is down ----------------------------------
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                        position.X += playerSpeedX;
                    }
                    //if A is up ------------------------------------
                    else if (Keyboard.GetState().IsKeyUp(Keys.D))
                    {
                        playerState = PlayerState.StandRight;
                    }
                    if (SingleKeyPress(Keys.W) && playerVelocity.Y == 0)
                    {
                        playerState = PlayerState.JumpLeft;
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    break;
                //===================================================================
                case PlayerState.StandRight:
                    //if A is down ----------------------------------
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                        position.X -= playerSpeedX;
                    }
                    //if D is down ----------------------------------
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                        position.X += playerSpeedX;
                    }
                    if (SingleKeyPress(Keys.W) && playerVelocity.Y == 0)
                    {
                        playerState = PlayerState.JumpRight;
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    break;
                //===================================================================
                case PlayerState.JumpRight:
                    //if D is down ----------------------------------
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        playerState = PlayerState.WalkRight;
                        position.X += playerSpeedX;
                    }
                    //if D is up ------------------------------------
                    else if (Keyboard.GetState().IsKeyUp(Keys.D))
                    {
                        playerState = PlayerState.StandRight;
                    }
                    break;
                //===================================================================
                case PlayerState.JumpLeft:
                    //if A is down ----------------------------------
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        playerState = PlayerState.WalkLeft;
                        position.X -= playerSpeedX;
                    }
                    //if A is up ------------------------------------
                    else if (Keyboard.GetState().IsKeyUp(Keys.A))
                    {
                        playerState = PlayerState.StandLeft;
                    }
                    break;
            }
            //---------------------------------------------------------------


            //update prevKB
            prevKB = currKB;
        }

        /// <summary>
        /// Applies gravity to the player
        /// </summary>
        private void ApplyGravity()
        {
            //add velocity and gravity
            playerVelocity += gravity;

            //add position and velocity
            position += playerVelocity;

            // ~*~ P  H  Y  S  I  C  S ~*~
        }

        public override Rectangle GetObjectRect()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                24,
                texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            // Handle input, apply gravity and then deal with collisions
            ProcessInput();
            ApplyGravity();
        }

        
        #endregion
    }
}

