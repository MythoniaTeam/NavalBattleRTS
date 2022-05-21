using XNAinput = Microsoft.Xna.Framework.Input;


namespace Mythonia.Game.Input
{
    public class Mouse : GameComponent
    {
        public static Mouse Ins;


        public MouseState State;

        public MVector Position => State.Position;

        


        public Mouse(MGame game) : base(game)
        {
            Ins = this;
            game.Components.Add(this);

            
        }


        public override void Initialize()
        {
            base.Initialize();
            UpdateOrder = -1000000;
            Update();
        }

        private void Update()
        {
            State = XNAinput.Mouse.GetState();
            foreach(GameComponent obj in Game.Components)
            {
                if (obj is IMouseTouchable touchable)
                {
                    touchable.CheckTouch(Position);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Update();
        }
    }
}
