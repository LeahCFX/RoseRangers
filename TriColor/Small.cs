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
    internal class Small : Collectible

    {
        /// <summary>
        /// instantiates the sprite of the small collectible
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="rectangle"></param>
        public Small(Texture2D texture, Rectangle rectangle) : base(texture, rectangle)
        {}
    }
}
