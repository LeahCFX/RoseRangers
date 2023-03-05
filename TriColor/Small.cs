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
    public class Small : Collectible

    {
        //field
        Capacity capacity;
        InkColor color;

        //properties
        public Capacity Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
            }
        }

        public InkColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// instantiates the sprite of the small collectible
        /// </summary>
        /// <param name="color"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        public Small(Capacity capacity, InkColor color, Texture2D texture, Vector2 position) : 
            base(texture, position)
        {
            this.capacity = capacity;
            this.color = color;
        }

        /// <summary>
        /// litterally just changes the state of the full oh mygvosd
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public override bool CheckCollision(GameObject check)
        {
            if (check.GetObjectRect().Intersects(this.GetObjectRect()) && capacity == Capacity.Empty)
            {
                //probably put the collision sound here
                active = false;
                capacity = Capacity.Half;
                return true;
            }
            else if (check.GetObjectRect().Intersects(this.GetObjectRect()) && capacity == Capacity.Half)
            {
                active = false;
                capacity = Capacity.Full;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
