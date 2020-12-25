using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    static class MessageFactory
    {
        public static List<Message> messages = new List<Message>();
        public static List<Message> removedMessages = new List<Message>();
        internal class Message
        {
            public float transparency { get; private set; }
            public Vector2 position { get; private set; }
            public Color color { get; }
            public string message { get; }
            public bool expire { get; private set; }

            public Message(string message, Color color)
            {
                transparency = 0;
                this.message = message;
                this.color = color;
                this.position = new Vector2(Game1.gameSettings.screenWidth/2 - TextureManager.FontArial.MeasureString(message).X/2,0);
                this.expire = false;
            }

            public void UpdatePosition()
            {

                position = new Vector2(position.X, position.Y + 2);

                if (position.Y < Game1.gameSettings.screenHeight/2-100 && transparency < 1)
                {
                    transparency += 0.005f;
                    position = new Vector2(position.X, position.Y + 8);
                }

                if (position.Y > Game1.gameSettings.screenHeight / 2 - 100 && position.Y < Game1.gameSettings.screenHeight / 2 + 100)
                {
                    transparency = 1f;
                    position = new Vector2(position.X, position.Y + 1);
                }

                if (position.Y > Game1.gameSettings.screenHeight / 2 + 100 && transparency > 0)
                {
                    transparency -= 0.005f;
                    position = new Vector2(position.X, position.Y + 8);
                }


                if(position.Y > Game1.gameSettings.screenHeight)
                {
                    expire = true;
                }

            }
            public void Draw(SpriteBatch s)
            {
                s.DrawString(TextureManager.FontArial, message,position,color*transparency);
            }
        }

        public static void DisplayMessage(string message, Color c)
        {
            messages.Add(new Message(message, c));
        }

        public static void Draw(SpriteBatch spritebatch)
        {
            foreach(var m in messages)
            {
                m.Draw(spritebatch);
            }
        }

        public static void Update(GameTime gameTime)
        {
            foreach (var m in messages)
            {
                m.UpdatePosition();
                if(m.expire) removedMessages.Add(m);
            }

            foreach(var m in removedMessages)
            {
                messages.Remove(m);
            }

            removedMessages.Clear();
            
        }

        public static void ClearMessages()
        {
            messages.Clear();
            removedMessages.Clear();
        }
    }
}
