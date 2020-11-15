using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebug
{
    class HitboxDebug
    {
        private int borderWith = 3;

        public void Draw(SpriteBatch spriteBatch)
        {
            // Mouse Targeting Line
            var origin = Player.Instance.Hitbox;
            Vector2 target = InputController.MousePosition;
            DrawLine(spriteBatch, target, new Vector2(origin.X+16,origin.Y+16),Color.Gainsboro, 5);

            //Entity Hitboxes

            foreach (var p in EntityManager.entities)
            {
                // TileCollisionBox                
                var borderTexture = TextureManager.tileHitboxBorder;

                if (p is Creature)
                {
                    var t = ((Creature)p).GetTileCollisionHitbox();
                    spriteBatch.Draw(borderTexture, new Rectangle(t.Left, t.Top, borderWith, t.Height), Color.Red); // Top
                    spriteBatch.Draw(borderTexture, new Rectangle(t.Right, t.Top, borderWith, t.Height), Color.Red); // 
                    spriteBatch.Draw(borderTexture, new Rectangle(t.Left, t.Top, t.Width, borderWith), Color.Red); // 
                    spriteBatch.Draw(borderTexture, new Rectangle(t.Left, t.Bottom, t.Width, borderWith), Color.Red); // Bottom
                }

                //Entity Hitbox
                var r = p.Hitbox;
                spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, borderWith, r.Height), Color.Blue); // Top
                spriteBatch.Draw(borderTexture, new Rectangle(r.Right, r.Top, borderWith, r.Height), Color.Blue); // 
                spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, r.Width, borderWith), Color.Blue); //   
                spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Bottom, r.Width, borderWith), Color.Blue); // Bottom
            }
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
