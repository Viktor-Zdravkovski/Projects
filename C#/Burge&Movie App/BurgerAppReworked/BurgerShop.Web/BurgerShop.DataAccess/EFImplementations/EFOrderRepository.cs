using BurgerShop.DataBase.Interfaces;
using BurgerShop.Domain;

namespace BurgerShop.DataBase.EFImplementations
{
    public class EFOrderRepository : IRepository<Order>
    {
        private readonly BurgerShopDbContext _dbContext;

        public IEnumerable<Order> GetAll()
        {
            return _dbContext.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _dbContext.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(int entity)
        {
            Order order = GetById(entity);

            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
            }
        }
    }
}
