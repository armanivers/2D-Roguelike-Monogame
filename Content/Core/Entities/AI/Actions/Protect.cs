using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    // TODO: Funktion zum Schützen einbinden
    public class Protect : Ability
    {
        public Protect(Humanoid callInst) : base(callInst, new ProtectAnimationIdentifier("SpellcastRight", "SpellcastLeft", "SpellcastDown", "SpellcastUp"))
        {
        }

        

        public override void UseAbility()
        {
            // TODO: Hier die Logik für den Ablauf einfügen
            CallingInstance.Invincible = true;
        }
        public override void SetLineOfSight()
        {
            
        }
    }
}
