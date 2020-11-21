using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class Melee : Attack
    {
        public Melee(Humanoid callInst) : base(callInst,new MeleeAnimationIdentifier("SlashRight", "SlashLeft", "SlashDown", "SlashUp"))
        {
        }

        public override void CommenceAttack()
        {
            // TODO: Ausführen der Logik für den Angriff
            throw new NotImplementedException();
        }

        public override void SetLineOfSight()
        {
            // TODO: Wohin schaut die Creature jetzt?
            throw new NotImplementedException();
        }
    }
}
