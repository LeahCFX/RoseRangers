using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Yeehaw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriColor
{
    internal class Item
    {
        //fields
        Texture2D texture;
        Rectangle rectangle;
        Capacity vialCapacity;
        InkColor vialColor;

        /// <summary>
        /// make it able to change the sprite
        /// </summary>
        public Texture2D Texture
        {
            set
            {
                texture = value;
            }
        }

        /// <summary>
        /// allows user to read and change vileCapacity
        /// </summary>
        public Capacity Capacity
        {
            get
            {
                return vialCapacity;
            }
            set
            {
                vialCapacity = value;
            }
        }

        /// <summary>
        /// allows user to read and change vileColor
        /// </summary>
        public InkColor VileColor
        {
            get
            {
                return vialColor;
            }
            set
            {
                vialColor = value;
            }
        }

        /// <summary>
        /// initialize
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="rectangle"></param>
        public Item(Texture2D texture, Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            vialCapacity = Capacity.Empty;
        }

        /// <summary>
        /// draws the vile to da screen
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, rectangle, Color.White);
        }


    }
}
