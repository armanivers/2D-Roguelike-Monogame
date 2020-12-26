using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    public class BrownZombie : Enemy
    {
        const int WEAPON_SLOT_CNT = 2; // 0: ShortRange / 1: LongRange
        public BrownZombie(Vector2 position, int maxHealthPoints, float movingSpeed, float attackTimespan = 0.4f ) : base(position, maxHealthPoints, attackTimespan, movingSpeed)
        {
            ai = new BrownZombieAI(this);
            WeaponInventory = new Weapon[WEAPON_SLOT_CNT];

            WeaponInventory[0] = new Fist(this,1f, 2.2f, 0.5f, 1f);
            WeaponInventory[1] = new Bow(this, 0.7f, 1.5f);
            CurrentWeapon = WeaponInventory[0];

            texture = TextureManager.ZombieBrown_Idle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float FRAME_SPEED = 0.09f;

            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.ZombieBrown_Idle,0,6,tmpFrameSpeed=FRAME_SPEED*2.5f)},
                {"IdleLeft", new Animation(TextureManager.ZombieBrown_Idle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.ZombieBrown_Idle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.ZombieBrown_Idle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp_Fist", new Animation(TextureManager.ZombieBrown_Walk_Fist,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Fist",new Animation(TextureManager.ZombieBrown_Walk_Fist,1,9,tmpFrameSpeed)},
                {"WalkDown_Fist", new Animation(TextureManager.ZombieBrown_Walk_Fist,2,9,tmpFrameSpeed)},
                {"WalkRight_Fist", new Animation(TextureManager.ZombieBrown_Walk_Fist,3,9,tmpFrameSpeed)},
                
                 // Melee-Angriff
                {"SlashUp_Fist", new Animation(TextureManager.ZombieBrown_Slash_Fist,0,6,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Fist",new Animation(TextureManager.ZombieBrown_Slash_Fist,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Fist", new Animation(TextureManager.ZombieBrown_Slash_Fist,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Fist", new Animation(TextureManager.ZombieBrown_Slash_Fist,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                // Pfeil Schießen
                 {"ShootUp", new Animation(TextureManager.ZombieBrown_Shoot,0,13,(tmpFrameSpeed=FRAME_SPEED*0.3f),NO_LOOP, PRIORITIZED)},
                {"ShootLeft",new Animation(TextureManager.ZombieBrown_Shoot,1,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootDown", new Animation(TextureManager.ZombieBrown_Shoot,2,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootRight", new Animation(TextureManager.ZombieBrown_Shoot,3,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.ZombieBrown_Hurt,0,6,FRAME_SPEED*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(this,animations["IdleDown"]);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override Vector2 GetDirection()
        {
            // TODO: KI nach Angaben fragen
            return new Vector2(0, 0);
        }

    }
}
