


namespace Mythonia.Game.Objects.Actions
{
    public interface IActionArg<T>
    {
        public T MultipleRate(float multiplier);

        public float Rate { get; }


    }
}
