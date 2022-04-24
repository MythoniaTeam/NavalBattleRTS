﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Actions
{
    public class ActionManager : List<IAction>
    {
        public void Update(GameTime gameTime)
        {
            List<IAction> removeList = new List<IAction>();
            this.Log(true, $"Actions Count: {Count}");

            foreach (IAction action in this) 
                if(action.ActionUpdate(gameTime)) 
                    removeList.Add(action);

            foreach (IAction action in removeList)
                Remove(action);
        }
    }
}
