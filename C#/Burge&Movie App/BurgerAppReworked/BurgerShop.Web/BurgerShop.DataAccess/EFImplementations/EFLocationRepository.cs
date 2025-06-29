using BurgerShop.DataBase.Interfaces;
using BurgerShop.Domain;

namespace BurgerShop.DataBase.EFImplementations
{
    public class EFLocationRepository : IRepository<Location>
    {
        private readonly BurgerShopDbContext _context;

        public EFLocationRepository(BurgerShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Locations.ToList();
        }

        public Location GetById(int id)
        {
            return _context.Locations.FirstOrDefault(s => s.Id == id);

        }

        public void Add(Location entity)
        {
            _context.Locations.Add(entity);
            _context.SaveChanges();
        }


        public void Update(Location entity)
        {
            _context.Locations.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var location = _context.Locations.FirstOrDefault(o => o.Id == id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
        }
    }
}
