using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebug
{
    class HitboxDebug
    {
        private int borderWidth = 3;
        public static bool DEBUG = true;
        public Texture2D borderTexture = TextureManager.tileHitboxBorder;

        public void Draw(SpriteBatch spriteBatch)
        {

            if (GameSettings.showMouse)
            {
                // Mouse Targeting Line
                var origin = Player.Instance.Hitbox;
                Vector2 target = InputController.MousePosition;
                DrawLine(spriteBatch, target, new Vector2(origin.X + 16, origin.Y + 16), Color.Gainsboro, 5);
            }

            if (GameSettings.showHitbox) {
                // Creature and Melee Hit Hitboxes
                foreach (var p in EntityManager.creatures)
                {
                    // Creature TileCollisionHitbox                
                    if (p is Creature)
                    {
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
                    // Creature Hitbox
                    DrawRectangleHitbox(p.Hitbox, spriteBatch, Color.Blue);
                }

                foreach (var p in EntityManager.projectiles)
                {
                    DrawRectangleHitbox(p.Hitbox, spriteBatch, Color.Blue);
                }

                foreach (var l in EntityManager.loots)
                {
                    DrawRectangleHitbox(l.Hitbox, spriteBatch, Color.Blue);
                }
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

            spriteBatch.Draw(TextureManager.tileHitboxBorder, rect, null, color, angle, origin, SpriteEffects.None, 0);
        }

    }
}
