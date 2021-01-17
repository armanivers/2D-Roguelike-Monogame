using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class UIManager
    {
        public static List<UIElementBasis> uiElementsDynamic = new List<UIElementBasis>();
        public static List<UIElementBasis> uiElementsStatic = new List<UIElementBasis>();
        public static List<UIElementBasis> uiElements = new List<UIElementBasis>();
        public static BossBar bossbar;

        public static void Update(GameTime gameTime)
        {
            foreach(var ui in uiElements)
            {
                ui.Update(gameTime);
            }
            bossbar.Update(gameTime);
            MessageFactory.Update(gameTime);
        }
        public static void DrawStatic(SpriteBatch spriteBatch)
        {
            MessageFactory.Draw(spriteBatch);
                
            foreach (var ui in uiElementsStatic)    
            {
                ui.Draw(spriteBatch);
            }
            bossbar.Draw(spriteBatch);
        }

        public static void DrawDynamic(SpriteBatch spriteBatch)
        {
            foreach(var ui in uiElementsDynamic)
            {
                ui.Draw(spriteBatch);
            }
        }

        public static void ForceResolutionUpdate()
        {
            foreach(var ui in uiElements)
            {
                ui?.ForceResolutionUpdate();
            }
        }

        public static void AddUIElementDynamic(UIElementBasis ui)
        {
            if(ui != null)
            {
                uiElementsDynamic.Add(ui);
                uiElements.Add(ui);
            }
        }
        public static void AddUIElementStatic(UIElementBasis ui)
        {
            if (ui != null)
            {
                uiElementsStatic.Add(ui);
                uiElements.Add(ui);
            }
        }

        public static void Load()
        {
            AddUIElementDynamic(new MobHealthBars());

            AddUIElementStatic(new Fog());
            AddUIElementStatic(new Skillbar(Player.Instance));
            AddUIElementStatic(new ExperienceBar(Player.Instance));
            AddUIElementStatic(new HealthBar(Player.Instance));
            AddUIElementStatic(new ToolTip(Player.Instance));
            AddUIElementStatic(new Highscore());
            AddUIElementStatic(new KeyStatus(Player.Instance));
            AddUIElementStatic(new ManaBar(Player.Instance));
            AddUIElementStatic(new UsableItemsBar(Player.Instance));
            AddUIElementStatic(new PlayerEffects(Player.Instance));
            bossbar = new BossBar();
        }

        public static void Unload()
        {
            uiElements.Clear();
            uiElementsDynamic.Clear();
            uiElementsStatic.Clear();
            bossbar = null;
            MessageFactory.ClearMessages();
        }

        public static void SwitchBossBarState()
        {
            bossbar.SwitchState();
        }


    }
}
