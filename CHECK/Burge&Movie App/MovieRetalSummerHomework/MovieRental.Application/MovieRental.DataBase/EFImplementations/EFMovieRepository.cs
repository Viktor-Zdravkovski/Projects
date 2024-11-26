using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Dtos.Dto;

namespace MovieRental.DataBase.EFImplementations
{
    public class EFMovieRepository : IRepository<Movie>
    {
        private readonly MovieRentalDbContext _dbContext;

        public EFMovieRepository(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IEnumerable<Movie> GetAll()
        {
            return _dbContext.Movie.ToList();
        }

        public Movie GetById(int id)
        {
            //return new Movie { Id = id };
            return _dbContext.Movie.FirstOrDefault(x => x.Id == id);
        }
        public void Add(Movie entity)
        {
            _dbContext.Movie.Add(entity);
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Movie entity)
        {
            //if (entity.Title == null)
            //{
            //    entity.Title = _dbContext.Movie.EntityType.Name;
            //}

            _dbContext.Movie.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
