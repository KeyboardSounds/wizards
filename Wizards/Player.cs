using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Wizards
{
    class Player : Entity
    {
        public Point destination;
        public bool moving = false;
        private int lastdisttogolen = int.MaxValue;
        private MouseState lastMouseState;
        KeyboardState oldState;
        public string projectileTextureFilename = "fireball";
        ProjectileManager projectileManager;
        private int projectileDamage = 1;
        private int projectileNumFrames = 1;
        private int projectileTextureFrameWidth = 40;
        private int projectileHeight = 40;
        private int projectileSpeed = 8;
        public Texture2D projectileTex;

        public Player(Point p, string tfn, int fw, int h, int nf, ProjectileManager projManager) : base(p, tfn, fw, h, nf)
        {
            projectileManager = projManager;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                        tex,
                        GetDestinationRectangle(),
                        GetSourceRectangle(),
                        Color.White);
        }

        public override Rectangle GetDestinationRectangle()
        {
            // we want the sprite's texture to be centered on the point loc, as opposed to having loc as the top left corner
            return new Rectangle(loc.X - frameWidth / 2, loc.Y - height / 2, frameWidth, height);
        }

        public override void Update(GameTime gameTime, Viewport viewport)
        {
            //-----------------------------------------
            // Movement
            //-----------------------------------------
            var currentMouseState = Mouse.GetState();

            // Recognize a single click of the left mouse button
            if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed 
                && viewport.Bounds.Contains(new Point(currentMouseState.X, currentMouseState.Y))) // we only want to react to clicks inside the viewport/window
            {

                destination = new Point(currentMouseState.X, currentMouseState.Y);
                moving = true;

                //Calculate direction to destination
                velocity.direction = new Vector2(destination.X, destination.Y) - new Vector2(loc.X, loc.Y);
                velocity.direction.Normalize();

            }

            if (moving)
            {
                Vector2 move = velocity.direction * velocity.speed; // how far we should move
                Vector2 disttogo = new Vector2(destination.X, destination.Y) - new Vector2(loc.X, loc.Y); // a vector representing how far left there is to move
                int disttogolen = (int)Math.Sqrt(Math.Pow(disttogo.X, 2) + Math.Pow(disttogo.Y, 2)); // the straight line distance left to move.

                if (disttogolen < velocity.speed) // we're very close to our destination, and if we move further we'll miss it.
                {
                    moving = false;
                    lastdisttogolen = int.MaxValue;
                    loc = destination;

                }
                else if (lastdisttogolen < disttogolen) //Oh no! we're now moving further away from our destination! Stop moving.
                {
                    moving = false;
                    lastdisttogolen = int.MaxValue;
                }
                else // Move as per usual
                {
                    loc += new Point((int)move.X, (int)move.Y);
                    lastdisttogolen = disttogolen;
                }

            }


            //-----------------------------------------
            // Projectile Firing
            //-----------------------------------------

            KeyboardState newState = Keyboard.GetState();


            if (oldState.IsKeyDown(Keys.Space) && !newState.IsKeyDown(Keys.Space))
            {
                // Key was down last update, but not down now, so
                // it has just been released.

                //calculate direction to mouse
                Vector2 direction = new Vector2(currentMouseState.X, currentMouseState.Y) - new Vector2(loc.X, loc.Y);
                direction.Normalize();

                launchProjectile(direction);

            }

            // Update saved state.
            lastMouseState = currentMouseState;
            oldState = newState;
        }

        private void launchProjectile(Vector2 direction)
        {
            Velocity v = new Velocity(projectileSpeed, direction);
            Projectile p = new Projectile(loc, projectileTextureFilename, projectileTextureFrameWidth, projectileHeight, projectileNumFrames, projectileDamage, v, projectileTex, projectileManager);
            projectileManager.addProjectile(p);
        }
    }
}
