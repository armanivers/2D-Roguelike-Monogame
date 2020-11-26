﻿using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class RangeAttack : Attack
    {
        public RangeAttack(Humanoid callInst) : base(callInst, new RangeAttackAnimationIdentifier("ShootRight", "ShootLeft", "ShootDown", "ShootUp"))
        {
        }
        public override void CommenceAttack()
        {
            // TODO: Ausführen der Logik für den Angriff
            CallingInstance.CooldownTimer = 0;
            SoundManager.PlayerAttack.Play(0.2f, 0.2f, 0);
            Arrow a = new Arrow(CallingInstance);

            /*Arris Notizen:
              // create an explosion
            //Explosion e = new Explosion();
            //EntityManager.Add(e);
            Arrow a = new Arrow();
            //Position = (new Vector2(InputController.MousePosition.X-32, InputController.MousePosition.Y-48));
            //Kill();
             */
        }

       
    }
}