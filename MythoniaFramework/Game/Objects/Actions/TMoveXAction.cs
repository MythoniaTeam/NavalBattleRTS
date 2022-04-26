using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Actions
{
    public class TMoveXAction : MAction<Args.Numerical>
    {
        MObject obj;

        public TMoveXAction(float duration, Args.Numerical moveDistance) : base(duration, moveDistance)
        {
            Main = MainAction;
        }

        private bool MainAction(Args.Numerical args)
        {
            obj.Position.X += args.Value;
            return true;
        }
    }
}
