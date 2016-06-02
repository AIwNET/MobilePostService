using System;

namespace MobilePostService.Repositories
{
    interface IRepository<T>
    {
        void Add(T element);
        void Delete(T element);
        void SaveChanges();
    }
}
