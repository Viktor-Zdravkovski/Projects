﻿using DropShipping.DataBase.Context;
using DropShipping.DataBase.Interfaces;
using DropShipping.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DropShipping.DataBase.Implementations.EFImpementations
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly DropShippingDbContext _context;

        public CategoryRepository(DropShippingDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Category;
        }

        public Category GetById(int id)
        {
            return _context.Category
                           .Include(c => c.Products)
                           .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Category entity)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == entity.Id);

            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Add(Category entity)
        {
            _context.Category.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Category entity)
        {
            _context.Category.Remove(entity);
            _context.SaveChanges();
        }
    }
}
