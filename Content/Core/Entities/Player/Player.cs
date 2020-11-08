using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Player
{
    class Player : EntityBasis
    {
        private static Player instance;
        private const float playerSpeed = 5;
        // cooldown in seconds!
        private const float attackCooldown = 2;
        private float cooldownTimer;
        // Erstellung der Hitbox 
        private Rectangle hitbox;
        public Rectangle Hitbox { get { return hitbox; }}

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }

        private Player()
        {
            this.position = new Vector2(2*32, 5*32);
            this.texture = TextureManager.Player;
            float frameSpeed = 0.09f;
            this.animations = new Dictionary<string, Animation>()
            {
                // Todo: idle animation texturesheet erstellen!
                {"Idle", new Animation(TextureManager.Player,1,frameSpeed*4)},
                {"WalkUp", new Animation(TextureManager.PlayerWalkUpAxe,9,frameSpeed)},
                {"WalkDown", new Animation(TextureManager.PlayerWalkDownAxe,9,frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.PlayerWalkLeftAxe,9,frameSpeed)},
                {"WalkRight", new Animation(TextureManager.PlayerWalkRightAxe,9,frameSpeed)}
            };
            this.animationManager = new AnimationManager(animations.First().Value);
            this.isExpired = false;
            this.cooldownTimer = attackCooldown;
            hitbox = new Rectangle((int)Position.X + 17, (int)Position.Y + 14, 29, 49);
        }
        private void DetermineAnimation()
        {
            if (Velocity.X > 0)
            {
                animationManager.Play(animations["WalkRight"]);
            }
            else if (Velocity.X < 0)
            {
                animationManager.Play(animations["WalkLeft"]);
            }
            else if(Velocity.Y > 0)
            {
                animationManager.Play(animations["WalkDown"]);
            }
            else if(Velocity.Y < 0)
            {
                animationManager.Play(animations["WalkUp"]);
            }
            else if(Velocity.X == 0 && Velocity.Y == 0)
            {
                animationManager.Play(animations["Idle"]);
            }
            else
            {
                animationManager.Stop();
            }
        }

        public void CheckMovement()
        {
            Velocity = playerSpeed * InputController.GetDirection();
            if ((InputController.GetPressedKeys().Intersect<Keys>(new Keys[] { Keys.W, Keys.A, Keys.S, Keys.D }).Count()) > 1)
                Velocity /= 1.5f;

            // TODO Du bist hässlich
            Velocity.X = (float)Math.Round((double)Velocity.X);
            Velocity.Y = (float)Math.Round((double)Velocity.Y);

            // von float in int
            hitbox.X += (int)Velocity.X;
            hitbox.Y += (int)Velocity.Y;

            // Wenn Bewegung nicht möglich: Hitbox wieder zurücksetzen
            // CollidesWithFrameBorder() weggemacht
            if (CollidesWithSolidTile())
            {
                hitbox.X -= (int)Velocity.X;
                hitbox.Y -= (int)Velocity.Y;
            }
            else
            {
                /*
                Rectangle tileHitbox = GetTileCollisionHitbox();
                Debug.Print("DEBUG: Hitbox Coordinates: NW:[{0},{1}]  NE[{2},{3}]  SE[{4},{5}]  SW[{6},{7}]",
                    tileHitbox.X, tileHitbox.Y,
                    tileHitbox.X+ tileHitbox.Width, tileHitbox.Y ,
                    tileHitbox.X+ tileHitbox.Width, tileHitbox.Y + tileHitbox.Height,
                    tileHitbox.X, tileHitbox.Y + tileHitbox.Height );
                */
                Position += Velocity;
            }
        }

        public void CheckAttacking()
        {
            if (!InputController.GetMouseClickPosition().Equals(Vector2.Zero) && cooldownTimer > attackCooldown)
            {
                cooldownTimer = 0;
                SoundManager.PlayerAttack.Play(0.2f, 0.2f, 0);
                Camera.ShakeScreen();
                // create an explosion
                Explosion e = new Explosion(new Vector2(InputController.MousePosition.X-20, InputController.MousePosition.Y-20));
                EntityManager.Add(e);
                e.Explode();
            }
        }

        public Rectangle GetTileCollisionHitbox() {
            return new Rectangle(hitbox.X+5, hitbox.Y +5 + 20, hitbox.Width -10 , hitbox.Height -10 - 20);
        }

        public bool CollidesWithSolidTile() {
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
            p = new Point( (tileCollisionHitbox.X + tileCollisionHitbox.Width) / 32, tileCollisionHitbox.Y / 32);   // NE

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            p = new Point(tileCollisionHitbox.X / 32, ( (tileCollisionHitbox.Y + tileCollisionHitbox.Height) / 32) );    // SW

            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }

            p = new Point( (tileCollisionHitbox.X + tileCollisionHitbox.Width ) / 32, (( tileCollisionHitbox.Y + tileCollisionHitbox.Height) / 32));    // SE
            if (!(p.X >= LevelManager.currentLevel.GetLength(0) || p.Y >= LevelManager.currentLevel.GetLength(1)) && LevelManager.currentLevel[p.X, p.Y].IsSolid())
            {
                return true;
            }
            return false;
        }

        public bool CollidesWithFrameBorder() {
            return !new Rectangle(0, 0, (int)Game1.ScreenSize.X, (int)Game1.ScreenSize.Y).Contains(hitbox);
        }

        public override void Update(GameTime gameTime)
        {
            CheckMovement();
            DetermineAnimation();
            CheckAttacking();

            cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            animationManager.Update(gameTime);
        }

    }
}
