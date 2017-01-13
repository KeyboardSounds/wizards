using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards
{
    public enum BeingDirection
    {
        Front,
        Right,
        Back,
        Left
    }

    abstract class Entity //TODO: fix inheritance
    {
        public Point loc;
        public Texture2D tex;
        public string texFileName;
        public int frameWidth;
        protected int FramesPerDir = 4;
        public BeingDirection direction = BeingDirection.Front;
        public int height;
        protected int frame = 0;
        protected int numFrames;
        public Velocity velocity = new Velocity(0, new Vector2(0,0));

        public Entity(Point p, string tfn, int fw, int h, int nf)
        {
            loc = p;
            texFileName = tfn;
            frameWidth = fw;
            height = h;
            numFrames = nf;
        }

        public virtual Rectangle GetSourceRectangle()
        {
            int x = frame % (tex.Width / frameWidth);

            return new Rectangle(x * frameWidth, 0, frameWidth, height);
        }

        public virtual Rectangle GetDestinationRectangle()
        {
            return new Rectangle(loc.X, loc.Y, frameWidth, height);
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime, Viewport viewport);

    }
}
