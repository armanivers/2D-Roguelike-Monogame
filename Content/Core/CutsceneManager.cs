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
        public static bool setScene = false;
        
        public static Queue<CutsceneBasis> scenesBuffer = new Queue<CutsceneBasis>();
        public static CutsceneBasis currentCutscene;

        public static float timer = 0;

        public static float delay = 1f;
        
        public static void QueueScene(CutsceneBasis cutscene)
        {
            scenesBuffer.Enqueue(cutscene);
        }

        public static void PlayCutsceneDelayed(CutsceneBasis cutscene, float inputDelay = 1f)
        {
            currentCutscene = cutscene;
            setScene = true;
            delay = inputDelay;
        }
        public static void PlayCutscene(CutsceneBasis cutscene)
        {
            currentCutscene = cutscene;
            setScene = true;
            activeCutscene = true;
        }

        public static void ClearCutscene()
        {
            currentCutscene = null;
            activeCutscene = false;
            setScene = false;
            timer = 0;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if(activeCutscene) currentCutscene?.Draw(spriteBatch);
        }

        public static void Update(GameTime gameTime)
        {
            // is there a currently set cutscene
            if (setScene)
            {
                // if its done, remove it
                if (currentCutscene.cutsceneDone) ClearCutscene();
            }

            // if a delayed scene is given to play but there isnt an active scene yet
            if(setScene && !activeCutscene)
            {
                timer++;
                // if delay of the scene is achieved, set the scene and play it
                if (timer > delay)
                {
                    activeCutscene = true;
                }
            }

            // check if there isn't an active scene
            if(!setScene)
            {
                // check if cutscene buffer has scenes which need to be displayed
                if (scenesBuffer.Count > 0) PlayCutscene(scenesBuffer.Dequeue());
            }

            currentCutscene?.Update(gameTime);
        }
    }
}
