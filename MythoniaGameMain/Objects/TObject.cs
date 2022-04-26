

namespace Mythonia.Framework.Game.Objects
{
    public class TObject : MObject
    {
        public TObject(MGame game, string name, MVector? position) : base(game, name, position)
        {
            this.LogConstruct(true);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.Log(true, "is Initializing");

            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n =>
            {
                Position.X += n.Value;
                return true;
            }));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n =>
            {
                Position.Y += n.Value;
                return true;
            }));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(50), n =>
            {
                Position.X += n.Value;
                return true;
            })); Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n =>
            {
                Position.X += n.Value;
                return true;
            }));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n =>
            {
                Position.Y += n.Value;
                return true;
            }));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(50), n =>
            {
                Position.X += n.Value;
                return true;
            }));
            
        }

        public override void Update(GameTime gameTime)
        {
            base.UpdateBefore(gameTime);

            this.Log(true, "Position: " + Position);

            base.UpdateAfter(gameTime);
        }

        
    }
}
