using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class Melee : Attack
    {
        private const int DAMAGE = 30;

        public Melee(Humanoid callInst) : base(callInst,new MeleeAnimationIdentifier("SlashRight", "SlashLeft", "SlashDown", "SlashUp"))
        {
        }

        public override void CommenceAttack()
        {
            CallingInstance.AttackTimeSpanTimer = 0;
            SoundManager.MeleeWeaponSwing.Play(GameSettings.soundeffectsLevel, 0.2f, 0);
            CallingInstance.CurrentWeapon.UseWeapon();
        }
    }
}
