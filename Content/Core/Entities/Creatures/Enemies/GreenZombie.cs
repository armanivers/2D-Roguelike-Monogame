using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    class GreenZombie : Enemy
    {
        const int WEAPON_SLOT_CNT = 2; // 0: ShortRange / 1: LongRange
        public GreenZombie(Vector2 position, int maxHealthPoints, float movingSpeed, float attackCooldown = 0.2f ) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            WeaponInventory = new Weapon[WEAPON_SLOT_CNT];

            WeaponInventory[0] = new Axe(1.2f);
            WeaponInventory[1] = new Bow(1.5f);

            texture = TextureManager.ZombieBrownIdle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float frameSpeed = 0.09f;
            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.ZombieBrownIdle,0,6,tmpFrameSpeed=frameSpeed*2.5f)},
                {"IdleLeft", new Animation(TextureManager.ZombieBrownIdle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.ZombieBrownIdle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.ZombieBrownIdle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp", new Animation(TextureManager.ZombieBrownWalk,0,9,tmpFrameSpeed=frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.ZombieBrownWalk,1,9,tmpFrameSpeed)},
                {"WalkDown", new Animation(TextureManager.ZombieBrownWalk,2,9,tmpFrameSpeed)},
                {"WalkRight", new Animation(TextureManager.ZombieBrownWalk,3,9,tmpFrameSpeed)},
                
                 // Melee-Angriff
                {"SlashUp", new Animation(TextureManager.ZombieBrownSlash,0,6,(tmpFrameSpeed=frameSpeed*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft",new Animation(TextureManager.ZombieBrownSlash,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown", new Animation(TextureManager.ZombieBrownSlash,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight", new Animation(TextureManager.ZombieBrownSlash,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                // Pfeil Schießen
                 {"ShootUp", new Animation(TextureManager.ZombieBrownShoot,0,13,(tmpFrameSpeed=frameSpeed*0.3f),NO_LOOP, PRIORITIZED)},
                {"ShootLeft",new Animation(TextureManager.ZombieBrownShoot,1,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootDown", new Animation(TextureManager.ZombieBrownShoot,2,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootRight", new Animation(TextureManager.ZombieBrownShoot,3,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.ZombieBrownHurt,0,6,frameSpeed*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(animations["IdleDown"]);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        
        public override Action DetermineAction()
        {
            // TODO: Anhand von Fakten, wie Status, position, playerPosition, blickfeld etc. eine Action erzeugen
            
            // Für Testzwecke, hier findet eigentlich entscheidung der KI statt
            if (!IsAttacking()){
                if (!WeaponInventory[1].InUsage())
                {
                    WeaponInventory[1].CooldownTimer = 0;
                    return new RangeAttack(this);

                }
                else if (!WeaponInventory[0].InUsage())
                {
                    WeaponInventory[0].CooldownTimer = 0;
                return new Melee(this);

                }
            }
            return new Move(this);
        }
    }
}
