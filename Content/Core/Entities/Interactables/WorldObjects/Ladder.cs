using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.UI;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Interactables.WorldObjects
{
    public class Ladder : WorldObject
    {

        public Ladder(Vector2 pos) : base(pos)
        {
            texture = TextureManager.TransparentImage;
            Hitbox = new Rectangle((int)(Position.X), (int)(Position.Y), (int)(32 * ScaleFactor), (int)(32 * ScaleFactor));
        }

        public override void OnContact()
        {
            // check if player has obtained the key
            if(Player.Instance.hasLevelKey)
            {
                // if yes remove player's key item, play fade out scene and load next level (LevelManager.nextlevel())
                MessageFactory.DisplayMessage("Key Accepted", Color.Green);
                //Debug.Print("Next Level!");
                SoundManager.NextLevel.Play(Game1.gameSettings.soundeffectsLevel, 0.3f, 0);
                LevelManager.NextLevel();
            }
            else
            {
                // if no diplay message "Key missing"
                MessageFactory.DisplayMessage("Key Missing!", Color.Red);
                //Debug.Print("Key Missing!");
                SoundManager.KeyMissing.Play(Game1.gameSettings.soundeffectsLevel, 0.3f, 0);
            }  
        }

        public override void Update(GameTime gameTime)
        {
            // update something
        }
    }
}
