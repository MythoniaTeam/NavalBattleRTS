using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Actions
{
    public class TMoveXAction : MAction<Args.Numerical>
    {
        GameObject obj;

        public TMoveXAction(float duration, Args.Numerical moveDistance) : base(duration, moveDistance)
        {
            Main = MainAction;
        }

        private void MainAction(Args.Numerical args)
        {
            obj.Position.X += args.Value;
        }
    }
}
