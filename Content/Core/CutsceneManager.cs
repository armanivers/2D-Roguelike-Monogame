using _2DRoguelike.Content.Core.Cutscenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class CutsceneManager
    {
        public static bool activeCutscene = false;
        public static Queue<CutsceneBasis> scenesToDisplay = new Queue<CutsceneBasis>();
        public static CutsceneBasis currentCutscene;

        public static void PlayCutscene(CutsceneBasis cutscene)
        {
            currentCutscene = cutscene;
            activeCutscene = true;
        }

        public static void ClearCutscene()
        {
            currentCutscene = null;
            activeCutscene = false;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if(activeCutscene) currentCutscene?.Draw(spriteBatch);
        }

        public static void Update(GameTime gameTime)
        {
            if (currentCutscene.cutsceneDone) ClearCutscene();
                
            currentCutscene?.Update(gameTime);
        }
    }
}
