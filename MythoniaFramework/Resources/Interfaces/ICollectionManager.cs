


namespace Mythonia.Resources.Interfaces
{
    public interface ICollectionManager<ElementType, IdentificatorType> : ICollection<ElementType>
    {
        public ElementType this [IdentificatorType identificator] { get; set; }


    }
}
