using BurgerShop.DataBase.Interfaces;
using BurgerShop.Domain;

namespace BurgerShop.DataBase.EFImplementations
{
    public class EFBurgerRepository : IRepository<Burger>
    {
        private readonly BurgerShopDbContext _dbContext;

        public IEnumerable<Burger> GetAll()
        {
            return _dbContext.Burgers.ToList();
        }

        public Burger GetById(int id)
        {
            return _dbContext.Burgers.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Burger entity)
        {
            _dbContext.Burgers.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Burger entity)
        {
            _dbContext.Burgers.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int entity)
        {
            Burger burger = GetById(entity);

            if (burger != null)
            {
                _dbContext.Burgers.Remove(burger);
                _dbContext.SaveChanges();
            }

        }
    }
}
