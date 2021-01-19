using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebugger
{
    class PlayerDebug
    {
        private Vector2 indent;

        public PlayerDebug()
        {
            indent = Vector2.Zero;
        }

        private Vector2 MoveIndent()
        {
            Vector2 ret = indent;
            indent += new Vector2(0, 40);
            return ret;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            indent = Vector2.Zero;
            if (Game1.gameSettings.playerDebug)
            {
                spriteBatch.DrawString(TextureManager.FontArial,
                "Position Player on Screen: {X: " + (int)Player.Instance.Position.X + " Y:" + (int)Player.Instance.Position.Y + "}",
                MoveIndent(), Color.White);
                spriteBatch.DrawString(TextureManager.FontArial,
                "Position Player on Map: {X: " + (int)(Player.Instance.GetTileCollisionHitbox().X / 32) + " Y:" + (int)(Player.Instance.GetTileCollisionHitbox().Y / 32) + "}",
                    MoveIndent(), Color.White);
                spriteBatch.DrawString(TextureManager.FontArial,
                    "Position Mouse on Map: {X: " + (int)InputController.MousePosition.X / 32 + " Y:" + (int)InputController.MousePosition.Y / 32 + "}",
                    MoveIndent(), Color.White);

                // Winkel Berechnung

                var differenz = InputController.MousePosition - new Vector2(Player.Instance.HitboxCenter.X, Player.Instance.HitboxCenter.Y);
                var angle = Math.Atan2(differenz.Y, differenz.X);
                // Umwandlung Radian -> Degree

                //angle = MathHelper.ToDegrees((float)angle);
                //if(angle < 0)
                //{
                //    angle = 360 - (-angle);
                //}
                spriteBatch.DrawString(TextureManager.FontArial, "Angle of Mouse: " +
                    angle, MoveIndent(), Color.White);
                spriteBatch.DrawString(TextureManager.FontArial, "Weapon: " +
                    Player.Instance.inventory.CurrentWeapon?.ToString(), MoveIndent(), Color.White);
                if (LevelManager.currentmap.currentroom != null)
                {
                    spriteBatch.DrawString(TextureManager.FontArial, "Room: " + LevelManager.currentmap.currentroom.Width + " , " + LevelManager.currentmap.currentroom.Height, MoveIndent(), Color.White);
                }
                spriteBatch.DrawString(TextureManager.FontArial, "Exit: " + Room.exithitbox.X / 32 + " , " + Room.exithitbox.Y / 32, MoveIndent(), Color.White);

                // spriteBatch.DrawString(TextureManager.FontArial, "Exit condition: " + LevelManager.levelList[LevelManager.level].exitCondition.PrintCondition() , MoveIndent(), Color.White);
                spriteBatch.DrawString(TextureManager.FontArial, "Condition " + (LevelManager.levelList[LevelManager.level].exitCondition.CanExit() ? "is fulfilled!" : "is not fulfilled yet"), MoveIndent(), Color.White);
                spriteBatch.DrawString(TextureManager.FontArial, "Mana: " + Player.Instance.Mana, MoveIndent(), Color.White);
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

            spriteBatch.Draw(TextureManager.ui.tileHitboxBorder, rect, null, color, angle, origin, SpriteEffects.None, 0);
        }
    }
}
