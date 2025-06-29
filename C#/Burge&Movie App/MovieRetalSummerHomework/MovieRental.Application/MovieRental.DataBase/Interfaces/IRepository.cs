using MovieRental.Domain;

namespace MovieRental.DataBase.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void SaveChanges();

        void Update(T entity);
    }
}
