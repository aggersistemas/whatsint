namespace Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionName : Attribute
    {
        public virtual string Name { get; }

        public CollectionName(string value)
        {
            Name = value;
        }
    }
}
