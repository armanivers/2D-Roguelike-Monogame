using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebug
{
    class PlayerDebug
    {
        private Vector2 playerPositionOnScreen;
        private Vector2 playerPositionOnMap;
        private Vector2 mousePosition;
        private Vector2 anglePosition;
        private Vector2 currentWeaponPosition;

        const bool DEBUG = true;
        public PlayerDebug()
        { 
            playerPositionOnScreen = new Vector2(0, 0);
            playerPositionOnMap = new Vector2(0, 40);
            mousePosition = new Vector2(0, 80);
            anglePosition = new Vector2(0, 120);
            currentWeaponPosition = new Vector2(0, 160);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (DEBUG)
            {
                spriteBatch.DrawString(TextureManager.FontArial,
                "Position Player auf Screen: {X: " + (int)Player.Instance.Position.X + " Y:" + (int)Player.Instance.Position.Y + "}",
                playerPositionOnScreen, Color.White);
                spriteBatch.DrawString(TextureManager.FontArial,
                "Position Player auf Map: {X: " + (int)(Player.Instance.GetTileCollisionHitbox().X / 32) + " Y:" + (int)(Player.Instance.GetTileCollisionHitbox().Y / 32) + "}",
                    playerPositionOnMap, Color.White);
                spriteBatch.DrawString(TextureManager.FontArial,
                    "Position Maus auf Map: {X: " + (int)InputController.MousePosition.X / 32 + " Y:" + (int)InputController.MousePosition.Y / 32 + "}",
                    mousePosition, Color.White);

                // Winkel Berechnung

                var differenz = InputController.MousePosition - new Vector2(Player.Instance.HitboxCenter.X, Player.Instance.HitboxCenter.Y);
                var angle = Math.Atan2(differenz.Y, differenz.X);
                // Umwandlung Radian -> Degree

                //angle = MathHelper.ToDegrees((float)angle);
                //if(angle < 0)
                //{
                //    angle = 360 - (-angle);
                //}
                spriteBatch.DrawString(TextureManager.FontArial, "Winkel des Mauses: " +
                    angle, anglePosition, Color.White);
                spriteBatch.DrawString(TextureManager.FontArial, "Waffe: " +
                    Player.Instance.CurrentWeapon?.ToString(), currentWeaponPosition, Color.White);
            }
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 from, Vector2 to, Color color, int width = 1)
        {
            Rectangle rect = new Rectangle((int)from.X, (int)from.Y, (int)(to - from).Length() + width, width);
            Vector2 vector = Vector2.Normalize(from - to);
            float angle = (float)Math.Acos(Vector2.Dot(vector, -Vector2.UnitX));
            Vector2 origin = Vector2.Zero;

            if (from.Y > to.Y)
                angle = MathHelper.TwoPi - angle;

            spriteBatch.Draw(TextureManager.tileHitboxBorder, rect, null, color, angle, origin, SpriteEffects.None, 0);
        }
    }
}
