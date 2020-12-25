using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class UIManager
    {
        public static List<UIElement> uiElementsDynamic = new List<UIElement>();
        public static List<UIElement> uiElementsStatic = new List<UIElement>();

        public static List<UIElement> uiElements = new List<UIElement>();
        public static void Update(GameTime gameTime)
        {
            foreach(var ui in uiElements)
            {
                ui.Update(gameTime);
            }
            MessageFactory.Update(gameTime);
        }
        public static void DrawStatic(SpriteBatch spriteBatch)
        {
            MessageFactory.Draw(spriteBatch);
            foreach (var ui in uiElementsStatic)    
            {
                ui.Draw(spriteBatch);
            }
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

        public static void AddUIElementDynamic(UIElement ui)
        {
            if(ui != null)
            {
                uiElementsDynamic.Add(ui);
                uiElements.Add(ui);
            }
        }
        public static void AddUIElementStatic(UIElement ui)
        {
            if (ui != null)
            {
                uiElementsStatic.Add(ui);
                uiElements.Add(ui);
            }
        }

        public static void ClearElements()
        {
            uiElements.Clear();
            uiElementsDynamic.Clear();
            uiElementsStatic.Clear();
        }

    }
}
