


using System.Diagnostics;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.AI.Actions.AnimationIdentifiers.EnemyAnimationIdentifiers;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.AI.Actions
{
    class ProjectileBarrage : Attack
    {
        public const float DEFAULT_TIME_IN_STATE = 50;
        float timeToStayInState;
        protected float expiredTimeInState;

        const float DEFAULT_AMOUNT_OF_ATTACKS = 3f;
        float amountOfFiredAttacks;
        

        private static float startingGameTime = 0f;


        public ProjectileBarrage(Humanoid callInst, float startingTime, float amountOfAttacks = DEFAULT_AMOUNT_OF_ATTACKS, float attackDuration = DEFAULT_TIME_IN_STATE) : base(callInst, new ProjectileBarrageAnimationIdentifier("ShootRight", "ShootLeft", "ShootDown", "ShootUp"))
        {

            startingGameTime = startingTime;
            timeToStayInState = attackDuration;
            amountOfFiredAttacks = amountOfAttacks;
        }

        float currentTimeInterval = 1f;
        public override void CommenceAttack()
        {
            CallingInstance.Mana = 0;

            if (expiredTimeInState > timeToStayInState * (currentTimeInterval / amountOfFiredAttacks))
            {
                // Debug.WriteLine("Current Time In State: " + expiredTimeInState);
                // Debug.WriteLine("Time has exceeded the gap: " + (DEFAULT_TIME_IN_STATE * (currentTimeInterval / expiredTimeInState)));

                currentTimeInterval++;
                new RangeAttack(CallingInstance).ExecuteAction();
            }

            // Debug.WriteLine("---");

        }

        public override bool StateFinished(float currentGameTime)
        {
            expiredTimeInState += (currentGameTime - startingGameTime);

            if (expiredTimeInState >= timeToStayInState) /*(currentGameTime - timeOfLastUsage) > DEFAULT_TIME_IN_STATE)*/
            {
                // ein letzter Angriff
                new RangeAttack(CallingInstance).ExecuteAction();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanSwitchToState(Humanoid creat)
        {
            return creat.Mana == Creature.MAX_MANA && !creat.IsAttacking() && creat.inventory.CurrentWeapon is LongRange /*&& !creat.inventory.CurrentWeapon.InUsage()*/;
            //return timeOfLastUsage == 0f || currentGameTime - timeOfLastUsage >= PROTECT_COOLDOWN;
        }
    }
}
