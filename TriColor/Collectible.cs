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
    internal class Collectible : GameObject
    {
        // fields
        protected bool active;

        //properties

        /// <summary>
        /// lets program know wheter or not the collectible should be on screen
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        

        //constructor
        /// <summary>
        /// creates an instance of collectible
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        public Collectible(Texture2D texture, Vector2 position) :
            base(texture, position)
        {
            active = true;
        }

        //methods
        /// <summary>
        /// Checks whether or not two items are colliding
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public virtual bool CheckCollision(GameObject check)
        {
            if (check.GetObjectRect().Intersects(this.GetObjectRect()))
            {
                //probably put the collision sound here
                active = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// only draw if active
        /// </summary>
        /// <param name="sb"></param>
        public override void Draw(SpriteBatch sb)
        {
            if (active)
            {
                base.Draw(sb);
            }
        }
    }
}
