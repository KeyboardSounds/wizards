using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wizards
{
    class Projectile : Entity
    {
        public int damage;
        public int id = -1;
        private ProjectileManager projectileManager;
        private Velocity velocity;

        public Projectile(Point p, string tfn, int fw, int h, int nf, int d, Velocity v, Texture2D t, ProjectileManager projManager) : base(p, tfn, fw, h, nf)
        {
            projectileManager = projManager;
            damage = d;
            velocity = v;
            tex = t;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                        tex,
                        GetDestinationRectangle(),
                        GetSourceRectangle(),
                        Color.White);
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            Vector2 move = velocity.direction * velocity.speed; // how far we should move

            loc.X += (int)move.X;
            loc.Y += (int)move.Y;

            if(!viewport.Bounds.Contains(loc))
            {
                // our projectile is outside the viewing area. destroy it
                Die();
            }
        }

        public void Die()
        {
            projectileManager.handleProjectileDestroyed(id);
        }
    }
}
