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
    internal class GameObject
    {
        // fields ----------------------------------------------------------
        protected Texture2D texture;
        protected Vector2 position;
        // -----------------------------------------------------------------

        #region properties

        /// <summary>
        /// get and set x position
        /// </summary>
        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }

        /// <summary>
        /// get and set y position
        /// </summary>
        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        /// <summary>
        /// returns position vector
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// returns texture image
        /// </summary>
        public Texture Texture
        {
            get
            {
                return texture;
            }
        }
        #endregion

        //constructor ------------------------------------------------------
        /// <summary>
        /// creates an instance of game object
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }
        //------------------------------------------------------------------

        //methods ----------------------------------------------------------
        /// <summary>
        /// overridden in child classes
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// calls basic draw method on this object
        /// </summary>
        /// <param name="sb"></param>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// creates a rectangle when needed
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle GetObjectRect()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                texture.Width,
                texture.Height);
        }
        // -----------------------------------------------------------------
    }
}
