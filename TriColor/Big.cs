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
    internal class Big : Collectible
    {
        //fields
        private InkColor color;

        /// <summary>
        /// instantiating the big paint viles
        /// </summary>
        /// <param name="color"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        public Big(InkColor color, Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.color = color;
        }
    }
}
