using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Position;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Utils
{
    public static class DebugUtils
    {
        public static void PrintActions(List<IAction> actions)
        {
            if (actions.Count > 0)
            {
                foreach (IAction action in actions)
                {
                    Debug.WriteLine("");
                    Debug.Write(action.GetType().ToString());
                    foreach (FieldInfo field in action.GetType().GetFields())
                    {
                        Debug.Write(";" + field.Name + ":" + field.GetValue(action).ToString());
                    }
                }
            }
        }
        public static void DrawGrid(SpriteBatch spriteBatch, GraphicsDevice graphics, ScalingUtils scalingUtil,int width, int height)
        { 
            Texture2D texture1px = new Texture2D(graphics, 1, 1);
            texture1px.SetData(new Color[] { Color.White });
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    Vector2 scaled = scalingUtil.ScalePosition(new Vector2(i, j));
                    if (i % 5 == 0 || j % 5 == 0)
                    {
                        spriteBatch.Draw(texture1px, scaled, null, Color.Yellow, 0f,new Vector2(0,0),new Vector2(1,1),SpriteEffects.None,0f);
                    }
                    else
                    {
                        spriteBatch.Draw(texture1px, scaled, null, Color.Red, 0f, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0f);
                    }
                }
            }
        }
    }
}
