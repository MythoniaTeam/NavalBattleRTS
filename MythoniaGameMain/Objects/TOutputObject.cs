using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mythonia.Framework.Game.Objects
{
    public class TOutputObject : MObject
    {
        public TOutputObject(MGame game, string name, int updateOrder) : base(game, name)
        {
            UpdateOrder = updateOrder;
            Debug.WriteLine($"!!!!!!!!!!!!!!!!!!!!!");

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debug.WriteLine($"{Name}, UpdateOrder: {UpdateOrder}");
        }
    }
}
