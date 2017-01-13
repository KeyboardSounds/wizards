using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards
{
    class ProjectileManager
    {
        private static ProjectileManager instance = null;
        int nextid = 0;
        Dictionary<int, Projectile> activeProjectiles = new Dictionary<int, Projectile>();

        // Private constructor for singleton
        private ProjectileManager()
        {

        }

        public static ProjectileManager getInstance()
        {
            if(instance == null)
            {
                instance = new ProjectileManager();
            }
            return instance;
        }

        public void addProjectile(Projectile p)
        {
            activeProjectiles.Add(nextid, p);
            nextid++;
        }

        public void UpdateProjectiles(GameTime gameTime, Viewport viewport)
        {
            foreach (Projectile p in activeProjectiles.Values)
            {
                p.Update(gameTime, viewport);
            }
        }

        public void handleProjectileDestroyed(int id)
        {
            activeProjectiles.Remove(id);
        }

        public void DrawProjectiles(SpriteBatch spriteBatch)
        {
            foreach (Projectile p in activeProjectiles.Values)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}
