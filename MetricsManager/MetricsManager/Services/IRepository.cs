namespace MetricsManager.Services
{
    public interface IRepository<T> where T : class
    {
        void Add(T item); 
        void Remove(int id);
        void Update(T item);
        void Enable(int id); 
        void Disable(int id);
        T[] GetAll(); 
        T GetById(int id);
    }
}
