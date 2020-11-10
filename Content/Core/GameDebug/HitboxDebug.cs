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

        // TODO: fur alle entities definieren, das heisst getTileCollisonHitbox() methode + Hitbox Property in EntityBasis erstellen und dann foreach Schleife!
        public void Draw(SpriteBatch spriteBatch)
        {
            Player p = Player.Instance;
                
            // TileCollisionBox                
            var borderTexture = TextureManager.tileHitboxBorder;

            var r = p.GetTileCollisionHitbox();
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, borderWith, r.Height), Color.Red); // 
            spriteBatch.Draw(borderTexture, new Rectangle(r.Right, r.Top, borderWith, r.Height), Color.Red); // 
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, r.Width, borderWith), Color.Red); // 
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Bottom, r.Width, borderWith), Color.Red); // 

            //Entity Hitbox
            r = p.Hitbox;
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, borderWith, r.Height), Color.Blue); // 
            spriteBatch.Draw(borderTexture, new Rectangle(r.Right, r.Top, borderWith, r.Height), Color.Blue); // 
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Top, r.Width, borderWith), Color.Blue); //   
            spriteBatch.Draw(borderTexture, new Rectangle(r.Left, r.Bottom, r.Width, borderWith), Color.Blue); // Bottom
        }

        // wird update ueberhaupt gebraucht?
        public void Update()
        {

        }
    }
}
