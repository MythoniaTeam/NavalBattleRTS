


namespace Mythonia.Game.Objects.Actions
{
    public class MActionManager : List<IAction>
    {
        public void Update(GameTime gameTime)
        {
            List<IAction> removeList = new List<IAction>();
            this.Log(false, $"Actions Count: {Count}");

            foreach (IAction action in this) 
                if(action.ActionUpdate(gameTime)) 
                    removeList.Add(action);

            foreach (IAction action in removeList)
                Remove(action);
        }
    }
}
