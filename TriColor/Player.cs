 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region properties

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

        public Texture2D Texture
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

            //press A to go left
            if (currKB.IsKeyDown(Keys.A))
            {
                position.X -= playerSpeedX;
            }
            //press D to go right
            if (currKB.IsKeyDown(Keys.D))
            {
                position.X += playerSpeedX;
            }
            //press space to jump
            if (SingleKeyPress(Keys.Space))
            {
                playerVelocity.Y = jumpVelocity.Y;
            }

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

        public override void Update(GameTime gameTime)
        {
            // Handle input, apply gravity and then deal with collisions
            ProcessInput();
            ApplyGravity();
        }

        
        #endregion
    }
}

