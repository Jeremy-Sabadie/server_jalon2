
namespace domain.Entities
{
    public class Category : IEntity, IEquatable<Category?>
    {


        public int Reference { get; set; }
        public string Name { get; set; }
        public List<Materiel> materiels { get; set; }
        public Category(int reference, string name)
        {
            Reference = reference;
            Name = name;
        }
        public Category()
        {

        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Category);
        }

        public bool Equals(Category? other)
        {
            return other is not null &&
                   Reference == other.Reference;
        }

        public static bool operator ==(Category? left, Category? right)
        {
            return EqualityComparer<Category>.Default.Equals(left, right);
        }

        public static bool operator !=(Category? left, Category? right)
        {
            return !(left == right);
        }
    }
}
