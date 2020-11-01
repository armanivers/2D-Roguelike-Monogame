using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Player
{
    class Player : EntityBasis
    {
        private static Player instance;
        private const float speed = 5;

        //animation
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame;
        private int totalFrames;
        private int timeSinceLastFrame = 0;
        private int millisecondsPerFrame = 50;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }

        private Player()
        {
            this.Position = new Vector2(2, 2);
            this.texture = TextureManager.Player;
            this.IsExpired = false;

            Rows = 1;
            Columns = 7;
            currentFrame = 0;
            totalFrames = 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Animations
            int width = 64;
            int height = 70;
            int row = (int)((float)currentFrame / Columns);
            int column = currentFrame % Columns;

            Rectangle sourceR = new Rectangle(width * column, height * row, width,height);
            Rectangle destR = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            spriteBatch.Draw(texture, destR,sourceR,Color.White);
            //spriteBatch.Draw(texture, Position, null, color, Orientation, Size / 2f, 1f, 0, 0);

        }

        public override void Update(GameTime gameTime)
        {
            //Handle Movement
            Velocity = speed * InputController.GetDirection();
            Position += Velocity;

            //Vector2.Clamp makes sure the player doesn't go outside of screen
            Position = Vector2.Clamp(Position, Size / 2, Game1.ScreenSize - Size / 2);

            //Animations
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if(timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                //increment current frame
                currentFrame++;
                timeSinceLastFrame = 0;
                if(currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }

        }

    }
}
