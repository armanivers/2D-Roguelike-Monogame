using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static _2DRoguelike.Content.Core.UI.MessageFactory.Message;

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
            public AnimationType animation { get; private set; }
            public float scale { get; private set; }
            public float rotation { get; private set; }

            public enum AnimationType
            {
                UpToDown,
                LeftToRight
            }

            public Message(string message, Color color,AnimationType animation)
            {
                this.message = message;
                this.animation = animation;
                SetAnimationProperties();
                this.color = color;
                this.expire = false;
            }

            public void SetAnimationProperties()
            {
                switch (animation)
                {
                    case AnimationType.UpToDown:
                        transparency = 0;
                        scale = 1f;
                        this.position = new Vector2(Game1.gameSettings.screenWidth / 2, 0);
                        rotation = 0f;
                        break;
                    case AnimationType.LeftToRight:
                        transparency = 0;
                        scale = 1f;
                        this.position = new Vector2(0, Game1.gameSettings.screenHeight / 2 - 100);
                        rotation = 0f;
                        break;
                    default:
                        UpToDownAnimation();
                        break;
                }
            }

            public void UpdatePosition()
            {
                switch (animation)
                {
                    case AnimationType.UpToDown:
                        UpToDownAnimation();
                        break;
                    case AnimationType.LeftToRight:
                        LeftToRightAnimation();
                        break;
                    default:
                        UpToDownAnimation();
                        break;
                }
            }

            public void UpToDownAnimation()
            {
                position = new Vector2(position.X, position.Y + 1);

                if (position.Y < Game1.gameSettings.screenHeight / 2 - 100 && transparency < 1)
                {
                    transparency += 0.005f;
                    position = new Vector2(position.X, position.Y + 4);
                }

                if (position.Y > Game1.gameSettings.screenHeight / 2 - 100 && position.Y < Game1.gameSettings.screenHeight / 2 + 100)
                {
                    transparency = 1f;
                    position = new Vector2(position.X, position.Y + 1);
                }

                if (position.Y > Game1.gameSettings.screenHeight / 2 + 100 && transparency > 0)
                {
                    transparency -= 0.005f;
                    position = new Vector2(position.X, position.Y + 5);
                }


                if (position.Y > Game1.gameSettings.screenHeight)
                {
                    expire = true;
                }
            }

            public void LeftToRightAnimation()
            {
                position = new Vector2(position.X+1, position.Y);

                if (position.X < Game1.gameSettings.screenWidth / 2 - 100 && transparency < 1)
                {
                    transparency += 0.005f;
                    position = new Vector2(position.X+4, position.Y);
                }

                if (position.X > Game1.gameSettings.screenWidth / 2 - 100 && position.X < Game1.gameSettings.screenWidth / 2 + 100)
                {
                    transparency = 1f;
                    position = new Vector2(position.X+1, position.Y);
                }

                if (position.X > Game1.gameSettings.screenWidth / 2 + 100 && transparency > 0)
                {
                    transparency -= 0.005f;
                    position = new Vector2(position.X+5, position.Y);
                }


                if (position.X > Game1.gameSettings.screenWidth)
                {
                    expire = true;
                }
            }

            public void Draw(SpriteBatch s)
            {
                //s.DrawString(TextureManager.FontArial, message,position,color*transparency);
                s.DrawString(TextureManager.FontArial, message, position, color*transparency, rotation, TextureManager.FontArial.MeasureString(message)/2, scale, SpriteEffects.None, 0);
            }
        }

        public static void DisplayMessage(string message, Color c)
        {
            messages.Add(new Message(message, c, AnimationType.LeftToRight));
        }
        public static void DisplayMessage(string message, Color c, AnimationType animation)
        {
            messages.Add(new Message(message, c,animation));
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
