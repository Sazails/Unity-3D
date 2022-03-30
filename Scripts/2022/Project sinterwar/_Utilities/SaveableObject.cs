namespace Assets._Core.Scripts._Utilities
{
    public interface SaveableObject
    {
        public void Save<T>();
        public object Load<T>();
    }
}