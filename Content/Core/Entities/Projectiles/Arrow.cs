using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Projectiles
{
    class Arrow : Projectile
    {
        private const float expireTimer = 3;
        private float timer;

        public Arrow() : base(new Vector2(Player.Player.Instance.Hitbox.X+16, Player.Player.Instance.Hitbox.Y+16), -7, +5, 10f)
        {
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y,13, 13);
            this.Velocity = Vector2.Normalize(InputController.MousePosition - Position);
            this.orientation = (float)Math.Atan2(Velocity.Y, Velocity.X);
            this.texture = TextureManager.Arrow;
            this.timer = 0;
        }

        public void checkCollision()
        {
            foreach (var livingEntity in EntityManager.entities)
            {
                //&& livingEntity != Player.Player.Instance pruefen damit das projectile dem spieler keinen schaden gibt
                if (livingEntity is Creature  && livingEntity != Player.Player.Instance)
                {
                    if (hitbox.Intersects(livingEntity.Hitbox))
                    {
                        ((Creature)livingEntity).GetHit(15);
                        isExpired = true;
                    }
                    else if (CollidesWithSolidTile())
                    {
                        SpeedModifier = 0f;
                        //Velocity = Vector2.Zero;
                       // isExpired = true; // Mittels expireTimer gelöst
                    }
                }
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
            Rectangle arrowHitbox = Hitbox;
            Point p = new Point(arrowHitbox.X / 32, arrowHitbox.Y / 32);    // NW

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            p = new Point((arrowHitbox.X + arrowHitbox.Width) / 32, arrowHitbox.Y / 32);   // NE

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            p = new Point(arrowHitbox.X / 32, ((arrowHitbox.Y + arrowHitbox.Height) / 32));    // SW

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }

            p = new Point((arrowHitbox.X + arrowHitbox.Width) / 32, ((arrowHitbox.Y + arrowHitbox.Height) / 32));    // SE
            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * flyingSpeed * SpeedModifier;
            
            if (timer > expireTimer)
            {
                this.isExpired = true;
            }
        }
    }
}
