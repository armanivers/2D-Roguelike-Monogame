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
        public static SpriteFont font = TextureManager.FontArial;
        public static List<Message> messages = new List<Message>();
        public static List<Message> removedMessages = new List<Message>();
        internal class Message
        {
            public float transparency { get; private set; }
            public Vector2 position { get; private set; }
            public Color color { get; }
            public string message { get; }
            public int messageSize { get; }
            public bool expire { get; private set; }
            public AnimationType animation { get; private set; }
            public float scale { get; private set; }
            public float rotation { get; private set; }
            public float timer { get; private set; }

            public enum AnimationType
            {
                UpToDown,
                LeftToRight,
                MiddleFadeInOut,
                LeftToRightUpDown,
                LeftToRightCos,
                DownSimpleFade,
                UpSimpleFade
            }

            public Message(string message, Color color,AnimationType animation)
            {
                // defaults, can be overwritten depending on animation preferences
                rotation = 0f;
                scale = 1f;
                transparency = 0f;

                this.message = message;
                this.animation = animation;
                
                SetAnimationProperties();

                this.color = color;
                this.expire = false;
                this.timer = 0;
            }

            public void SetAnimationProperties()
            {
                switch (animation)
                {
                    case AnimationType.UpToDown:
                        this.position = new Vector2(Game1.gameSettings.screenWidth / 2, 0);
                        break;
                    case AnimationType.LeftToRight:
                        this.position = new Vector2(0, Game1.gameSettings.screenHeight / 2 - 100);
                        break;
                    case AnimationType.MiddleFadeInOut:
                        position = new Vector2(Game1.gameSettings.screenWidth / 2, Game1.gameSettings.screenHeight / 2-100);
                        break;
                    case AnimationType.LeftToRightCos:
                        transparency = 1f;
                        position = new Vector2(0, Game1.gameSettings.screenHeight / 2-120);
                        break;
                    case AnimationType.DownSimpleFade:
                        transparency = 1f;
                        position = new Vector2(Game1.gameSettings.screenWidth / 2, Game1.gameSettings.screenHeight  - 200);
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
                    case AnimationType.MiddleFadeInOut:
                        MiddleFadeInOut();
                        break;
                    case AnimationType.LeftToRightCos:
                        LeftRightCos();
                        break;
                    case AnimationType.DownSimpleFade:
                        DownSimpleFade();
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

            public void MiddleFadeInOut()
            {
                transparency = -0.04f * timer * (timer - 10f);
                
                if (timer > 10)
                {
                    expire = true;
                }

                timer+=0.05f;
            }

            public void LeftRightCos()
            {
                // timer hier used as time variable for the cos function
                timer += 0.05f;

                // 3 in x coordinate is default speed and the (2* ..) in y coordinate is for the vertical scale of the cos function, because it otherwise only returns a value between -1 and 1
                position = new Vector2(position.X + 4, position.Y + 2 * (float)Math.Cos(timer));

                if (position.X > Game1.gameSettings.screenWidth)
                {
                    expire = true;
                }

            }

            public void DownSimpleFade()
            {
                transparency = -0.04f * timer * (timer - 10f);

                if (timer > 10)
                {
                    expire = true;
                }

                timer += 0.05f;
            }

            public void Draw(SpriteBatch s)
            {
                //s.DrawString(TextureManager.FontArial, message,position,color*transparency);
                s.DrawString(font, message, position, color*transparency, rotation, TextureManager.FontArial.MeasureString(message)/2, scale, SpriteEffects.None, 0);
            }
        }

        public static void DisplayMessage(string message, Color c)
        {
            messages.Add(new Message(message, c, AnimationType.DownSimpleFade));
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
