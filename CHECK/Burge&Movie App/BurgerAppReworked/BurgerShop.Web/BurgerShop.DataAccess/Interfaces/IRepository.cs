﻿using BurgerShop.Domain;

namespace BurgerShop.DataBase.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(int entity);
    }
}
