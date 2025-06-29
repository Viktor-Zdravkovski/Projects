using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;

namespace MovieRental.DataBase.EFImplementations
{
    public class EFUserRepository : IRepository<User>
    {
        private readonly MovieRentalDbContext _dbContext;

        public EFUserRepository(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IEnumerable<User> GetAll()
        {
            return _dbContext.User.ToList();
        }

        public User GetById(int id)
        {
            //return new User { Id = id };
            return _dbContext.User.FirstOrDefault(x => x.Id == id);
        }

        public void Add(User entity)
        {
            _dbContext.User.Add(entity);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _dbContext.User.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
