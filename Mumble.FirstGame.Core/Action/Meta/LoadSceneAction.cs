﻿using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.ActionResult.Meta;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Meta
{
    public class LoadSceneAction : IAction
    {
        public List<IActionResult> Results { get; private set; }

        public void CalculateEffect(Director director)
        {
            director.TransitionScene();
            Results = new List<IActionResult>()
            {
                new SceneLoadedActionResult()
            };
        }
    }
}