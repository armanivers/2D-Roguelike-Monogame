using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities
{
    abstract class Humanoid: Creature
    {

        public Humanoid(Vector2 position,int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed){
            Hitbox = new Rectangle((int)Position.X + 17, (int)Position.Y + 14, 29, 49);
            // alle Humanoids besitzen gleiche Hitbox
        }

       
        public override void Move()
        {
            Velocity = movingSpeed * speedModifier * GetDirection();
            if (MultipleDirections(GetDirection()))
                Velocity /= 1.3f;

            Velocity.X = (float)Math.Round((double)Velocity.X);
            Velocity.Y = (float)Math.Round((double)Velocity.Y);

            // von float in int
            hitbox.X += (int)Velocity.X;
            // Wenn Bewegung nicht möglich: Hitbox wieder zurücksetzen
            // CollidesWithFrameBorder() weggemacht
            if (CollidesWithSolidTile())
            {
                hitbox.X -= (int)Velocity.X;

            }
            else
            {
                Position += new Vector2(Velocity.X, 0);
            }


            hitbox.Y += (int)Velocity.Y;
            if (CollidesWithSolidTile())
            {
                hitbox.Y -= (int)Velocity.Y;
            }
            else
            {
                Position += new Vector2(0, Velocity.Y);
            }
        }


        public bool CollidesWithSolidTile()
        {
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
                        - diese 4 Tiles überprüfen wir nun: Ist mindestens einer davon UNPASSABLE: nicht bewegen
                */
            Rectangle tileCollisionHitbox = GetTileCollisionHitbox();
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
            return false;
        }

        public bool CollidesWithFrameBorder()
        {
            return !new Rectangle(0, 0, (int)Game1.ScreenSize.X, (int)Game1.ScreenSize.Y).Contains(hitbox);
        }

        protected bool MultipleDirections(Vector2 direction) {
            return direction.X != 0 && direction.Y != 0;
        }

        protected abstract Vector2 GetDirection();
        // Player: Prüfen mit Tastatur
        // Enemies: Ermitteln mit KI-Ausgabe

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
