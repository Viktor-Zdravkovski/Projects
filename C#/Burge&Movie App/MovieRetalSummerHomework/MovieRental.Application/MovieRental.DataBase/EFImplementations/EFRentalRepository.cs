using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;

namespace MovieRental.DataBase.EFImplementations
{
    public class EFRentalRepository : IRepository<Rental>
    {
        private readonly MovieRentalDbContext _dbContext;

        public EFRentalRepository(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Rental> GetAll()
        {
            return _dbContext.Rental.ToList();
        }

        public Rental GetById(int id)
        {
            //return new Rental { Id = id };
            return _dbContext.Rental.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Rental entity)
        {
            _dbContext.Rental.Add(entity);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Rental entity)
        {
            _dbContext.Rental.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
