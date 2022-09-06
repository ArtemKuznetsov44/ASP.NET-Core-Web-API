namespace MetricsAgent.Services
{
    // Timeplate interface for universal
    public interface IRepository<T> where T : class
    {
        // Describing structure of repository: 
        IList<T> GetAll();
        IList<T> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
