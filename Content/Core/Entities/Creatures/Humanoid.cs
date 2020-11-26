using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class Humanoid : Creature
    {


        public Actions.Action PerformedAction;
        public string LastAnimation { get; set; }
        private bool lockedAnimation = false;

        private const int TIME_BEFORE_DISAPPEARING = 100;
        private int disappearingTimer = 0;
        public Vector2 LineOfSight { get; set; }

        public Humanoid(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            Hitbox = new Rectangle((int)Position.X + 17, (int)Position.Y + 14, 29, 49);
            // alle Humanoids besitzen gleiche Hitbox
        }

        // ----------------------------------

        public abstract Actions.Action DetermineAction();

        /*Deprecated (wird in Action.ChooseAnimation geregelt):
          protected override void DetermineAnimation(Dictionary<string, Animation> animations)
        {
            // Angriff
            // TODO: Art des Angriffes bestimmen

            if (this is Player.Player && InputController.IsLeftMouseButtonPressed())
            {
                var differenz = InputController.MousePosition - Position;
                var angle = Math.Atan2(differenz.X, differenz.Y);

                if (angle > 1 && angle < 2)
                {
                    animationManager.Play(animations["ShootRight"]);
                }
                else if (angle > 2 && angle < 3)
                {
                    animationManager.Play(animations["ShootUp"]);
                }
                else if (angle > -3 && angle < -2)
                {
                    animationManager.Play(animations["ShootUp"]);
                }
                else if (angle > -1 && angle < 1)
                {
                    animationManager.Play(animations["ShootDown"]);
                }
                else if (angle < -1 && angle > -2)
                {
                    animationManager.Play(animations["ShootLeft"]);
                }
            }
            else
            {
                if (Acceleration.X > 0)
                {
                    animationManager.Play(animations["WalkRight"]);
                }
                else if (Acceleration.X < 0)
                {
                    animationManager.Play(animations["WalkLeft"]);
                }
                else if (Acceleration.Y > 0)
                {
                    animationManager.Play(animations["WalkDown"]);
                }
                else if (Acceleration.Y < 0)
                {
                    animationManager.Play(animations["WalkUp"]);
                }
                else if (Acceleration.X == 0 && Acceleration.Y == 0)
                {
                    animationManager.Play(animations["Idle"]);
                }
                else animationManager.Stop();
            }
        }*/

        public void SetAnimation(String animationIdentifier)
        {
            if (lockedAnimation)
            {
                if (!animationManager.IsRunning())
                    lockedAnimation = false;
                else return;
            }

            Debug.WriteLine(animationIdentifier);

            if (animationManager != null)
            {
                animationManager.Play(animations[animationIdentifier]);
                if (animationManager.IsPrioritized())
                    lockedAnimation = true;
            }

        }
         

        public void SetLineOfSight(Vector2 direction)
        {
            if (direction != Vector2.Zero)
                LineOfSight = direction;
        }
        public abstract Vector2 GetDirection();
        // Für Bewegungen
        // Player: Prüfen mit Tastatur
        // Enemies: Ermitteln mit KI-Ausgabe

        public abstract Vector2 GetAttackDirection();
        // Für Angriffe
        // Player: Prüfen mit Mausposition
        // Enemies: Ermitteln mit Player-Position

        public abstract Vector2 getAttackLineOfSight();
        // Für Blickrichtung nach Angriff
        // Player: Prüfen mit Mausposition
        // Enemies: Ermitteln mit Player-Position

        public bool MutipleDirections(Vector2 direction)
        {
            return direction.X != 0 && direction.Y != 0;
        }

        public bool CollidesWithSolidTile()
        {
            // Update: Code ist nun allgemeingültig für Entities mit größeren TileCollision-Hitboxen

            #region Explanation
                        /*     Wir überprüfen, ob die TileCollisionHitbox einer der Tiles überprüft
                    - ein Tile im Array ist 32x32 groß, d.h.:
                        -Tile1 im Array in currentLevel[0,0] geht von (0,0) bis (31,31)
                        - Tile2 im Array in currentLevel[0,1] geht von (32,0) bis (63,31) usw.
                    Also kann man die 4 Eckpunkte der Hitbox / 32 teilen und man erfährt die Indizes für die zu prüfenden Felder
                        -z.B.: HITBOX KOORDINATEN SIND: 
                            NW:[83,20]  
                            NE[112,20]  
                            SE[112,49]  
                            SW[83,49]
                        - teilen wir die Werte durch 32 ergibt das:
                            NW:[2,0]  
                            NE[3,0]  
                            SE[3,1]  
                            SW[2,1]
                        - in diesen Intervallen wird die Map nun überprüft: mindestens EINE solida Wand? Kollision
                */
            #endregion


            Rectangle tileCollisionHitbox = GetTileCollisionHitbox();
            int levelWidth = LevelManager.currentLevel.GetLength(0);
            int levelHeight = LevelManager.currentLevel.GetLength(1);
            // Handling von NullPointer-Exception
            int northWest = tileCollisionHitbox.X < 0 ? 0 : tileCollisionHitbox.X / 32;
            int northEast = (tileCollisionHitbox.X + tileCollisionHitbox.Width) / 32 >= levelWidth ? levelWidth - 1 : (tileCollisionHitbox.X + tileCollisionHitbox.Width) / 32;
            int southWest = tileCollisionHitbox.Y < 0 ? 0 : tileCollisionHitbox.Y / 32;
            int southEast = (tileCollisionHitbox.Y + tileCollisionHitbox.Height) / 32 >= levelHeight ? levelHeight - 1 : (tileCollisionHitbox.Y + tileCollisionHitbox.Height) / 32;


            for (int x = northWest;x <= northEast;x++)
            {
                for (int y = southWest;y <= southEast;y++)
                {
                    if (LevelManager.currentLevel[x, y].IsSolid())
                        return true;
                }
            }
            return false;

            #region AlterCode
             /*Alter Code:
            Point p = new Point(tileCollisionHitbox.X / 32, tileCollisionHitbox.Y / 32);    // NW

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            p = new Point((tileCollisionHitbox.X + tileCollisionHitbox.Width) / 32, tileCollisionHitbox.Y / 32);   // NE

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            p = new Point(tileCollisionHitbox.X / 32, ((tileCollisionHitbox.Y + tileCollisionHitbox.Height) / 32));    // SW

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }

            p = new Point((tileCollisionHitbox.X + tileCollisionHitbox.Width) / 32, ((tileCollisionHitbox.Y + tileCollisionHitbox.Height) / 32));    // SE
            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            return false;*/
            #endregion
           
        }

        public bool CollidesWithFrameBorder()
        {
            return !new Rectangle(0, 0, (int)Game1.ScreenSize.X, (int)Game1.ScreenSize.Y).Contains(hitbox);
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);

            #region Testinputs
            if (InputController.keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.K))
                if (this is Enemy)
                    Kill();
            if (InputController.keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X))
                if (this is Player.Player)
                    Kill();
            #endregion


            if (IsDead())
            {
                CommenceKillLogic();
            }
            else
            {
                PerformedAction = DetermineAction();
                PerformedAction.ExecuteAction();

                SetAnimation(PerformedAction.ChooseAnimation());
                // TODO: Method updateStats()
                if (CooldownTimer <= attackCooldown)
                    CooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            animationManager.Update(gameTime);
        }

        private void CommenceKillLogic() {
            // Priorität von Dieing-Animation geht über andere
            lockedAnimation = false;
            SetAnimation("Die");
            if (!animationManager.IsRunning())
            {
                if (disappearingTimer < TIME_BEFORE_DISAPPEARING) disappearingTimer++;
                else 
                Disappear();
            }
                
        }

        protected virtual void Disappear() {
            // TODO: Für Enemies: Loot droppen und EXP geben
            isExpired = true;
        }
    }

    
}
