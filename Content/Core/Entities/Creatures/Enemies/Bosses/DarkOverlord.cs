using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.AI.Enemies_AI.Bosses_AI;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses
{
    public class DarkOverlord : Boss
    {
        const int DEFAULT_HEALTHPOINTS = 200;
        const int WEAPON_SLOT_CNT = 2; // 0: ShortRange / 1: LongRange
        public DarkOverlord(Vector2 position, float movingSpeed = 2, float attackTimespan = 0.4f, float scaleFactor = 1.6f) : base(position, DEFAULT_HEALTHPOINTS, attackTimespan, movingSpeed, scaleFactor)
        {

            ai = new DarkOverlordAI(this, (int)(DarkOverlordAI.DEFAULT_REACTION_TIME_MIN * 2.5), (int)(DarkOverlordAI.DEFAULT_REACTION_TIME_MAX * 2.5));

            bossName = "The Dark Overlord";


            inventory.WeaponInventory[0] = new FireballWeapon(this, 2.4f, 0.8f, 0.8f);
            inventory.WeaponInventory[1] = new EnergyballWeapon(this, 1f, 0.8f);

            inventory.CurrentWeapon = inventory.WeaponInventory[0];

            texture = TextureManager.enemy.DarkOverlord_Idle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float FRAME_SPEED = 0.09f;

            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.enemy.DarkOverlord_Idle,0,6,tmpFrameSpeed=FRAME_SPEED*2.5f)},
                {"IdleLeft", new Animation(TextureManager.enemy.DarkOverlord_Idle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.enemy.DarkOverlord_Idle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.enemy.DarkOverlord_Idle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp_Fist", new Animation(TextureManager.enemy.DarkOverlord_Walk_Fist,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Fist",new Animation(TextureManager.enemy.DarkOverlord_Walk_Fist,1,9,tmpFrameSpeed)},
                {"WalkDown_Fist", new Animation(TextureManager.enemy.DarkOverlord_Walk_Fist,2,9,tmpFrameSpeed)},
                {"WalkRight_Fist", new Animation(TextureManager.enemy.DarkOverlord_Walk_Fist,3,9,tmpFrameSpeed)},
                


                // Magie-Animation
                 {"SpellcastUp", new Animation(TextureManager.enemy.DarkOverlord_Spellcast,0,7,(tmpFrameSpeed=FRAME_SPEED*0.7f),NO_LOOP, PRIORITIZED)},
                {"SpellcastLeft",new Animation(TextureManager.enemy.DarkOverlord_Spellcast,1,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastDown", new Animation(TextureManager.enemy.DarkOverlord_Spellcast,2,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastRight", new Animation(TextureManager.enemy.DarkOverlord_Spellcast,3,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.enemy.DarkOverlord_Hurt,0,6,FRAME_SPEED*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(this, animations["IdleDown"]);
        }




        public override void Update(GameTime gameTime)
        {
            while(CannotWalkHere())
                Position = Room.getRandomCoordinateInCurrentRoom(this);
            base.Update(gameTime);
        }
    }
}
