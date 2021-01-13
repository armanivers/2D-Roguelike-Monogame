using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebugger
{
    class HitboxDebug
    {
        private int borderWidth = 3;
        public static bool DEBUG = true;
        public Texture2D borderTexture = TextureManager.ui.tileHitboxBorder;

        public List<Box> boxDebugBuffer = new List<Box>();



        public void AddToBoxDebugBuffer(Rectangle box, Color color,  int timeToDraw = 0)
        {
            if (Game1.gameSettings.DEBUG && Game1.gameSettings.showHitbox)
            {
                boxDebugBuffer.Add(new Box(box, color, timeToDraw));
            }
        }

        public void AddToBoxDebugBuffer(Rectangle box, Color color, bool always)
        {
            if (Game1.gameSettings.DEBUG &&  Game1.gameSettings.showHitbox)
            {
                boxDebugBuffer.Add(new Box(box, color, always));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.gameSettings.showMouse)
            {
                // Mouse Targeting Line
                var origin = Player.Instance.Hitbox;
                Vector2 target = InputController.MousePosition;
                DrawLine(spriteBatch, target, new Vector2(origin.X + 16, origin.Y + 16), Color.Gainsboro, 5);
            }

            if (Game1.gameSettings.showHitbox)
            {
                AddAllHitboxDataToList();
                DrawHitboxesFromBuffer(spriteBatch);
            }
        }

        private void DrawHitboxesFromBuffer(SpriteBatch spriteBatch)
        {
            for (int i = boxDebugBuffer.Count - 1; i >= 0; i--)
            {

                DrawRectangleHitbox(boxDebugBuffer[i].box, spriteBatch, boxDebugBuffer[i].color);

                if (!boxDebugBuffer[i].showAlways && boxDebugBuffer[i].timeToDraw == 0)
                    boxDebugBuffer.RemoveAt(i);
                else
                {
                    boxDebugBuffer[i].timeToDraw--;
                }
            }
        }

        private void AddAllHitboxDataToList()
        {
            #region ManuelleHitbox-Zuweisung für Creatures
            /*AddToBoxDebugBuffer(Player.Instance.GetTileCollisionHitbox(), Color.Red);
            AddToBoxDebugBuffer(Player.Instance.Hitbox, Color.Blue);
            AddToBoxDebugBuffer(Player.Instance.AttackHitbox, Color.White);

            DrawRectangleHitbox(Player.Instance.GetTileCollisionHitbox(), spriteBatch, Color.Red);
            DrawRectangleHitbox(Player.Instance.Hitbox, spriteBatch, Color.Blue);
            DrawRectangleHitbox(Player.Instance.AttackHitbox, spriteBatch, Color.White);


            Creature and Melee Hit Hitboxes
            foreach (var p in EntityManager.creatures)
            {
                Creature TileCollisionHitbox
                if (p is Creature)
                {
                    AddToBoxDebugBuffer(((Creature)p).GetTileCollisionHitbox(), Color.Red);
                    AddToBoxDebugBuffer(p.Hitbox, Color.Blue);
                    AddToBoxDebugBuffer(((Humanoid)p).AttackHitbox, Color.White);
                    if (p is Enemy)
                    {
                        AddToBoxDebugBuffer(((Enemy)p).AttackRangeHitbox, Color.Violet);
                    }

                    var t = ((Creature)p).GetTileCollisionHitbox();
                    DrawRectangleHitbox(t, spriteBatch, Color.Red);

                    // Melee Hitbox
                    t = ((Humanoid)p).AttackHitbox;
                    DrawRectangleHitbox(t, spriteBatch, Color.White);

                    // Melee Range Hitbox
                    if (p is Enemy)
                    {
                        t = ((Enemy)p).AttackRangeHitbox;
                        DrawRectangleHitbox(t, spriteBatch, Color.Violet);
                    }
                }
            }
            */
            #endregion

            foreach (var p in EntityManager.projectiles)
            {
                AddToBoxDebugBuffer(p.Hitbox, Color.Blue);
                //DrawRectangleHitbox(p.Hitbox, spriteBatch, Color.Blue);
            }

            foreach (var l in EntityManager.interactables)
            {
                AddToBoxDebugBuffer(l.Hitbox, Color.Blue);
                //DrawRectangleHitbox(l.Hitbox, spriteBatch, Color.Blue);
            }
        }

        public void DrawRectangleHitbox(Rectangle r, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, borderWidth, r.Height), color); // Top
            spriteBatch.Draw(borderTexture, new Rectangle(r.Right, r.Top, borderWidth, r.Height), color); // 
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, r.Width, borderWidth), color); //   
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Bottom, r.Width, borderWidth), color); // Bottom
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 from, Vector2 to, Color color, int width = 1)
        {
            Rectangle rect = new Rectangle((int)from.X, (int)from.Y, (int)(to - from).Length() + width, width);
            Vector2 vector = Vector2.Normalize(from - to);
            float angle = (float)Math.Acos(Vector2.Dot(vector, -Vector2.UnitX));
            Vector2 origin = Vector2.Zero;

            if (from.Y > to.Y)
                angle = MathHelper.TwoPi - angle;

            spriteBatch.Draw(TextureManager.ui.tileHitboxBorder, rect, null, color, angle, origin, SpriteEffects.None, 0);
        }

    }

    class Box
    {
        public Rectangle box;
        public Color color;
        public bool showAlways;
        public int timeToDraw;

        public Box(Rectangle box, Color color, bool always)
        {
            this.box = box;
            this.color = color;
            this.showAlways = always;
        }

        public Box(Rectangle box, Color color, int timeToDraw)
        {
            this.box = box;
            this.color = color;
            this.timeToDraw = timeToDraw;
        }
    }
}
