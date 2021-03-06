﻿
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    public class Wizard : Enemy
    {
        public Wizard(Vector2 position, int maxHealthPoints = 75, float movingSpeed = 3, float attackTimespan = 0.4f) : base(position, maxHealthPoints, attackTimespan, movingSpeed)
        {
            ai = new WizardAI(this);

            inventory.WeaponInventory[0] = new Fist(this, 1f, 2.2f);
            inventory.WeaponInventory[1] = new Bow(this, 0.2f, 1.5f);
            // inventory.WeaponInventory[2] = new FireballWeapon(this,0.5f, 1.5f, 1.5f);
            inventory.WeaponInventory[2] = new EnergyballWeapon(this, 0.5f, 1.5f);


            inventory.CurrentWeapon = inventory.WeaponInventory[0];

            texture = TextureManager.enemy.Wizard_Idle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float FRAME_SPEED = 0.09f;

            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            { 
                // Idle
                {"IdleUp", new Animation(TextureManager.enemy.Wizard_Idle,0,6,tmpFrameSpeed=FRAME_SPEED*2.5f)},
                {"IdleLeft", new Animation(TextureManager.enemy.Wizard_Idle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.enemy.Wizard_Idle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.enemy.Wizard_Idle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp_Fist", new Animation(TextureManager.enemy.Wizard_Walk_Fist,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Fist",new Animation(TextureManager.enemy.Wizard_Walk_Fist,1,9,tmpFrameSpeed)},
                {"WalkDown_Fist", new Animation(TextureManager.enemy.Wizard_Walk_Fist,2,9,tmpFrameSpeed)},
                {"WalkRight_Fist", new Animation(TextureManager.enemy.Wizard_Walk_Fist,3,9,tmpFrameSpeed)},

                {"WalkUp_Dagger", new Animation(TextureManager.enemy.Wizard_Walk_Dagger,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Dagger",new Animation(TextureManager.enemy.Wizard_Walk_Dagger,1,9,tmpFrameSpeed)},
                {"WalkDown_Dagger", new Animation(TextureManager.enemy.Wizard_Walk_Dagger,2,9,tmpFrameSpeed)},
                {"WalkRight_Dagger", new Animation(TextureManager.enemy.Wizard_Walk_Dagger,3,9,tmpFrameSpeed)},

                { "WalkUp_Cane", new Animation(TextureManager.enemy.Wizard_Walk_Cane, 0, 9, tmpFrameSpeed = FRAME_SPEED)},
                { "WalkLeft_Cane",new Animation(TextureManager.enemy.Wizard_Walk_Cane, 1, 9, tmpFrameSpeed)},
                { "WalkDown_Cane", new Animation(TextureManager.enemy.Wizard_Walk_Cane, 2, 9, tmpFrameSpeed)},
                { "WalkRight_Cane", new Animation(TextureManager.enemy.Wizard_Walk_Cane, 3, 9, tmpFrameSpeed)},
                
                 // Melee-Angriff
                { "SlashUp_Fist", new Animation(TextureManager.enemy.Wizard_Slash_Fist,0,6,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Fist",new Animation(TextureManager.enemy.Wizard_Slash_Fist,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Fist", new Animation(TextureManager.enemy.Wizard_Slash_Fist,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Fist", new Animation(TextureManager.enemy.Wizard_Slash_Fist,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                {"SlashUp_Dagger", new Animation(TextureManager.enemy.Wizard_Slash_Dagger,0,6,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Dagger",new Animation(TextureManager.enemy.Wizard_Slash_Dagger,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Dagger", new Animation(TextureManager.enemy.Wizard_Slash_Dagger,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Dagger", new Animation(TextureManager.enemy.Wizard_Slash_Dagger,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                // Pfeil Schießen
                 {"ShootUp", new Animation(TextureManager.enemy.Wizard_Shoot,0,13,(tmpFrameSpeed=FRAME_SPEED*0.3f),NO_LOOP, PRIORITIZED)},
                {"ShootLeft",new Animation(TextureManager.enemy.Wizard_Shoot,1,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootDown", new Animation(TextureManager.enemy.Wizard_Shoot,2,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootRight", new Animation(TextureManager.enemy.Wizard_Shoot,3,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Zauber-Animation
                {"SpellcastUp", new Animation(TextureManager.enemy.Wizard_Spellcast,0,7,(tmpFrameSpeed=FRAME_SPEED*0.7f),NO_LOOP, PRIORITIZED)},
                {"SpellcastLeft",new Animation(TextureManager.enemy.Wizard_Spellcast,1,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastDown", new Animation(TextureManager.enemy.Wizard_Spellcast,2,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastRight", new Animation(TextureManager.enemy.Wizard_Spellcast,3,7,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.enemy.Wizard_Hurt,0,6,FRAME_SPEED*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(this,animations["IdleDown"]);
            defaultAnimationWeapon = "Fist";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


    }
}
