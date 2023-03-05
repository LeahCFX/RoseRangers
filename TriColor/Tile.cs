using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Yeehaw
{
    internal class Tile : GameObject
    {
        //fields
        private Rectangle frame;

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

        #endregion

        /// <summary>
        /// initializing fields
        /// </summary>
        /// <param name="texture"> texture of the sprite </param>
        /// <param name="position"> </param>
        /// <param name="frame"></param>
        public Tile(Texture2D texture, Vector2 position, Rectangle frame) :
            base(texture, position)
        {
            this.frame = frame;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, frame, Color.White);
        }
    }
}
