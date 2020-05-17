using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
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
                    Debug.Write(action.Result.GetType().ToString());
                    foreach (FieldInfo field in action.Result.GetType().GetFields())
                    {
                        Debug.Write(";" + field.Name + ":" + field.GetValue(action.Result).ToString());
                    }
                }
            }
        }
    }
}
