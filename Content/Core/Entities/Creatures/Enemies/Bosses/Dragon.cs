using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.AI.Enemies_AI.Bosses_AI;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses
{
    public class Dragon : Boss
    {
        const int DEFAULT_HEALTHPOINTS = 600;
        const int WEAPON_SLOT_CNT = 2; // 0: ShortRange / 1: LongRange
        public Dragon(Vector2 position, float movingSpeed = 3, float attackTimespan = 0.4f, float scaleFactor = 1.6f) : base(position, DEFAULT_HEALTHPOINTS, attackTimespan, movingSpeed, scaleFactor)
        {

            ai = new DragonAI(this, DragonAI.DEFAULT_REACTION_TIME_MIN * 2, DragonAI.DEFAULT_REACTION_TIME_MAX * 2);

            bossName = "The Dragon";

            inventory.WeaponInventory[0] = new Fist(this, 2.4f, 0.8f, 0.8f);
            inventory.WeaponInventory[1] = new FireballWeapon(this, 1f, 0.8f);

            inventory.CurrentWeapon = inventory.WeaponInventory[0];

            texture = TextureManager.enemy.Dragon_Idle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float FRAME_SPEED = 0.09f;

            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.enemy.Dragon_Idle,0,6,tmpFrameSpeed=FRAME_SPEED*2.5f)},
                {"IdleLeft", new Animation(TextureManager.enemy.Dragon_Idle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.enemy.Dragon_Idle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.enemy.Dragon_Idle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp_Fist", new Animation(TextureManager.enemy.Dragon_Walk_Fist,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Fist",new Animation(TextureManager.enemy.Dragon_Walk_Fist,1,9,tmpFrameSpeed)},
                {"WalkDown_Fist", new Animation(TextureManager.enemy.Dragon_Walk_Fist,2,9,tmpFrameSpeed)},
                {"WalkRight_Fist", new Animation(TextureManager.enemy.Dragon_Walk_Fist,3,9,tmpFrameSpeed)},
                
                 // Melee-Angriff
                {"SlashUp_Fist", new Animation(TextureManager.enemy.Dragon_Slash_Fist,0,6,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Fist",new Animation(TextureManager.enemy.Dragon_Slash_Fist,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Fist", new Animation(TextureManager.enemy.Dragon_Slash_Fist,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Fist", new Animation(TextureManager.enemy.Dragon_Slash_Fist,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                // Magie-Animation
                 {"SpellcastUp", new Animation(TextureManager.enemy.Dragon_Spellcast,0,7,(tmpFrameSpeed=FRAME_SPEED*0.5f),NO_LOOP, PRIORITIZED)},
                {"SpellcastLeft",new Animation(TextureManager.enemy.Dragon_Spellcast,1,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastDown", new Animation(TextureManager.enemy.Dragon_Spellcast,2,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastRight", new Animation(TextureManager.enemy.Dragon_Spellcast,3,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.enemy.Dragon_Hurt,0,6,FRAME_SPEED*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(this, animations["IdleDown"]);
        }
        
         
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
