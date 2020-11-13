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
        public HitboxDebug()
        {
            // TODO
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Player p = Player.Instance;
                
            foreach(var p in EntityManager.entities)
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

        // wird update ueberhaupt gebraucht?
        public void Update()
        {

        }
    }
}
